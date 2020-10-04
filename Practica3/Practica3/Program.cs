using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int id;
                string nombre;
                List<string> archivo;
                SICEstandarLexer lexer;
                CommonTokenStream tokens;
                SICEstandarParser parser;
                StreamWriter writer;
                TextWriter outPutError;
                TextWriter outPut;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Nombre del archivo:");
                    nombre = Console.ReadLine();
                    if (nombre.Contains(".s"))
                    {
                        nombre = nombre.Replace(".s","");
                    }
                    archivo = MetodosAuxiliares.leeArchivo(nombre);
                    outPutError = Console.Error;
                    outPut = Console.Out;
                    id = 0;
                    using (writer = new StreamWriter(new FileStream(Directory.GetCurrentDirectory() + @"\" + "tmp.s", FileMode.Create)))
                    {
                        foreach (string linea in archivo)
                        {
                            Console.SetOut(writer);
                            Console.Write("Linea " + id + ": ");
                            lexer = new SICEstandarLexer(new AntlrInputStream(linea));
                            tokens = new CommonTokenStream(lexer);
                            parser = new SICEstandarParser(tokens);
                            parser.prog();
                            Console.SetError(writer);
                            Console.Write("\n");
                            Console.SetOut(outPutError);
                            id++;
                        }
                    }
                    Console.SetOut(outPut);
                    MetodosAuxiliares.generaSalida(nombre);
                    Console.WriteLine("Desea salir? Y/N");
                    nombre = Console.ReadLine();
                    nombre = nombre.ToUpper();
                    if (nombre.Equals("Y"))
                    {
                        break;
                    }
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Ese archivo no existe");
            }
        }
    }
}
