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
        private string[,] mapa;
        private long filas;
        private const int columnas = 16;
        #endregion

        #region Constructores
        public Memoria(long filas)
        {
            this.filas = filas;
            this.mapa = new string[filas, columnas];
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
        #endregion

    }
}
