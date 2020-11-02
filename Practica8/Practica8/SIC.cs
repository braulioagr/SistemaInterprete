using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica6;

namespace Practica8
{
    class SIC
    {
        #region Variables de instancia
        private string nombre;
        private Memoria memoria;
        private long cp;
        #endregion
        #region Constructores
        public SIC(string nombre)
        {
            this.nombre = nombre;
            this.memoria = new Memoria(this.nombre);
            cp = this.memoria.vaciaRegistrosT();
        }
        #endregion

        #region Gets & Sets
        public Memoria Memoria
        {
            get { return this.memoria; }
        }
        #endregion
    }
}
