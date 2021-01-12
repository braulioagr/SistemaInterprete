using Antlr4.Runtime;
using Practica6;
using Practica9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica8
{
    public class Ensamblador
    {

        #region Variables de Instancia
        private long cp;
        private long tamaño;
        private string nombre;
        private bool paso1Logrado;
        private string[] directiva = { "BYTE", "RESW", "RESB" };
        private List<string> archivo;
        private List<string> intermedio;
        private List<string> errores;
        private List<bool> lineasError;
        private List<string> archivoObj;
        private string[] codigoObj;
        private Dictionary<string, long> tabSim;
        private Dictionary<string, string> instrucciones;
        private SICEstandarLexer lexer;
        private CommonTokenStream tokens;
        private SICEstandarParser parser;
        #endregion

        #region Constructores

        public Ensamblador(string nombre)
        {
            this.nombre = nombre;
            this.archivo = MetodosAuxiliares.leeArchivo(nombre);
            #region Agregamos Instrucciones
            this.instrucciones = new Dictionary<string, string>();
            this.instrucciones.Add("ADD", "18");
            this.instrucciones.Add("AND", "40");
            this.instrucciones.Add("COMP", "28");
            this.instrucciones.Add("DIV", "24");
            this.instrucciones.Add("J", "3C");
            this.instrucciones.Add("JEQ", "30");
            this.instrucciones.Add("JGT", "34");
            this.instrucciones.Add("JLT", "38");
            this.instrucciones.Add("JSUB", "48");
            this.instrucciones.Add("LDA", "00");
            this.instrucciones.Add("LDCH", "50");
            this.instrucciones.Add("LDL", "08");
            this.instrucciones.Add("LDX", "04");
            this.instrucciones.Add("MUL", "20");
            this.instrucciones.Add("OR", "44");
            this.instrucciones.Add("RD", "D8");
            this.instrucciones.Add("RSUB", "4C");
            this.instrucciones.Add("STA", "0C");
            this.instrucciones.Add("STCH", "54");
            this.instrucciones.Add("STL", "14");
            this.instrucciones.Add("STSW", "E8");
            this.instrucciones.Add("STX", "10");
            this.instrucciones.Add("SUB", "1C");
            this.instrucciones.Add("TD", "E0");
            this.instrucciones.Add("TIX", "2C");
            this.instrucciones.Add("WD", "DC");
            this.paso1Logrado = false;
            #endregion
        }

        #endregion

        #region Gets & Sets

        public string Nombre
        {
            get { return this.nombre; }
        }

        public Dictionary<string, string> Instrucciones
        {
            get { return this.instrucciones; }
        }

        public string[] Directivas
        {
            get { return this.directiva; }
        }

        public long CP
        {
            get { return this.cp; }
            set { this.cp = value; }
        }

        public List<string> Archivo
        {
            get { return this.archivo; }
        }

        public List<string> Errores
        {
            get { return this.errores; }
        }

        public List<string> Intermedio
        {
            get { return this.intermedio; }
        }

        public Dictionary<string, long> TabSim
        {
            get { return this.tabSim; }
        }

        public long Tamaño
        {
            get { return this.tamaño; }
            set { this.tamaño = value; }
        }


        public bool Paso1Logrado
        {
            get { return this.paso1Logrado; }
        }

        public List<string> ArchivoObj
        {
            get { return this.archivoObj; }
        }

        #endregion

        #region Metodos

        #region Paso 1

        public void paso1(DataGridView dataGridViewIntermedio)
        {
            string[] codigo;
            MyErrorListener errorListener;
            this.lineasError = new List<bool>();
            this.errores = new List<string>();
            this.intermedio = new List<string>();
            this.tabSim = new Dictionary<string, long>();
            for (int i = 0; i < this.Archivo.Count; i++)
            {
                lexer = new SICEstandarLexer(new AntlrInputStream(this.Archivo[i]));
                tokens = new CommonTokenStream(lexer);
                parser = new SICEstandarParser(tokens);
                errorListener = new MyErrorListener(i + 1);
                parser.AddErrorListener(errorListener);
                parser.prog();
                codigo = this.Archivo[i].Split('\t');
                this.lineasError.Add(errorListener.ExisteError);
                if (!errorListener.ExisteError)
                {
                    try
                    {
                        if (!codigo[1].Equals("START") && !codigo[1].Equals("END"))
                        {
                            this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "No");
                            if (!string.IsNullOrEmpty(codigo[0]))
                            {
                                this.TabSim.Add(codigo[0], this.cp);
                            }
                            this.incrementaInstruccionDirectiva(codigo);
                        }
                        else if (codigo[1].Equals("START"))
                        {
                            codigo[2] = codigo[2].ToUpper();
                            if (codigo[2].Contains("H"))
                            {
                                this.cp = MetodosAuxiliares.hexadecimalADecimal(codigo[2].Replace("H", ""));
                                this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "No");
                            }
                            else
                            {
                                this.cp = long.Parse(codigo[2]);
                                this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "no");
                            }
                        }
                        else if (codigo[1].Equals("END"))
                        {
                            //this.intermedio.Add(this.cp.ToString() + "\t" + this.archivo[i]);
                            this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "no");
                        }
                    }
                    catch (ArgumentException)
                    {
                        this.errores.Add("Linea" + (i + 1).ToString() + ": Error Simbolo repetido");
                        dataGridViewIntermedio.Rows.Remove(dataGridViewIntermedio.Rows[dataGridViewIntermedio.Rows.Count - 1]);
                        this.intermedio.Remove(this.intermedio.Last());
                        //this.intermedio.Add(this.cp.ToString() + "\t" + this.archivo[i] + "*");
                        this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Simbolo");
                        this.incrementaInstruccionDirectiva(codigo);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.Archivo[i]))
                    {
                        if (this.instrucciones.Keys.Contains(codigo[1]) || this.Directivas.Contains(codigo[1]) || codigo[1].Equals("WORD"))
                        {
                            if (this.instrucciones.Keys.Contains(codigo[0]) || this.Directivas.Contains(codigo[0]) || codigo[0].Equals("WORD"))
                            {
                                this.errores.Add("Linea" + (i + 1).ToString() + ": Error de sintaxis la etiqueta no puede ser la palabra reservada \"" + codigo[0] + "\"");
                                this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Syntax");
                            }
                            else
                            {
                                this.errores.Add("Linea" + (i + 1).ToString() + ": Error de sintaxis el operando: \"" + codigo[2] + "\" Esta mal escrito");
                                this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Syntax");
                            }
                        }
                        else
                        {
                            this.errores.Add("Linea" + (i + 1).ToString() + ": Error instruccion \"" + codigo[1] + "\" no existe");
                            this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Instruccion");
                        }
                    }
                    else
                    {
                        this.errores.Add("Linea" + (i + 1).ToString() + ": Error de sintaxis no debe haber lineas vacias");
                        codigo = new string[] { "\t", "\t", "\t" };
                        this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Vacia");
                        
                    }
                }
            }
            this.paso1Logrado = true;
        }

        private void incrementaInstruccionDirectiva(string[] codigo)
        {
            if (this.instrucciones.Keys.Contains(codigo[1]) || codigo[1].Equals("WORD"))
            {
                this.cp += 3;
            }
            else if (this.directiva.Contains(codigo[1]))
            {
                codigo[2] = codigo[2].ToUpper();
                switch (codigo[1])
                {
                    case "BYTE":
                        if (codigo[2].Contains("X"))
                        {
                            cp += (long)Math.Ceiling((decimal)(codigo[2].Length - 3) / 2);
                        }
                        else if (codigo[2].Contains("C"))
                        {
                            cp += codigo[2].Length - 3;
                        }
                        break;
                    case "RESW":
                        cp += 3 * long.Parse(codigo[2]);
                        break;
                    case "RESB":
                        codigo[2] = codigo[2].ToUpper();
                        if (codigo[2].Contains("H"))
                        {
                            cp += MetodosAuxiliares.hexadecimalADecimal(codigo[2].Replace("H", ""));
                        }
                        else
                        {
                            cp += long.Parse(codigo[2]);
                        }
                        break;
                }
            }
        }

        private void ensamblaIntermedio(DataGridView dataGridViewIntermedio, string[] codigo, int i, string error)
        {
            if (!error.Equals("Vacia"))
            {
                if (codigo.Length == 3)
                {
                    dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.decimalAHexadecimal(this.cp), codigo[0], codigo[1], codigo[2], error);
                    this.intermedio.Add(MetodosAuxiliares.decimalAHexadecimal(this.cp) + "\t" + this.archivo[i] + "\t" + error);
                }
                else if (codigo.Length == 2)
                {
                    dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.decimalAHexadecimal(this.cp), codigo[0], codigo[1], "", error);
                    this.intermedio.Add(MetodosAuxiliares.decimalAHexadecimal(this.cp) + "\t" + this.archivo[i] + "\t\t" + error);
                }
            }
            else
            {
                dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.decimalAHexadecimal(this.cp), codigo[0], codigo[1], codigo[2], error);
                this.intermedio.Add(MetodosAuxiliares.decimalAHexadecimal(this.cp) + "\t\t\t\t"+error);
            }
        }

        #endregion

        #region Paso 2

        public void paso2(DataGridView dataGridViewIntermedio)
        {
            bool x;
            int resto;
            string aux;
            char[] cadena;
            string[] linea;
            this.codigoObj = new string[this.intermedio.Count];
            for (int i = 0; i < this.intermedio.Count; i++)
            {
                linea = this.intermedio[i].Split('\t');
                if (!string.IsNullOrEmpty(linea[2]))
                {
                    if (!this.lineasError[i])
                    {
                        if (this.instrucciones.Keys.Contains(linea[2]) | linea[2].Equals("WORD") | linea[2].Equals("BYTE"))
                        {
                            switch (linea[2])
                            {
                                case "BYTE":
                                    #region Byte
                                    if (linea[3].Contains("X") | linea[3].Contains("x"))
                                    {
                                        //FE
                                        codigoObj[i] = linea[3].Replace("X'", "");
                                        codigoObj[i] = codigoObj[i].Replace("x'", "");
                                        codigoObj[i] = codigoObj[i].Replace("'", "");
                                    }
                                    else
                                    {
                                        codigoObj[i] = linea[3].Replace("C'", "");
                                        codigoObj[i] = codigoObj[i].Replace("c'", "");
                                        codigoObj[i] = codigoObj[i].Replace("'", "");
                                        codigoObj[i] = MetodosAuxiliares.ASCIIToHexadecimal(codigoObj[i]);
                                    }
                                    #endregion
                                break;
                                case "WORD":
                                    #region WORD
                                    x = linea[3].Contains("H") | linea[3].Contains("h");
                                    codigoObj[i] = linea[3].Replace("h", "");
                                    codigoObj[i] = linea[3].Replace("H", "");
                                    if (!x)
                                    {
                                        codigoObj[i] = MetodosAuxiliares.decimalAHexadecimal(int.Parse(codigoObj[i]));
                                    }
                                    if (codigoObj[i].Length < 6)
                                    {
                                        resto = 6 - codigoObj[i].Length;
                                        aux = "";
                                        for(int j = 0; j < resto; j++)
                                        {
                                            aux += "0";
                                        }
                                        codigoObj[i] = aux + codigoObj[i];
                                    }
                                    else if(codigoObj[i].Length > 6)
                                    {
                                        codigoObj[i] = codigoObj[i].Substring(0, 6);
                                    }
                                    #endregion
                                break;
                                default:
                                    #region Instrucciones
                                    if (!linea[2].Equals("RSUB"))
                                    {
                                        linea[3] = linea[3].Replace(" ", "");
                                        x = linea[3].Contains(",X") | linea[3].Contains(",x");
                                        linea[3] = linea[3].Replace(",X", "");
                                        linea[3] = linea[3].Replace(",x", "");
                                        if (this.tabSim.Keys.Contains(linea[3]))
                                        {
                                            codigoObj[i] = this.instrucciones[linea[2]] + MetodosAuxiliares.decimalAHexadecimal(this.tabSim[linea[3]]);
                                        }
                                        else
                                        {
                                            codigoObj[i] = this.instrucciones[linea[2]] + "7FFF";
                                        }
                                        if (x)
                                        {
                                            codigoObj[i] = MetodosAuxiliares.hexadecimalABinario(codigoObj[i]);
                                            cadena = codigoObj[i].ToCharArray();
                                            cadena[8] = '1';
                                            codigoObj[i] = new string(cadena);
                                            codigoObj[i] = MetodosAuxiliares.decimalAHexadecimal(MetodosAuxiliares.binarioADecimal(codigoObj[i]));
                                        }
                                    }
                                    else
                                    {
                                        codigoObj[i] = this.instrucciones[linea[2]] +"0000";
                                    }
                                    #endregion
                                break;
                            }
                        }
                        else
                        {
                            codigoObj[i] = "---";

                        }
                    }
                    else
                    {
                        codigoObj[i] = "Error: de Sintaxis";
                    }
                }
                else
                {
                    this.codigoObj[i] = "Error: Linea vacia";
                }
            }
            this.ensamblaIntermedio(dataGridViewIntermedio);
            this.creaArchivoObj();
        }

        private void ensamblaIntermedio(DataGridView dataGridViewIntermedio)
        {
            dataGridViewIntermedio.Rows.Clear();
            for(int i = 0 ;  i < this.intermedio.Count ; i++)
            {
                dataGridViewIntermedio.Rows.Add(((i + 1).ToString()+"\t"+this.intermedio[i]+"\t"+codigoObj[i]).Split('\t'));
            }
        }


        private void creaArchivoObj()
        {
            string linea;
            string[] instruccion;
            this.archivoObj = new List<string>();
            instruccion = this.intermedio.First().Split('\t');
            linea = instruccion[3].Replace("H", "");
            linea = linea.Replace("h", "");
            linea = "H" + MetodosAuxiliares.ajustaCadena(instruccion[1], 6) + MetodosAuxiliares.ajustaDireccion(linea) +
                          MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.tamaño));
            this.archivoObj.Add(linea);
            this.generaRegistrosT();
            instruccion = this.intermedio.Last().Split('\t');
            linea ="E"+this.calculaDireccionEnd(instruccion[3]);
            this.archivoObj.Add(linea);

        }

        private void generaRegistrosT()
        {
            int i;
            int j;
            string registroT;
            string aux;
            string bytes;
            aux = "";
            i = 1;
            while (!this.archivo[i].Contains("END"))
            {
                registroT = "T" + MetodosAuxiliares.ajustaDireccion(this.intermedio[i].Split('\t').First());
                for(j = i ; j < this.codigoObj.Length-1 ; j++)
                {
                    if (!this.codigoObj[j].Contains("Error"))
                    {
                        if (((aux.Length + this.codigoObj[j].Length) / 2) > 30)
                        {
                            break;
                        }
                        else if (this.codigoObj[j].Equals("---"))
                        {
                            break;
                        }
                        else 
                        {
                            aux += this.codigoObj[j];
                        }
                    }

                }
                bytes = MetodosAuxiliares.decimalAHexadecimal(aux.Length / 2);
                if (bytes.Length < 2)
                {
                    bytes = "0" + bytes;
                }
                registroT += bytes;
                registroT += aux;
                aux = "";
                this.archivoObj.Add(registroT);
                for (int k = j; k < this.codigoObj.Length; k++)
                {
                    if(!codigoObj[k].Equals("---") | this.intermedio[k].Contains("END"))
                    {
                        i = k;
                        break;
                    }
                }

            } 
        }

        private string calculaDireccionEnd(string instruccion)
        {
            string inicio;
            string[] aux;
            inicio = "";
            try
            {

                if (!string.IsNullOrEmpty(instruccion))
                {
                    inicio = MetodosAuxiliares.decimalAHexadecimal(this.tabSim[instruccion]);
                }
                else
                {
                    foreach (string linea in this.intermedio)
                    {
                        aux = linea.Split('\t');
                        if (this.instrucciones.Keys.Contains(aux[2]))
                        {
                            inicio = aux[0];
                            break;
                        }
                    }
                }
                inicio = MetodosAuxiliares.ajustaDireccion(inicio);
            }
            catch (KeyNotFoundException)
            {
                inicio = "FFFFFF";
            }
            return inicio;
        }



        #endregion

        #endregion

    }
}
