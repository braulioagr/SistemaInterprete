using Antlr4.Runtime;
using Practica3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica4
{
    class Ensamblador
    {

        #region Variables de Instancia
        private long cp;
        private long tamaño;
        private string nombre;
        private string[] directiva = { "BYTE", "RESW", "RESB" };
        private List<string> archivo;
        private List<string> intermedio;
        private List<string> errores;
        private Dictionary<string, long> tabSim;
        private Dictionary<string, string> instrucciones;
        SicEstandarLexer lexer;
        CommonTokenStream tokens;
        SicEstandarParser parser;
        #endregion

        #region Constructores

        public Ensamblador(string nombre)
        {
            this.nombre = nombre;
            this.instrucciones = new Dictionary<string, string>();
            this.errores = new List<string>();
            this.tabSim = new Dictionary<string, long>();
            this.intermedio = new List<string>();
            this.archivo = MetodosAuxiliares.leeArchivo(nombre);
            #region Agregamos Instrucciones
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

        #endregion

        #region Metodos

        public void compila(DataGridView dataGridViewIntermedio, int i)
        {
            string[] codigo;
            MyErrorListener errorListener;
            lexer = new SicEstandarLexer(new AntlrInputStream(this.Archivo[i]));
            tokens = new CommonTokenStream(lexer);
            parser = new SicEstandarParser(tokens);
            errorListener = new MyErrorListener(i + 1);
            parser.AddErrorListener(errorListener);
            parser.prog();
            codigo = this.Archivo[i].Split('\t');
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
                            this.cp = MetodosAuxiliares.convierteDecimal(codigo[2].Replace("H", ""));
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
                        this.intermedio.Add(this.cp.ToString() + "\t" + this.archivo[i]);
                        this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "no");
                    }
                }
                catch (ArgumentException)
                {
                    this.errores.Add("Linea" + (i + 1).ToString() + ": Error Simbolo repetido");
                    dataGridViewIntermedio.Rows.Remove(dataGridViewIntermedio.Rows[dataGridViewIntermedio.Rows.Count - 1]);
                    this.intermedio.Remove(this.intermedio.Last());
                    this.intermedio.Add(this.cp.ToString() + "\t" + this.archivo[i] + "*");
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
                    this.intermedio.Add(this.cp.ToString() + "\t\t\t\t");
                    codigo = new string[] { " ", " ", " " };
                    this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Vacia");
                }
            }
        }

        private void incrementaInstruccionDirectiva(string[] codigo)
        {
            if (this.instrucciones.Keys.Contains(codigo[1]) || codigo[1].Equals("WORD"))
            {
                this.cp += 3;
            }
            else if (this.instrucciones.Keys.Contains(codigo[1]))
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
                            cp += MetodosAuxiliares.convierteDecimal(codigo[2].Replace("H", ""));
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
            if (codigo.Length == 3)
            {
                dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], codigo[2], error);
            }
            else if (codigo.Length == 2)
            {
                dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], "", error);
            }
            this.intermedio.Add(MetodosAuxiliares.convierteHexadecimal(this.cp) + "\t" + this.archivo[i]);
        }

        #endregion


    }
}
