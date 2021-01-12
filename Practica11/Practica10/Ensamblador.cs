using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica10
{
    public abstract class Ensamblador
    {

        #region Variables de Instancia
        protected long cp;
        protected long tamaño;
        protected string nombre;
        protected bool paso1Logrado;
        protected string[] directiva;
        protected List<string> archivo;
        protected List<string> intermedio;
        protected List<string> errores;
        protected List<bool> lineasError;
        protected List<string> archivoObj;
        protected string[] codigoObj;
        protected Dictionary<string, long> tabSim;
        #endregion

        #region Constructores

        public Ensamblador(string nombre)
        {

            this.nombre = nombre;
            this.archivo = MetodosAuxiliares.leeArchivo(nombre);
            #region Agregamos Instrucciones
            this.paso1Logrado = false;
            #endregion
        }

        #endregion


        #region Gets & Sets

        public string Nombre
        {
            get { return this.nombre; }
        }


        public string[] Directivas
        {
            get { return this.directiva; }
        }

        public long CP
        {
            get { return this.cp; }
            set { this.cp = value; }
        }

        public List<string> Archivo
        {
            get { return this.archivo; }
        }

        public List<string> Errores
        {
            get { return this.errores; }
        }

        public List<string> Intermedio
        {
            get { return this.intermedio; }
        }

        public Dictionary<string, long> TabSim
        {
            get { return this.tabSim; }
        }

        public long Tamaño
        {
            get { return this.tamaño; }
            set { this.tamaño = value; }
        }


        public bool Paso1Logrado
        {
            get { return this.paso1Logrado; }
        }

        public List<string> ArchivoObj
        {
            get { return this.archivoObj; }
        }

        #endregion

        #region Metodos

        #region Paso 1

        public abstract void paso1(DataGridView dataGridViewIntermedio);

        protected abstract void incrementaInstruccionDirectiva(string[] codigo);

        protected abstract void ensamblaIntermedio(DataGridView dataGridViewIntermedio, string[] codigo, int i, string error);

        #endregion

        #region Paso 2

        public abstract void paso2(DataGridView dataGridViewIntermedio);

        protected abstract void ensamblaIntermedio(DataGridView dataGridViewIntermedio);


        protected abstract void creaArchivoObj();

        protected abstract void generaRegistrosT();

        protected abstract string calculaDireccionEnd(string instruccion);

        #endregion

        #endregion

    }
}
