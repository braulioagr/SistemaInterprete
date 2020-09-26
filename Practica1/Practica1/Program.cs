using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "";
            Combined1Lexer lex;
            CommonTokenStream tokens;
            Combined1Parser parser;
            //VARIABLE PARA ALMACENAR LA CADENA DE ENTRADA
            while (true)
            {
                try
                {
                    line = Console.ReadLine();
                    //SE ALMACENA LA CADENA DE ENTRADA
                    if (line.Contains("EXIT") || line.Contains("exit"))
                    {
                        //SI DETECTA EXIT SALE DEL PROGRAMA
                        break;
                    }
                    lex = new Combined1Lexer(new AntlrInputStream(line + Environment.NewLine));
                    //CREAMOS UN LEXER CON LA CADENA QUE ESCRIBIO EL USUARIO
                    tokens = new CommonTokenStream(lex);
                    //CREAMOS LOS TOKENS SEGUN EL LEXER CREADO
                    parser = new Combined1Parser(tokens);
                    //CREAMOS EL PARSER CON LOS TOKENS CREADOS
                    parser.expresion();
                    //SE VERIFICA QUE EL ANALIZADOR EMPIECE CON LA EXPRESION
                }
                catch (RecognitionException e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Usted no puede hacer semejante barbaridad!!!");
                }
            }
        }
    }
}
