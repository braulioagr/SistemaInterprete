using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica6
{
    public class MetodosAuxiliares
    {
        public const int limiteRegistroT = 30;

        #region archivos
        public static List<string> leeArchivo(string nombre)
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

        public static void grabaTabSim(string nombre, Dictionary<string, long> tabSim)
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
                    writer.WriteLine(simbolo.Key + "\t" + MetodosAuxiliares.decimalAHexadecimal(simbolo.Value));
                }
            }
        }

        public static long FinalPrograma(string inicio, string tamaño)
        {
            return MetodosAuxiliares.hexadecimalADecimal(inicio) + MetodosAuxiliares.hexadecimalADecimal(tamaño);
        }

        public static void grabaArchivoSIC(string nombre, List<string> archivo)
        {
            string ruta;
            StreamWriter writer;
            ruta = Path.GetDirectoryName(nombre);
            nombre = Path.GetFileNameWithoutExtension(nombre);
            using (writer = new StreamWriter(new FileStream(ruta + @"\" + nombre + ".s", FileMode.Create)))
            {
                foreach (string linea in archivo)
                {
                    writer.WriteLine(linea);
                }
            }
        }

        public static void grabaIntermedio(string nombre, List<string> intermedio)
        {
            string ruta;
            StreamWriter writer;
            ruta = Path.GetDirectoryName(nombre);
            nombre = Path.GetFileNameWithoutExtension(nombre);
            using (writer = new StreamWriter(new FileStream(ruta + @"\" + nombre + ".loc", FileMode.Create)))
            {
                writer.WriteLine("CP\tEtiqueta\tSimbolo\tDireccion");
                foreach (string linea in intermedio)
                {
                    writer.WriteLine(linea);
                }
            }
        }
        
        public static void grabaObj(string nombre, List<string> archivoObj)
        {
            string ruta;
            StreamWriter writer;
            ruta = Path.GetDirectoryName(nombre);
            nombre = Path.GetFileNameWithoutExtension(nombre);
            using (writer = new StreamWriter(new FileStream(ruta + @"\" + nombre + ".obj", FileMode.Create)))
            {
                foreach (string linea in archivoObj)
                {
                    writer.WriteLine(linea);
                }
            }
        }


        #endregion

        #region Conversiones

        public static string decimalAHexadecimal(long dec)
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

        public static long hexadecimalADecimal(string hex)
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
                string chex = decimalAHexadecimal(str[i]);

                if (chex.Length == 1)
                {
                    chex = chex.Insert(0, "0");
                }
                hex += chex;
            }

            return hex;
        }

        public static string decimalABinario(long dec)
        {
            string binStr;
            binStr = "1";
            if (dec > 1)
            {

                while (dec > 0)
                {
                    binStr = binStr.Insert(0, (dec % 2).ToString());
                    dec /= 2;
                }
            }
            return binStr;
        }

        public static long binarioADecimal(string bin)
        {
            int binLength = bin.Length;
            double dec = 0;

            for (int i = 0; i < binLength; ++i)
            {
                dec += ((byte)bin[i] - 48) * Math.Pow(2, ((binLength - i) - 1));
            }

            return (int)dec;
        }

        internal static string hexadecimalABinario(string hexadecimal)
        {
            string binario;
            binario = "";
            for (int i = 0; i < hexadecimal.Length; i++)
            {
                switch (hexadecimal[i])
                {
                    case '0':
                        binario += "0000";
                        break;
                    case '1':
                        binario += "0001";
                        break;
                    case '2':
                        binario += "0010";
                        break;
                    case '3':
                        binario += "0011";
                        break;
                    case '4':
                        binario += "0100";
                        break;
                    case '5':
                        binario += "0101";
                        break;
                    case '6':
                        binario += "0110";
                        break;
                    case '7':
                        binario += "0111";
                        break;
                    case '8':
                        binario += "1000";
                        break;
                    case '9':
                        binario += "1001";
                        break;
                    case 'A':
                        binario += "1010";
                        break;
                    case 'B':
                        binario += "1011";
                        break;
                    case 'C':
                        binario += "1100";
                        break;
                    case 'D':
                        binario += "1101";
                        break;
                    case 'E':
                        binario += "1110";
                        break;
                    case 'F':
                        binario += "1111";
                        break;
                }
            }
            return binario;
        }


        #endregion



        public static long calculaTamaño(string first, string last)
        {
            long inicio;
            long final;
            inicio = MetodosAuxiliares.hexadecimalADecimal(first.Split('\t').First());
            final = MetodosAuxiliares.hexadecimalADecimal(last.Split('\t').First());
            return final - inicio;
        }

        public static string ajustaCadena(string cadena, int tam)
        {
            if (cadena.Length < 6)
            {
                do
                {
                    cadena += " ";
                } while (cadena.Length < 6);
            }
            else if(cadena.Length > 6)
            {
                cadena = cadena.Substring(0, 6);
            }
            return cadena;
        }


        public static string ajustaDireccion(string direccion)
        {
            if (direccion.Length < 6)
            {
                string ceros;
                ceros = "";
                for (int i = 0; i < 6 - direccion.Length; i++)
                {
                    ceros += "0";
                }
                direccion = ceros + direccion;
            }
            return direccion;
        }


        public static long calculaTamañoMemoria(string v)
        {
            string tamaño;
            tamaño = v.Substring(13, 6);
            return (MetodosAuxiliares.hexadecimalADecimal(tamaño) / 16)+1;
        }

        public static long CalculaFilaMemoria(string direccioInicio, string inicio, long filas)
        {
            long indice;
            indice = -1;
            for (long i = 0; i < filas; i++)
            {
                if (MetodosAuxiliares.hexadecimalADecimal(direccioInicio) == (MetodosAuxiliares.hexadecimalADecimal(inicio) + (i * 16)))
                {
                    indice = i;
                    break;
                }
            }
            return indice;
        }
    }
}
