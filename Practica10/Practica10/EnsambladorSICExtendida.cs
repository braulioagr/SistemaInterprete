using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica10
{
    public class EnsambladorSICExtendida : Ensamblador
    {
        #region Variables de instancia
        private SICExtendidaLexer lexer;
        private CommonTokenStream tokens;
        private SICExtendidaParser parser;
        private Dictionary<string, string> instruccionesFormato1;
        private Dictionary<string, string> instruccionesFormato2;
        private Dictionary<string, string> instruccionesFormato3;
        private Dictionary<string, string> instruccionesFormato4;
        #endregion

        #region Construcciones

        public EnsambladorSICExtendida(string nombre) : base(nombre)
        {
            this.directiva = new string[]
            {
                "BYTE",
                "RESW",
                "RESB",
                "WORD",
                "BASE"
            };
            this.instruccionesFormato1 = new Dictionary<string, string>();
            this.instruccionesFormato2 = new Dictionary<string, string>();
            this.instruccionesFormato3 = new Dictionary<string, string>();
            this.instruccionesFormato4 = new Dictionary<string, string>();
            //Formato1
            this.instruccionesFormato1.Add("FIX", "C4");
            this.instruccionesFormato1.Add("FLOAT", "C0");
            this.instruccionesFormato1.Add("HIO", "F4");
            this.instruccionesFormato1.Add("NORM", "C8");
            this.instruccionesFormato1.Add("SIO", "F0");
            this.instruccionesFormato1.Add("TIO", "F8");
            //Formato2
            this.instruccionesFormato2.Add("ADDR", "90");
            this.instruccionesFormato2.Add("CLEAR", "B4");
            this.instruccionesFormato2.Add("COMPR", "A0");
            this.instruccionesFormato2.Add("DIVR", "9C");
            this.instruccionesFormato2.Add("MULR", "98");
            this.instruccionesFormato2.Add("RMO", "AC");
            this.instruccionesFormato2.Add("SHIFTL", "A4");
            this.instruccionesFormato2.Add("SHIFTR", "A8");
            this.instruccionesFormato2.Add("SUBR", "94");
            this.instruccionesFormato2.Add("SVC", "B0");
            this.instruccionesFormato2.Add("TIXR", "B8");
            //Formato3/4
            this.instruccionesFormato3.Add("ADD", "18");
            this.instruccionesFormato3.Add("ADDF", "58");
            this.instruccionesFormato3.Add("AND", "40");
            this.instruccionesFormato3.Add("COMP", "28");
            this.instruccionesFormato3.Add("COMPF", "88");
            this.instruccionesFormato3.Add("DIV", "24");
            this.instruccionesFormato3.Add("DIVF", "64");
            this.instruccionesFormato3.Add("J", "3C");
            this.instruccionesFormato3.Add("JEQ", "30");
            this.instruccionesFormato3.Add("JGT", "34");
            this.instruccionesFormato3.Add("JLT", "38");
            this.instruccionesFormato3.Add("JSUB", "48");
            this.instruccionesFormato3.Add("LDA", "00");
            this.instruccionesFormato3.Add("LDB", "68");
            this.instruccionesFormato3.Add("LDCH", "50");
            this.instruccionesFormato3.Add("LDF", "70");
            this.instruccionesFormato3.Add("LDL", "08");
            this.instruccionesFormato3.Add("LDS", "6C");
            this.instruccionesFormato3.Add("LDT", "74");
            this.instruccionesFormato3.Add("LDX", "04");
            this.instruccionesFormato3.Add("LPS", "D0");
            this.instruccionesFormato3.Add("MUL", "20");
            this.instruccionesFormato3.Add("MULF", "60");
            this.instruccionesFormato3.Add("OR", "44");
            this.instruccionesFormato3.Add("RD", "D8");
            this.instruccionesFormato3.Add("RSUB", "4C");
            this.instruccionesFormato3.Add("SSK", "EC");
            this.instruccionesFormato3.Add("STA", "0C");
            this.instruccionesFormato3.Add("STB", "78");
            this.instruccionesFormato3.Add("STCH", "54");
            this.instruccionesFormato3.Add("STF", "80");
            this.instruccionesFormato3.Add("STI", "D4");
            this.instruccionesFormato3.Add("STL", "14");
            this.instruccionesFormato3.Add("STS", "7C");
            this.instruccionesFormato3.Add("STSW", "E8");
            this.instruccionesFormato3.Add("STT", "84");
            this.instruccionesFormato3.Add("STX", "10");
            this.instruccionesFormato3.Add("SUB", "1C");
            this.instruccionesFormato3.Add("SUBF", "5C");
            this.instruccionesFormato3.Add("TD", "E0");
            this.instruccionesFormato3.Add("TIX", "2C");
            this.instruccionesFormato3.Add("WD", "DC");
            //
            //Formato3/4
            this.instruccionesFormato4.Add("+ADD", "18");
            this.instruccionesFormato4.Add("+ADDF", "58");
            this.instruccionesFormato4.Add("+AND", "40");
            this.instruccionesFormato4.Add("+COMP", "28");
            this.instruccionesFormato4.Add("+COMPF", "88");
            this.instruccionesFormato4.Add("+DIV", "24");
            this.instruccionesFormato4.Add("+DIVF", "64");
            this.instruccionesFormato4.Add("+J", "3C");
            this.instruccionesFormato4.Add("+JEQ", "30");
            this.instruccionesFormato4.Add("+JGT", "34");
            this.instruccionesFormato4.Add("+JLT", "38");
            this.instruccionesFormato4.Add("+JSUB", "48");
            this.instruccionesFormato4.Add("+LDA", "00");
            this.instruccionesFormato4.Add("+LDB", "68");
            this.instruccionesFormato4.Add("+LDCH", "50");
            this.instruccionesFormato4.Add("+LDF", "70");
            this.instruccionesFormato4.Add("+LDL", "08");
            this.instruccionesFormato4.Add("+LDS", "6C");
            this.instruccionesFormato4.Add("+LDT", "74");
            this.instruccionesFormato4.Add("+LDX", "04");
            this.instruccionesFormato4.Add("+LPS", "D0");
            this.instruccionesFormato4.Add("+MUL", "20");
            this.instruccionesFormato4.Add("+MULF", "60");
            this.instruccionesFormato4.Add("+OR", "44");
            this.instruccionesFormato4.Add("+RD", "D8");
            this.instruccionesFormato4.Add("+RSUB", "4C");
            this.instruccionesFormato4.Add("+SSK", "EC");
            this.instruccionesFormato4.Add("+STA", "0C");
            this.instruccionesFormato4.Add("+STB", "78");
            this.instruccionesFormato4.Add("+STCH", "54");
            this.instruccionesFormato4.Add("+STF", "80");
            this.instruccionesFormato4.Add("+STI", "D4");
            this.instruccionesFormato4.Add("+STL", "14");
            this.instruccionesFormato4.Add("+STS", "7C");
            this.instruccionesFormato4.Add("+STSW", "E8");
            this.instruccionesFormato4.Add("+STT", "84");
            this.instruccionesFormato4.Add("+STX", "10");
            this.instruccionesFormato4.Add("+SUB", "1C");
            this.instruccionesFormato4.Add("+SUBF", "5C");
            this.instruccionesFormato4.Add("+TD", "E0");
            this.instruccionesFormato4.Add("+TIX", "2C");
            this.instruccionesFormato4.Add("+WD", "DC");
        }

        #endregion

        #region Metodos

        #region Paso1
        public override void paso1(DataGridView dataGridViewIntermedio)
        {
            string[] codigo;
            MyErrorListener errorListener;
            this.lineasError = new List<bool>();
            this.errores = new List<string>();
            this.intermedio = new List<string>();
            this.tabSim = new Dictionary<string, long>();
            for (int i = 0; i < this.archivo.Count; i++)
            {
                lexer = new SICExtendidaLexer(new AntlrInputStream(this.Archivo[i]));
                tokens = new CommonTokenStream(lexer);
                parser = new SICExtendidaParser(tokens);
                errorListener = new MyErrorListener(i + 1);
                parser.AddErrorListener(errorListener);
                parser.prog();
                codigo = this.Archivo[i].Split('\t');
                this.lineasError.Add(errorListener.ExisteError);
                if(!errorListener.ExisteError)
                {
                    try
                    {
                        if (!codigo[1].Equals("START") & !codigo[1].Equals("END") & !codigo[1].Equals("BASE"))
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
                        else if (codigo[1].Equals("END")| codigo[1].Equals("BASE"))
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
                        this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Simbolo");
                        this.incrementaInstruccionDirectiva(codigo);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.Archivo[i]))
                    {
                        if (this.instruccionesFormato1.Keys.Contains(codigo[1]) ||
                            this.instruccionesFormato2.Keys.Contains(codigo[1]) ||
                            this.instruccionesFormato3.Keys.Contains(codigo[1]) ||
                            this.Directivas.Contains(codigo[1]))
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
                        this.errores.Add("Linea" + (i + 1).ToString() + ": Error de sintaxis no debe haber lineas vacias");
                        codigo = new string[] { "\t", "\t", "\t" };
                        this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "Vacia");
                    }
                }
            }
            this.paso1Logrado = true;
        }

        protected override void incrementaInstruccionDirectiva(string[] codigo)
        { 
            if (this.instruccionesFormato4.Keys.Contains(codigo[1]))
            {
                this.cp += 4;
            }
            else if (this.instruccionesFormato3.Keys.Contains(codigo[1]) || codigo[1].Equals("WORD"))
            {
                this.cp += 3;
            }
            else if (this.instruccionesFormato2.Keys.Contains(codigo[1]))
            {
                this.cp += 2;
            }
            else if(this.instruccionesFormato1.Keys.Contains(codigo[1]))
            {
                this.cp++;
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
                        codigo[2] = codigo[2].ToUpper();
                        if (codigo[2].Contains("H"))
                        {
                            cp += 3 * MetodosAuxiliares.hexadecimalADecimal(codigo[2].Replace("H", ""));
                        }
                        else
                        {
                            cp += 3 * long.Parse(codigo[2]);
                        }
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

        protected override void ensamblaIntermedio(DataGridView dataGridViewIntermedio, string[] codigo, int i, string error)
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
                this.intermedio.Add(MetodosAuxiliares.decimalAHexadecimal(this.cp) + "\t\t\t\t" + error);
            }
        }
        #endregion

        #region Paso 2
        public override void paso2(DataGridView dataGridViewIntermedio)
        {
            throw new NotImplementedException();
        }

        protected override string calculaDireccionEnd(string instruccion)
        {
            throw new NotImplementedException();
        }

        protected override void creaArchivoObj()
        {
            throw new NotImplementedException();
        }


        protected override void ensamblaIntermedio(DataGridView dataGridViewIntermedio)
        {
            throw new NotImplementedException();
        }

        protected override void generaRegistrosT()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
