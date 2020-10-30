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
        private List<string> registros;
        private Memoria memoria;
        private string inicio;
        private string final;
        #endregion
        #region Constructores
        public SIC(string nombre)
        {
            long columnas;
            this.nombre = nombre;
            registros = MetodosAuxiliares.leeArchivo(nombre);
            inicio = registros.First().Substring(7, 6);
            final = MetodosAuxiliares.decimalAHexadecimal(MetodosAuxiliares.hexadecimalADecimal(inicio) + MetodosAuxiliares.hexadecimalADecimal(registros.First().Substring(13, 6)));
            columnas = MetodosAuxiliares.calculaTamañoMemoria(registros.First());
            this.memoria = new Memoria(columnas);
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
