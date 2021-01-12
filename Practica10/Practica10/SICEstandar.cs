using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica10
{
    class SICEstandar
    {
        #region Variables de instancia
        private bool ejecutable;
        private long a;
        private long x;
        private long l;
        private long cp;
        private string cc;
        private string nombre;
        private Memoria memoria;
        #endregion

        #region Constructores

        public SICEstandar(string nombre)
        {
            this.ejecutable = true;
            this.a = 16777215;
            this.x = 16777215;
            this.l = 16777215;
            this.cc = "null";
            this.nombre = nombre;
            this.memoria = new Memoria(this.nombre);
            cp = this.memoria.vaciaRegistrosT();
        }
        
        #endregion

        #region Gets & Sets

        public Memoria Memoria
        {
            get { return this.memoria; }
        }

        public long CP
        {
            get { return this.cp; }
        }
        
        public long A
        {
            get { return this.a; }
        }
        
        public long X
        {
            get { return this.x; }
        }
        
        public long L
        {
            get { return this.l; }
        }
        
        public string CC
        {
            get { return this.cc; }
        }

        #endregion

        #region Metodos

        #region Ejecución

        public string ejecutaPaso()
        {
            string mensaje;
            mensaje = "";
            try
            {
                if (ejecutable)
                {
                    string codOp;
                    string m;
                    codOp = this.memoria.ObtenCodOp(ref this.cp);
                    m = this.memoria.ObtenM(ref this.cp);
                    m = this.indexaM(m);
                    if (this.cp < MetodosAuxiliares.FinalPrograma(this.memoria.Inicio, this.memoria.Tamaño))
                    {
                        if (MetodosAuxiliares.hexadecimalADecimal(m) <= MetodosAuxiliares.FinalPrograma(this.memoria.Inicio, this.memoria.Tamaño))
                        {
                            switch (codOp)
                            {
                                case "18"://ADD
                                    mensaje = this.ADD(m);
                                    break;
                                case "40"://AND
                                    mensaje = this.AND(m);
                                    break;
                                case "28"://COMP
                                    mensaje = this.COMP(m);
                                    break;
                                case "24"://DIV
                                    mensaje = this.DIV(m);
                                    break;
                                case "3C"://J
                                    mensaje = this.J(m);
                                    break;
                                case "30"://JEQ
                                    mensaje = this.JEQ(m);
                                    break;
                                case "34"://JGT
                                    mensaje = this.JGT(m);
                                    break;
                                case "38"://JLT
                                    mensaje = this.JLT(m);
                                    break;
                                case "48"://JSUB
                                    mensaje = this.JSUB(m);
                                    break;
                                case "00"://LDA
                                    mensaje = this.LDA(m);
                                    break;
                                case "50"://LDCH
                                    mensaje = this.LDCH(m);
                                    break;
                                case "08"://LDL
                                    mensaje = this.LDL(m);
                                    break;
                                case "04"://LDX
                                    mensaje = this.LDX(m);
                                    break;
                                case "20"://MUL
                                    mensaje = this.MUL(m);
                                    break;
                                case "44"://OR
                                    mensaje = this.OR(m);
                                    break;
                                case "D8"://RD
                                    mensaje = this.RD(m);
                                    break;
                                case "4C"://RSUB
                                    mensaje = this.RSUB();
                                    break;
                                case "0C"://STA
                                    mensaje = this.STA(m);
                                    break;
                                case "54"://STCH
                                    mensaje = this.STCH(m);
                                    break;
                                case "14"://STL
                                    mensaje = this.STL(m);
                                    break;
                                case "E8"://STSW
                                    mensaje = this.STSW(m);
                                    break;
                                case "10"://STX
                                    mensaje = this.STX(m);
                                    break;
                                case "1C"://SUB
                                    mensaje = this.SUB(m);
                                    break;
                                case "E0"://TD
                                    //mensaje = this.TD(m);
                                    break;
                                case "2C"://TIX
                                    mensaje = this.TIX(m);
                                    break;
                                case "DC"://WD
                                    //mensaje = this.WD(m);
                                    break;
                            }
                        }
                        else
                        {
                            throw new EndProgramException("Error se intento acceder a una localidad de memoria inexistente");
                        }
                    }
                    else
                    {
                        throw new EndProgramException("Se acabaron las intrucciones a ejecutar, por lo tanto el programa finalizo");
                    }
                }
            }
            catch(EndProgramException e)
            {
                this.ejecutable = false;
                MessageBox.Show(e.Message, "El programa ah finalizado", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            return mensaje;
        }

        private string indexaM(string m)
        {
            char[] aux;
            long maux;
            
            aux = MetodosAuxiliares.hexadecimalABinario(m).ToCharArray();
            if (aux[0].Equals('1'))
            {
                aux[0] = '0';
                maux = MetodosAuxiliares.binarioADecimal(new string(aux));
                maux += this.x;
                m = MetodosAuxiliares.decimalAHexadecimal(maux);
            }
            return m;
        }

        private string[] obtenOperando(string direccion)
        {
            long i;
            long j;
            string[] m;
            m = new string[3];
            i = this.memoria.ObtenFila(direccion);
            j = MetodosAuxiliares.hexadecimalADecimal(direccion.Last().ToString());
            m[0] = this.memoria.Mapa[i, j];
            j++;
            if (j > 16)
            {
                i++;
                j = 0;
            }
            m[1] = this.memoria.Mapa[i, j];
            j++;
            if (j > 16)
            {
                i++;
                j = 0;
            }
            m[2] = this.memoria.Mapa[i, j];
            return m;
        }

        #endregion

        #region Instrucciónes

        #region Braulio

        private string AND(string m)
        {
            char[] aux;
            string mensaje;
            string binA;
            string binM;
            string[] operando;
            operando = this.obtenOperando(m);
            binA = MetodosAuxiliares.hexadecimalABinario(MetodosAuxiliares.decimalAHexadecimal(this.a));
            binM = MetodosAuxiliares.hexadecimalABinario(operando[0] + operando[1] + operando[2]);
            aux = new char[binA.Length];
            mensaje = "Efecto: A ← (A) & (m, m+1, m+2)\n";
            mensaje += "\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a)
                     + " & " + operando[0] + operando[1] + operando[2] + "\n";
            for (int i = 0; i < aux.Length; i++)
            {
                if (binA[i].Equals('1') & binM[i].Equals('1'))
                {
                    aux[i] = '1';
                }
                else
                {
                    aux[i] = '0';
                }
            }

            this.a = MetodosAuxiliares.binarioADecimal(new string(aux));
            mensaje += "\t\t Resultado: A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a);
            return mensaje;
        }

        private string JLT(string m)
        {
            string mensaje;
            mensaje = "Efecto: CP ← m si CC está en <\n";
            if (cc.Equals("<"))
            {
                this.cp = MetodosAuxiliares.hexadecimalADecimal(m);
                mensaje += "\t\t CP ← " + this.cp;
            }
            else
            {
                mensaje += "\t\t No cumple con las condiciones, el CP no fue alterado";
            }
            return mensaje;
        }

        private string JSUB(string m)
        {
            string mensaje;
            mensaje = "L ← (CP); CP ← m\n";
            mensaje = "\t\t L ← " + MetodosAuxiliares.decimalAHexadecimal(this.cp) + ";  CP ← " + m;
            this.l = this.cp;
            cp = MetodosAuxiliares.hexadecimalADecimal(m);
            return mensaje;
        }

        private string LDA(string m)
        {
            string mensaje;
            string[] operando;
            operando = this.obtenOperando(m);
            mensaje = "Efecto A ← (m, m+1, m+2)\n";
            mensaje = "\t\t A ← " + operando[0] + operando[1] + operando[2];
            this.a = MetodosAuxiliares.hexadecimalADecimal(operando[0] + operando[1] + operando[2]);
            return mensaje;
        }

        private string LDX(string m)
        {
            string mensaje;
            string[] operando;
            operando = this.obtenOperando(m);
            mensaje = "Efecto X ← (m, m+1, m+2)\n";
            mensaje = "\t\t X ← " + operando[0] + operando[1] + operando[2];
            this.x = MetodosAuxiliares.hexadecimalADecimal(operando[0] + operando[1] + operando[2]);
            return mensaje;
        }

        private string TIX(string m)
        {
            string mensaje;
            string[] operando;
            long decM;
            mensaje = "Efecto: X ← (X) + 1; (X) : (m..m+2) \n" +
                      "Incrementa el valor de X en 1 y lo compara con una \n" +
                      "palabra en memoria y establece el código de \n" +
                      "condición para indicar el resultado(<, = o >) \n";
            operando = this.obtenOperando(m);
            mensaje = "\t X ← " + MetodosAuxiliares.decimalAHexadecimal(this.x) + " + 1; " + MetodosAuxiliares.decimalAHexadecimal(this.x)
                    + " : " + operando[0] + operando[1] + operando[2] + "\n";
            decM = MetodosAuxiliares.hexadecimalADecimal(operando[0] + operando[1] + operando[2]);
            this.x++;
            if (this.x == decM)
            {
                this.cc = "=";
            }
            else if (this.x > decM)
            {
                this.cc = ">";
            }
            else if (this.x < decM)
            {
                this.cc = "<";
            }
            mensaje += "\t\t Resultado: X ← " + MetodosAuxiliares.decimalAHexadecimal(this.x) + "; CC ← " + this.cc;
            return mensaje;
        }

        private string SUB(string m)
        {
            string mensaje;
            string[] operando;
            operando = this.obtenOperando(m);
            mensaje = "Efecto: A ← (A) – (m, m+1, m+2)\n";
            mensaje = "\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a) + "+" + operando[0] + operando[1] + operando[2];
            this.a = this.a - MetodosAuxiliares.hexadecimalADecimal(operando[0] + operando[1] + operando[2]);
            mensaje = "\t\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a);
            return mensaje;
        }

        #endregion

        #region Moreno

        private string ADD(string m)
        {
            //A ← (A) + (m..m+2)
            string mensaje;
            string[] operando;
            operando = this.obtenOperando(m);
            mensaje = "Efecto: A ← (A) + (m, m+1, m+2)\n";
            mensaje = "\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a) + "+" + operando[0] + operando[1] + operando[2];
            this.a = this.a + MetodosAuxiliares.hexadecimalADecimal(operando[0] + operando[1] + operando[2]);
            mensaje = "\t\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a);
            return mensaje;
        }

        private string STA(string m)
        {
            //m..m+2 ← (A)
            string mensaje;
            string[] operando;
            string aux;
            char[] aux2;
            operando = this.obtenOperando(m);
            mensaje = "(m, m+1, m+2) ← (A)\n";
            mensaje = "\t\t (m, m+1, m+2) ← " + MetodosAuxiliares.decimalAHexadecimal(a);
            aux = MetodosAuxiliares.decimalAHexadecimal(a);
            aux2 = aux.ToCharArray();
            operando[0] = Convert.ToString(aux2[0]) + Convert.ToString(aux2[1]);
            operando[1] = Convert.ToString(aux2[2]) + Convert.ToString(aux2[3]);
            operando[2] = Convert.ToString(aux2[4]) + Convert.ToString(aux2[5]);
            this.memoria.almacenaDatos(operando,3,m);
            //mensaje = "\t\t (m, m+1, m+2) ← " + MetodosAuxiliares.decimalAHexadecimal(Convert.ToInt32(operando[0])) + MetodosAuxiliares.decimalAHexadecimal(Convert.ToInt32(operando[1])) + MetodosAuxiliares.decimalAHexadecimal(Convert.ToInt32(operando[2]));
            mensaje = "\t\t (m, m+1, m+2) ← " + operando[0] + operando[1] + operando[2];
            return mensaje;
        }

        private string STL(string m)
        {
            // m..m+2 ← (L)
            string mensaje;
            string[] operando;
            string aux;
            char[] aux2;
            operando = this.obtenOperando(m);
            mensaje = "(m, m+1, m+2) ← (L)\n";
            mensaje = "\t\t (m, m+1, m+2) ← " + MetodosAuxiliares.decimalAHexadecimal(this.l);
            aux = MetodosAuxiliares.decimalAHexadecimal(this.l);
            aux2 = aux.ToCharArray();
            operando[0] = Convert.ToString(aux2[0]) + Convert.ToString(aux2[1]);
            operando[1] = Convert.ToString(aux2[2]) + Convert.ToString(aux2[3]);
            operando[2] = Convert.ToString(aux2[4]) + Convert.ToString(aux2[5]);
            this.memoria.almacenaDatos(operando, 3, m);
            mensaje = "\t\t (m, m+1, m+2) ← " + operando[0] + operando[1] + operando[2];
            return mensaje;
        }

        private string JGT(string m)
        {
            string mensaje;
            mensaje = "Efecto: CP ← m si CC está en <\n";
            if (cc.Equals(">"))
            {
                this.cp = MetodosAuxiliares.hexadecimalADecimal(m);
                mensaje += "\t\t CP ← " + this.cp;
            }
            else
            {
                mensaje += "\t\t No cumple con las condiciones, el CP no fue alterado";
            }
            return mensaje;
        }

        private string MUL(string m)
        {
            //A ← (A) * (m..m+2)
            string mensaje;
            string[] operando;
            operando = this.obtenOperando(m);
            mensaje = "Efecto: A ← (A) * (m, m+1, m+2)\n";
            mensaje = "\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a) + "*" + operando[0] + operando[1] + operando[2];
            this.a = this.a * MetodosAuxiliares.hexadecimalADecimal(operando[0] + operando[1] + operando[2]);
            mensaje = "\t\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a);
            return mensaje;
        }

        private string RSUB()
        {
            //PC ← (L)
            string mensaje;
            mensaje = "Efecto PC ← (L)\n";
            mensaje = "\t\t PC ← " + MetodosAuxiliares.decimalAHexadecimal(this.l);
            this.cp = this.l;
            return mensaje;
        }

        private string STSW(string m)
        {
            //m..m+2 ← (SW)
            string mensaje;
            string[] operando;
            operando = this.obtenOperando(m);
            mensaje = "(m, m+1, m+2) ← (SW)\n";
            mensaje = "\t\t (m, m+1, m+2) ← " + cc;
            operando[0] = cc;
            operando[1] = cc;
            operando[2] = cc;
            return mensaje;
        }

        private string STCH(string m)
        {
            //m ← (A) [el byte más a la derecha]
            string mensaje;
            string[] A;
            char[] aux;
            A = new string[1];
            mensaje = "Efecto: m ← (A) [el byte más a la derecha]\n";
            A[0] = MetodosAuxiliares.decimalAHexadecimal(this.a);
            aux = A[0].ToCharArray();
            A[0] = Convert.ToString(aux[aux.Length - 2]) + Convert.ToString(aux[aux.Length - 1]);
            mensaje = "\t\t "+m+" ← " + A[0];
            this.memoria.almacenaDatos(A, 1, m);
            
            return mensaje;
        }

        #endregion

        #region Aldo

        private string COMP(string m)
        {
            long valorTemp;
            string sentencia = "";
            sentencia = this.memoria.BuscaDireccion3(m);
            valorTemp = MetodosAuxiliares.hexadecimalADecimal(sentencia);
            if (this.a < valorTemp)
            {
                this.cc = "<";
            }
            else if (this.a > valorTemp)
            {
                this.cc = ">";
            }
            else if (this.a == valorTemp)
            {
                this.cc = "=";
            }

            return "(A) : (m..m+2)";
        }
        private string DIV(string m)
        {
            long valorTemp;
            string sentencia = "";
            sentencia = this.memoria.BuscaDireccion3(m);
            valorTemp = MetodosAuxiliares.hexadecimalADecimal(sentencia);

            this.a = this.a / valorTemp;
            return "(A) <- (A)/(m..m+2)";
        }
        private string J(string m)/////
        {
            this.cp = MetodosAuxiliares.hexadecimalADecimal(m);

            return "CP<-m";
        }
        private string JEQ(string m)
        {
            if (cc.Equals("="))
            {
                this.cp = MetodosAuxiliares.hexadecimalADecimal(m);
            }
            return "CP <- m si CC esta en =";
        }
        private string LDCH(string m)
        {
            string cadena = "";
            string aux = "";
            string hexadecimal = "";
            string valorA = MetodosAuxiliares.decimalAHexadecimal(this.a);
            aux = valorA.Substring(0, 4);
            hexadecimal = MetodosAuxiliares.hexadecimalABinario(m);
            cadena = memoria.BuscaFinal(m);
            aux += cadena;
            this.a = MetodosAuxiliares.hexadecimalADecimal(aux);
            return "A[el byte de mas a la derecha ] <- (m)";
        }
        private string LDL(string m)
        {
            string sentencia = "";
            long valorL;
            sentencia = memoria.BuscaDireccion3(m);
            valorL = MetodosAuxiliares.hexadecimalADecimal(sentencia);
            this.l = valorL;

            return "L <- (m...m+2)";
        }
        private string OR(string m)
        {
            char[] aux;
            string mensaje;
            string binA;
            string binM;
            string[] operando;
            operando = this.obtenOperando(m);
            binA = MetodosAuxiliares.hexadecimalABinario(MetodosAuxiliares.decimalAHexadecimal(this.a));
            binM = MetodosAuxiliares.hexadecimalABinario(operando[0] + operando[1] + operando[2]);
            aux = new char[binA.Length];
            mensaje = "Efecto: A ← (A) or (m, m+1, m+2)\n";
            mensaje += "\t A ← " + MetodosAuxiliares.decimalAHexadecimal(this.a)
                     + " & " + operando[0] + operando[1] + operando[2] + "\n";
            for (int i = 0; i < aux.Length; i++)
            {
                if (binA[i].Equals('1') | binM[i].Equals('1'))
                {
                    aux[i] = '1';
                }
                else
                {
                    aux[i] = '0';
                }
            }
            return mensaje;
        }
        private string RD(string m)////
        {
            return "A[el byte mas a la derecha] <- datos del dispositivo especificado por (m)";
        }
        private string STX(string m)////
        {
            // m..m+2 ← (X)
            string mensaje;
            string[] operando;
            string aux;
            char[] aux2;
            operando = this.obtenOperando(m);
            mensaje = "(m, m+1, m+2) ← (X)\n";
            mensaje = "\t\t (m, m+1, m+2) ← " + MetodosAuxiliares.decimalAHexadecimal(this.x);
            aux = MetodosAuxiliares.decimalAHexadecimal(this.x);
            aux2 = aux.ToCharArray();
            operando[0] = Convert.ToString(aux2[0]) + Convert.ToString(aux2[1]);
            operando[1] = Convert.ToString(aux2[2]) + Convert.ToString(aux2[3]);
            operando[2] = Convert.ToString(aux2[4]) + Convert.ToString(aux2[5]);
            this.Memoria.almacenaDatos(operando, 3, m);
            //mensaje = "\t\t (m, m+1, m+2) ← " + Paso1.convierteHEX(Convert.ToInt32(operando[0])) + Paso1.convierteHEX(Convert.ToInt32(operando[1])) + Paso1.convierteHEX(Convert.ToInt32(operando[2]));
            mensaje = "\t\t (m, m+1, m+2) ← " + operando[0] + operando[1] + operando[2];
            return mensaje;
        }


        #endregion

        #endregion

        #endregion
    }
}
