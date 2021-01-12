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
        private long a;
        private long x;
        private long l;
        private string cc;
        private string nombre;
        private Memoria memoria;
        private long cp;
        #endregion

        #region Constructores

        public SIC(string nombre)
        {
            this.a = 16777215;
            this.x = 16777215;
            this.l = 16777215;
            this.cc = "null";
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

        public long CP
        {
            get { return this.cp; }
        }
        
        public long A
        {
            get { return this.a; }
        }
        
        public long X
        {
            get { return this.x; }
        }
        
        public long L
        {
            get { return this.l; }
        }
        
        public string CC
        {
            get { return this.cc; }
        }

        #endregion



    }
}
