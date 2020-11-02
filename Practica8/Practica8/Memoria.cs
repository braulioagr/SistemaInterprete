using Practica6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica8
{
    public class Memoria
    {

        #region Variables de instancia
        private const int columnas = 16;
        private long filas;
        private string inicio;
        private string final;
        private string[,] mapa;
        private List<string> registros;
        #endregion

        #region Constructores
        public Memoria(string nombre)
        {
            this.registros = MetodosAuxiliares.leeArchivo(nombre);
            this.filas = MetodosAuxiliares.calculaTamañoMemoria(registros.First());
            inicio = registros.First().Substring(7, 6);
            this.mapa = new string[filas, columnas];
            final = MetodosAuxiliares.decimalAHexadecimal(MetodosAuxiliares.hexadecimalADecimal(inicio) +
                                                          MetodosAuxiliares.hexadecimalADecimal(registros.First().Substring(13, 6)));
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    mapa[i, j] = "FF";
                }
            }
        }
        #endregion

        #region Gets & Sets
        public string[,] Mapa
        {
            get { return this.mapa; }
        }

        public long Filas
        {
            get { return this.filas; }
        }

        public int Columnas
        {
            get { return columnas; }
        }

        public string Inicio
        {
            get { return this.inicio; }
        }
        #endregion

        #region Metodos


        public long vaciaRegistrosT()
        {
            long cp;
            string registroE;
            registroE = this.registros.Last();
            cp = 0;
            foreach(string registro in this.registros)
            {
                switch (registro.First())
                {
                    case 'E':
                        cp = MetodosAuxiliares.hexadecimalADecimal(registro.Substring(1));
                    break;
                    case 'T':
                        int bytesVaciados;
                        long filaInicio;
                        long columnaInicio;
                        long bytes;
                        string direccioInicio;
                        string contenido;
                        bytesVaciados = 0;
                        direccioInicio = registro.Substring(1, 6);
                        bytes = MetodosAuxiliares.hexadecimalADecimal(registro.Substring(7, 2));
                        contenido = registro.Substring(9);
                        if (!direccioInicio.Last().Equals('0'))
                        {
                            char[] aux;
                            aux = direccioInicio.ToCharArray();
                            aux[aux.Length - 1] = '0';
                            filaInicio =MetodosAuxiliares.CalculaFilaMemoria(new string(aux),inicio,filas);
                            columnaInicio = MetodosAuxiliares.hexadecimalADecimal(direccioInicio.Last().ToString());
                        }
                        else
                        {
                            filaInicio = MetodosAuxiliares.CalculaFilaMemoria(direccioInicio, inicio, filas);
                            columnaInicio = 0;
                        }
                        if (filaInicio == -1)
                        {
                            filaInicio = Filas - 1;
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            if (bytesVaciados == bytes | ((int)columnaInicio) + i ==16)
                            {
                                break;
                            }
                            this.mapa[filaInicio, ((int)(columnaInicio))+i] = contenido[(i * 2)].ToString() + contenido[(i * 2) + 1].ToString();
                            bytesVaciados++;
                        }
                        while (bytesVaciados != bytes)
                        {
                            filaInicio++;
                            int desplazamiento;
                            desplazamiento = bytesVaciados;
                            for (int i = 0; i < 16; i++)
                            {
                                string bite;
                                bite = contenido[(desplazamiento * 2) + (i * 2)].ToString();
                                bite += contenido[(desplazamiento * 2) + (i * 2) + 1].ToString();
                                this.mapa[filaInicio, i] =  bite;
                                bytesVaciados++;
                                if(bytesVaciados == bytes)
                                {
                                    break;
                                }
                            }
                        }
                    break;
                }
            }
            return cp;
        }
        #endregion


    }
}
