using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica3
{
    class MetodosAuxiliares
    {
        public static List<string> leeArchivo(string nombre)
        {
            string linea;
            List<string> archivo;
            StreamReader reader;
            linea = "";
            archivo = new List<string>();
            nombre += ".s";
            reader = new StreamReader(Directory.GetCurrentDirectory() + @"\" + nombre);
            while (linea != null)
            {
                linea = reader.ReadLine();
                if (linea != null)
                {
                    archivo.Add(linea);
                }
            }
            reader.Close();
            return archivo;
        }

        public static void generaSalida(string nombre)
        {
            try
            {
                List<string> archivo;
                StreamWriter writer;
                archivo = MetodosAuxiliares.leeArchivo("tmp");
                using (writer = writer = new StreamWriter(new FileStream(Directory.GetCurrentDirectory() + @"\" + "Salida_" + nombre + ".txt", FileMode.Create)))
                {
                    foreach (string linea in archivo)
                    {
                        if (linea.Length > 11)
                        {
                            writer.WriteLine(linea);
                        }
                    }
                }
                File.Delete(Directory.GetCurrentDirectory() + @"\tmp.s");
            }
            catch(FileNotFoundException)
            {
                Console.Write("Error");
            }
        }
    }
}
