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
        private long cp;
        private string nombre;
        private string[] codOp = {"ADD", "AND", "COMP", "DIV", "J", "JEQ", "JGT", "JLT", "JSUB",
                          "LDA", "LDCH", "LDL", "LDX", "MUL", "OR", "RD", "STA", "STCH",
                          "STL", "STSW", "STX", "SUB", "TD", "TIX", "WD", "RSUB"};
        private string[] directiva = { "BYTE", "RESW", "RESB" };
        private List<string> archivo;
        private List<string> errores;
        private Dictionary<string, long> tabSim;
        SicEstandarLexer lexer;
        CommonTokenStream tokens;
        SicEstandarParser parser;

        public Ensamblador(string nombre)
        {
            this.nombre = nombre;
            this.archivo = MetodosAuxiliares.leeArchivo(nombre);
            this.errores = new List<string>();
            this.tabSim = new Dictionary<string, long>();
        }

        #region Gets & Sets
        public string Nombre
        {
            get { return this.nombre; }
        }
        public string[] CodOp
        {
            get{ return this.codOp; }
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
        public Dictionary<string, long> TabSim
        {
            get { return this.tabSim; }
        }
        #endregion


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
            if(!errorListener.ExisteError)
            {
                try
                {
                    if (!codigo[1].Equals("START") && !codigo[1].Equals("END"))
                    {
                        if (codigo.Length == 3)
                        {
                            dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], codigo[2]);
                        }
                        else if (codigo.Length == 2)
                        {
                            dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], "");
                        }
                        if (!string.IsNullOrEmpty(codigo[0]))
                        {
                            this.TabSim.Add(codigo[0], this.cp);
                        }
                        if (this.codOp.Contains(codigo[1]) || codigo[1].Equals("WORD"))
                        {
                            this.cp += 3;
                        }
                        else if (this.codOp.Contains(codigo[1]))
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
                    else if (codigo[1].Equals("START"))
                    {
                        codigo[2] = codigo[2].ToUpper();
                        if (codigo[2].Contains("H"))
                        {
                            this.cp = MetodosAuxiliares.convierteDecimal(codigo[2].Replace("H", ""));
                            if (codigo.Length == 3)
                            {
                                dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], codigo[2]);
                            }
                            else if (codigo.Length == 2)
                            {
                                dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], "");
                            }
                        }
                        else
                        {
                            this.cp = long.Parse(codigo[2]);
                            if (codigo.Length == 3)
                            {
                                dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], codigo[2]);
                            }
                            else if (codigo.Length == 2)
                            {
                                dataGridViewIntermedio.Rows.Add(i + 1, MetodosAuxiliares.convierteHexadecimal(this.cp), codigo[0], codigo[1], "");
                            }
                        }
                    }
                }
                catch (ArgumentException)
                {
                    this.errores.Add("Linea" + (i + 1).ToString() + ": Error Simbolo repetido");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Archivo[i]))
                {
                    if (this.codOp.Contains(codigo[1]) || this.Directivas.Contains(codigo[1]) || codigo[1].Equals("WORD"))
                    {
                        if(this.codOp.Contains(codigo[0]) || this.Directivas.Contains(codigo[0]) || codigo[0].Equals("WORD"))
                        {
                            this.errores.Add("Linea" + (i + 1).ToString() + ": Error de sintaxis la etiqueta no puede ser la palabra reservada \"" + codigo[0] + "\"");
                        }
                        else
                        {
                            this.errores.Add("Linea" + (i + 1).ToString() + ": Error de sintaxis el operando: \"" + codigo[2]+ "\" Esta mal escrito");
                        }
                    }
                    else
                    {
                        this.errores.Add("Linea" + (i + 1).ToString() + ": Error instruccion \"" + codigo[1]+ "\" no existe");
                    }
                }
                else
                {
                    this.errores.Add("Linea" + (i + 1).ToString() + ": Error de sintaxis no debe haber lineas vacias");
                }
            }
        }
    }
}
