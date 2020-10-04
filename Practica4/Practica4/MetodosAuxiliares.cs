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
        #region archivos
        public static List<string>leeArchivo(string nombre)
        {
            string linea;
            List<string> archivo;
            StreamReader reader;
            linea = "";
            archivo = new List<string>();
            reader = new StreamReader(nombre);
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

        public static void grabaErrores(string nombre, List<string> errores)
        {
            string ruta;
            StreamWriter writer;
            ruta = Path.GetDirectoryName(nombre);
            nombre = Path.GetFileNameWithoutExtension(nombre);
            using (writer = new StreamWriter(new FileStream(ruta + @"\" + nombre + ".error", FileMode.Create)))
            {
                foreach (string error in errores)
                {
                    writer.WriteLine(error);
                }
            }
        }

        public static void grabaTabSim(string nombre, Dictionary<string,long> tabSim)
        {
            string ruta;
            StreamWriter writer;
            ruta = Path.GetDirectoryName(nombre);
            nombre = Path.GetFileNameWithoutExtension(nombre);
            using (writer = new StreamWriter(new FileStream(ruta + @"\" + nombre + ".tabSim", FileMode.Create)))
            {
                writer.WriteLine("Simbolo\tDireccion");
                foreach (var simbolo in tabSim)
                {
                    writer.WriteLine(simbolo.Key+"\t"+MetodosAuxiliares.convierteHexadecimal(simbolo.Value));
                }
            }
        }
        #endregion

        #region Conversiones

        public static string convierteHexadecimal(long dec)
        {
            string hexStr;
            long hex;
            hexStr = "0";
            hex = dec;
            if (dec > 1)
            {
                hexStr = string.Empty;
                while (dec > 0)
                {
                    hex = dec % 16;
                    if (hex < 10)
                    {
                        hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
                    }
                    else
                    {
                        hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());
                    }

                    dec /= 16;
                }
            }

            return hexStr;
        }

        public static long convierteDecimal(string hex)
        {
            hex = hex.ToUpper();
            double dec = 0;
            for (int i = 0; i < hex.Length; ++i)
            {
                byte b = (byte)hex[i];

                if (b >= 48 && b <= 57)
                {
                    b -= 48;
                }
                else if (b >= 65 && b <= 70)
                {
                    b -= 55;
                }
                dec += b * Math.Pow(16, ((hex.Length - i) - 1));
            }
            return (long)dec;
        }

        public static string ASCIIToHexadecimal(string str)
        {
            string hex = string.Empty;

            for (int i = 0; i < str.Length; ++i)
            {
                string chex = convierteHexadecimal(str[i]);

                if (chex.Length == 1)
                {
                    chex = chex.Insert(0, "0");
                }
                hex += chex;
            }

            return hex;
        }
        
        #endregion
    }
}
