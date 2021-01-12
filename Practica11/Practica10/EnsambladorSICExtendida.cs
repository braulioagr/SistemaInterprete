using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Practica10
{
    public class EnsambladorSICExtendida : Ensamblador
    {

        #region Variables de instancia
        private long b;
        private string bAux;
        private string nombreProg;
        private List<string> relocalizables;
        private SICExtendidaLexer lexer;
        private CommonTokenStream tokens;
        private SICExtendidaParser parser;
        private Dictionary<string, string> instruccionesFormato1;
        private Dictionary<string, string> instruccionesFormato2;
        private Dictionary<string, string> instruccionesFormato3;
        private Dictionary<string, string> instruccionesFormato4;
        private Dictionary<string, string> registrosEspeciales;
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
            this.registrosEspeciales = new Dictionary<string, string>();
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
            //Registros especiales
            this.registrosEspeciales.Add("B", "3");
            this.registrosEspeciales.Add("S", "4");
            this.registrosEspeciales.Add("T", "5");
            this.registrosEspeciales.Add("F", "6");
            this.registrosEspeciales.Add("A", "0");
            this.registrosEspeciales.Add("X", "1");
            this.registrosEspeciales.Add("L", "2");
            this.registrosEspeciales.Add("CP", "8");
            this.registrosEspeciales.Add("SW", "9");
            this.b = -1;
            this.bAux = "null";
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
                parser = new 
                    
                    SICExtendidaParser(tokens);
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
                        else if (codigo[1].Equals("END") | codigo[1].Equals("BASE"))
                        {
                            this.ensamblaIntermedio(dataGridViewIntermedio, codigo, i, "no");
                            if (codigo[1].Equals("BASE"))
                            {
                                this.bAux = codigo[2];
                            }
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
                            this.instruccionesFormato4.Keys.Contains(codigo[1]) ||
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
            bool x;
            int resto;
            string aux;
            string[] linea;
            string[] lineaSig;
            this.asignaBase();
            this.codigoObj = new string[this.intermedio.Count];
            for (int i = 0; i < this.intermedio.Count; i++)
            {
                linea = this.intermedio[i].Split('\t');
                if (!string.IsNullOrEmpty(linea[2]))
                {
                    if (!this.lineasError[i])
                    {
                        if (this.instruccionesFormato1.Keys.Contains(linea[2]))
                        {
                            this.codigoObj[i] = this.instruccionesFormato1[linea[2]];
                        }
                        else if(this.instruccionesFormato2.Keys.Contains(linea[2]))
                        {
                            this.codigoObj[i] = this.ensamblaInstruccion2(linea[2], linea[3]);
                        }
                        else if (this.instruccionesFormato3.Keys.Contains(linea[2]))
                        {
                            lineaSig = this.intermedio[i+1].Split('\t');
                            this.codigoObj[i] = this.ensamblaInstruccion3(linea[2], linea[3], lineaSig[0]);
                        }
                        else if (this.instruccionesFormato4.Keys.Contains(linea[2]))
                        {
                            this.codigoObj[i] = this.ensamblaInstruccion4(linea[2], linea[3]);
                        }
                        else
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
                                        if (codigoObj[i].Length % 2 != 0)
                                        {
                                            codigoObj[i] = "0" + codigoObj[i];
                                        }
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
                                        for (int j = 0; j < resto; j++)
                                        {
                                            aux += "0";
                                        }
                                        codigoObj[i] = aux + codigoObj[i];
                                    }
                                    else if (codigoObj[i].Length > 6)
                                    {
                                        codigoObj[i] = codigoObj[i].Substring(0, 6);
                                    }
                                    #endregion
                                break;
                                default:
                                    this.codigoObj[i] = "---";
                                break;
                            }

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

        #region Instrucciones
        private string ensamblaInstruccion4(string codOp, string operando)
        {

            string codigoObj;
            char[] nixbpe;
            codigoObj = "";
            long dir;
            int resto;
            nixbpe = new char[6];
            codigoObj = MetodosAuxiliares.hexadecimalABinario(this.instruccionesFormato4[codOp]).Substring(0, 6);
            nixbpe[3] = '0';
            nixbpe[4] = '0';
            nixbpe[5] = '1';

            if (!codOp.Equals("RSUB"))
            {
                switch (operando.First())
                {
                    case '@'://Direccionamiento Indirecto
                        nixbpe[0] = '1';
                        nixbpe[1] = '0';
                        nixbpe[2] = '0';
                        operando = operando.Replace("@", "");
                    break;
                    case '#'://Direccionamiento Inmediato
                        nixbpe[0] = '0';
                        nixbpe[1] = '1';
                        nixbpe[2] = '0';
                        operando = operando.Replace("#", "");
                    break;
                    default://Direccionamiento Simple
                        nixbpe[0] = '1';
                        nixbpe[1] = '1';
                        if (operando.Contains(",X") | operando.Contains(",x"))
                        {
                            operando = operando.Replace(",X", "");
                            operando = operando.Replace(",x", "");
                            nixbpe[2] = '1';
                        }
                        else
                        {
                            nixbpe[2] = '0';
                        }
                        break;
                }
                if (tabSim.TryGetValue(operando, out dir))
                {
                    operando = MetodosAuxiliares.decimalAHexadecimal(dir);
                }
                else
                {
                    bool h;
                    h = operando.Last().Equals('H') | operando.Last().Equals('h');
                    if (h)
                    {
                        operando = operando.Replace("H", "");
                        operando = operando.Replace("h", "");
                    }
                    if (Information.IsNumeric(operando))
                    {
                        if (h)
                        {
                            operando = MetodosAuxiliares.hexadecimalADecimal(operando).ToString();
                        }
                        int c;
                        c = int.Parse(operando);
                        if (c > 4095)
                        {
                            operando = MetodosAuxiliares.decimalAHexadecimal(c);
                        }
                        else
                        {
                            operando = "FFFFF";
                        }
                    }
                    else
                    {
                        operando = "FFFFF";
                    }
                }
                if (MetodosAuxiliares.hexadecimalADecimal(operando) > 0)
                {
                    operando = MetodosAuxiliares.ajustaOperando(operando, 5, "0");
                }
                else
                {
                    operando = MetodosAuxiliares.ajustaOperando(operando, 5, "F");
                }
                codigoObj = MetodosAuxiliares.decimalAHexadecimal(MetodosAuxiliares.binarioADecimal(codigoObj + new string(nixbpe))) + operando;
                codigoObj += "*";
            }
            else
            {
                codigoObj = "4F100000*";
            }
            return codigoObj;
        }

        private string ensamblaInstruccion3(string codOp, string operando, string cpSig)
        {
            string codigoObj;
            char[] nixbpe;
            codigoObj = "";
            nixbpe = new char[6];
            codigoObj = MetodosAuxiliares.hexadecimalABinario(this.instruccionesFormato3[codOp]).Substring(4,2);
            nixbpe[5] = '0';
            if (!codOp.Equals("RSUB"))
            {
                codOp = this.instruccionesFormato3[codOp][0].ToString();
                switch (operando.First())
                {
                    case '@'://Direccionamiento Indirecto
                        nixbpe[0] = '1';
                        nixbpe[1] = '0';
                        nixbpe[2] = '0';
                        operando = operando.Replace("@","");
                    break;
                    case '#'://Direccionamiento Inmediato
                        nixbpe[0] = '0';
                        nixbpe[1] = '1';
                        nixbpe[2] = '0';
                        operando = operando.Replace("#", "");
                        break;
                    default://Direccionamiento Simple
                        nixbpe[0] = '1';
                        nixbpe[1] = '1';
                        if (operando.Contains(",X") | operando.Contains(",x"))
                        {
                            operando = operando.Replace(",X", "");
                            operando = operando.Replace(",x", "");
                            nixbpe[2] = '1';
                        }
                        else
                        {
                            nixbpe[2] = '0';
                        }
                        break;
                }
                if (this.tabSim.Keys.Contains(operando))
                {
                    this.determinaBanderas(ref nixbpe, ref operando, cpSig);
                }
                else
                {
                    bool h;
                    h = operando.Last().Equals('H') | operando.Last().Equals('h');
                    if (h)
                    {
                        operando = operando.Replace("H", "");
                        operando = operando.Replace("h", "");
                    }
                    if (Information.IsNumeric(operando))
                    {
                        if (h)
                        {
                            operando = MetodosAuxiliares.hexadecimalADecimal(operando).ToString();
                        }
                        int c;
                        c = int.Parse(operando);
                        operando = MetodosAuxiliares.decimalAHexadecimal(c);
                        if (0 <= c & c <= 4095)
                        {
                            nixbpe[3] = '0';
                            nixbpe[4] = '0';
                            operando = MetodosAuxiliares.ajustaOperando(operando, 3, "0");
                        }
                        else
                        {
                            this.determinaBanderas(ref nixbpe, ref operando, cpSig);
                        }
                    }
                    else
                    {
                        nixbpe[3] = '1';
                        nixbpe[4] = '1';
                        operando = "FFF";
                    }
                }
                codigoObj += new string(nixbpe);
                codigoObj = codOp + MetodosAuxiliares.decimalAHexadecimal(MetodosAuxiliares.binarioADecimal(codigoObj)) + operando;
            }
            else
            {
                codigoObj = "4F0000";
            }
            return codigoObj;
        }

        private void determinaBanderas(ref char[] nixbpe, ref string operando, string cpSig)
        {
            if (this.relativoContador(ref operando, cpSig))
            {
                nixbpe[3] = '0';
                nixbpe[4] = '1';

            }
            else if (this.relativoBase(ref operando))
            {
                nixbpe[3] = '1';
                nixbpe[4] = '0';
            }
            else
            {
                nixbpe[3] = '1';
                nixbpe[4] = '1';
                operando = "FFF";
            }
        }

        #region relatividades

        private bool relativoBase(ref string operando)
        {
            bool band;
            long desp;
            long dir;
            band = false;
            if (tabSim.TryGetValue(operando, out dir))
            {
                desp = dir - b;
                if (desp >= 0 & desp <= 4095)
                {
                    operando = MetodosAuxiliares.decimalAHexadecimal(desp);
                    operando = MetodosAuxiliares.ajustaOperando(operando, 3, "0");
                    band = true;
                }

            }
            return band;
        }

        private bool relativoContador(ref string operando, string cpSig)
        {
            bool band;
            long desp;
            long dir;
            band = false;
            if (tabSim.TryGetValue(operando, out dir))
            {
                desp = dir - MetodosAuxiliares.hexadecimalADecimal(cpSig);
                if (desp >= (-2048) & desp <= 2047)
                {
                    operando = MetodosAuxiliares.decimalAHexadecimal(desp);
                    if (desp < 0)
                    {
                        operando = MetodosAuxiliares.ajustaOperando(operando, 3, "F");
                    }
                    else
                    {
                        operando = MetodosAuxiliares.ajustaOperando(operando, 3, "0");
                    }
                    band = true;
                }
            }
            return band;

        }

        #endregion

        private string ensamblaInstruccion2(string codOp, string operando)
        {
            string codigoObj;
            string[] operandos;
            codigoObj = this.instruccionesFormato2[codOp];
            operandos = operando.Split(',');
            long n;
            if (operandos.Length == 2)
            {
                if (!(codOp.Equals("SHIFTL") | codOp.Equals("SHIFTR")))
                {
                    codigoObj += this.registrosEspeciales[operandos[0]] + this.registrosEspeciales[operandos[1]];
                }
                else //Otro caso especial, me lleva
                {
                    n = int.Parse(operandos[1]);
                    if (1 <= n & n <= 16)
                    {
                        codigoObj += this.registrosEspeciales[operandos[0]] + MetodosAuxiliares.decimalAHexadecimal(n - 1);
                    }
                    else
                    {
                        codigoObj += "FF";
                    }
                }
            }
            else//Casos especiales
            {
                if (!codOp.Equals("SVC"))
                {
                    codigoObj += this.registrosEspeciales[operandos[0]] + "0";
                }
                else
                {
                    n = int.Parse(operandos[1]);
                    if (1 <= n & n <= 16)
                    {
                        codigoObj += this.registrosEspeciales[operandos[0]] + MetodosAuxiliares.decimalAHexadecimal(n - 1);
                    }
                    else
                    {
                        codigoObj += "FF";
                    }
                }
            }
            return codigoObj;
        }
        
        #endregion

        private void asignaBase()
        {
            try
            {
                this.b = this.tabSim[this.bAux];
            }
            catch(KeyNotFoundException)
            {
                this.b = -1;
            }
        }

        protected override void ensamblaIntermedio(DataGridView dataGridViewIntermedio)
        {
            dataGridViewIntermedio.Rows.Clear();
            for (int i = 0; i < this.intermedio.Count; i++)
            {
                dataGridViewIntermedio.Rows.Add(((i + 1).ToString() + "\t" + this.intermedio[i] + "\t" + codigoObj[i]).Split('\t'));
            }
        }
        
        #region ArchivoObj
        protected override string calculaDireccionEnd(string instruccion)
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
                        if (this.instruccionesFormato1.Keys.Contains(aux[2]) || this.instruccionesFormato2.Keys.Contains(aux[2]) ||
                            this.instruccionesFormato3.Keys.Contains(aux[2]) || this.instruccionesFormato4.Keys.Contains(aux[2]))
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

        protected override void creaArchivoObj()
        {
            string linea;
            string[] instruccion;
            this.archivoObj = new List<string>();
            instruccion = this.intermedio.First().Split('\t');
            this.nombreProg = MetodosAuxiliares.ajustaCadena(instruccion[1], 6);
            linea = instruccion[3].Replace("H", "");
            linea = linea.Replace("h", "");
            linea = "H" + nombreProg + MetodosAuxiliares.ajustaDireccion(linea) +
                          MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.tamaño));
            this.archivoObj.Add(linea);
            this.relocalizables = new List<string>();
            this.generaRegistrosT();
            this.generaRegistrosM();
            instruccion = this.intermedio.Last().Split('\t');
            linea = "E" + this.calculaDireccionEnd(instruccion[3]);
            this.archivoObj.Add(linea);
        }

        private void generaRegistrosM()
        {
            foreach (string cp in this.relocalizables)
            {

                this.archivoObj.Add("M" + cp + "05+" + this.nombreProg);
            }
        }

        protected override void generaRegistrosT()
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
                for (j = i; j < this.codigoObj.Length - 1; j++)
                {
                    if (!this.codigoObj[j].Contains("Error"))
                    {
                        if (this.codigoObj[j].Contains("*"))
                        {
                            string cpHex;
                            cpHex = MetodosAuxiliares.decimalAHexadecimal(MetodosAuxiliares.hexadecimalADecimal(this.intermedio[j].Split('\t').First()) +1); 
                            this.relocalizables.Add(MetodosAuxiliares.ajustaOperando(cpHex,6,"0"));
                            this.codigoObj[j] = this.codigoObj[j].Replace("*", "");
                        }
                        if (((aux.Length + this.codigoObj[j].Length) / 2) > 30)
                        {
                            break;
                        }
                        else if (this.codigoObj[j].Equals("---") & !this.archivo[j].Contains("BASE"))
                        {
                            break;
                        }
                        else if(!this.archivo[j].Contains("BASE"))
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
                if (registroT.Length > 9)
                {
                    this.archivoObj.Add(registroT);
                }
                for (int k = j; k < this.codigoObj.Length; k++)
                {
                    if (!codigoObj[k].Equals("---") | this.intermedio[k].Contains("END"))
                    {
                        i = k;
                        break;
                    }
                }

            }
        }
        #endregion
        
        #endregion

        #endregion
    }
}
