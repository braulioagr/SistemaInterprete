using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica10
{
    public class Memoria
    {
        #region Variables de instancia
        private const int columnas = 16;
        private long filas;
        private string inicio;
        private string tamaño;
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
            tamaño = registros.First().Substring(13, 6);
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

        public string Tamaño
        {
            get { return this.tamaño; }
        }
        #endregion

        #region Metodos


        public long vaciaRegistrosT()
        {
            long cp;
            cp = 0;
            foreach(string registro in this.registros)
            {
                switch(registro.First())
                {
                    case 'E':
                        cp = MetodosAuxiliares.hexadecimalADecimal(registro.Substring(1));
                    break;
                    case 'T':
                        this.cargaRegistroT(registro);
                    break;
                }
            }
            return cp;
        }

        private void cargaRegistroT(string registro)
        {
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
                filaInicio = MetodosAuxiliares.CalculaFilaMemoria(new string(aux), inicio, filas);
                columnaInicio = MetodosAuxiliares.hexadecimalADecimal(direccioInicio.Last().ToString());
            }
            else
            {
                filaInicio = MetodosAuxiliares.CalculaFilaMemoria(direccioInicio, inicio, filas);
                columnaInicio = 0;
            }
            for (int i = 0; i < 16; i++)
            {
                if (bytesVaciados == bytes | columnaInicio + i == 16)
                {
                    break;
                }
                this.mapa[filaInicio, ((int)(columnaInicio)) + i] = contenido[(i * 2)].ToString() + contenido[(i * 2) + 1].ToString();
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
                    this.mapa[filaInicio, i] = bite;
                    bytesVaciados++;
                    if (bytesVaciados == bytes)
                    {
                        break;
                    }
                }
            }
        }


        public string ObtenCodOp(ref long cp)
        {
            int i;
            int j;
            string codOp;
            string cpHex;
            cpHex = MetodosAuxiliares.decimalAHexadecimal(cp);
            i = (int)this.ObtenFila(cpHex);
            j = (int)MetodosAuxiliares.hexadecimalADecimal(cpHex.Last().ToString());
            codOp = this.mapa[i, j];
            cp++;
            return codOp;

        }
        
        public string ObtenM(ref long cp)
        {
            int i;
            int j;
            string m;
            string cpHex;
            cpHex = MetodosAuxiliares.decimalAHexadecimal(cp);
            i = (int)this.ObtenFila(cpHex);
            j = (int)MetodosAuxiliares.hexadecimalADecimal(cpHex.Last().ToString());
            m = this.mapa[i, j];
            j++;
            if (j > 16)
            {
                i++;
                j = 0;
            }
            m += this.mapa[i, j];
            cp += 2;
            return m;
        }
        

        public long ObtenFila(string direccion)
        {
            char[] aux;
            aux = direccion.ToCharArray();
            aux[aux.Length - 1] = '0';
            return (MetodosAuxiliares.hexadecimalADecimal(new string(aux))- MetodosAuxiliares.hexadecimalADecimal(this.inicio))/16;
        }

        public void almacenaDatos(string[] operando, int v, string m)
        {
            int i;
            int j;
            long decM;
            for (int k = 0; k < v; k++)
            {
                i = (int)this.ObtenFila(m);
                j = (int)MetodosAuxiliares.hexadecimalADecimal(m.Last().ToString());
                this.mapa[i, j] = operando[k];
                decM = MetodosAuxiliares.hexadecimalADecimal(m)+1;
                m = MetodosAuxiliares.decimalAHexadecimal(decM);
            }
            
        }

        public string BuscaDireccion3(string m)
        {
            long cp = MetodosAuxiliares.hexadecimalADecimal(m);
            string Valor = "";
            int i = (int)ObtenFila(m);
            int j = (int)MetodosAuxiliares.hexadecimalADecimal(m.Last().ToString());

            Valor = this.mapa[i, j];
            j++;
            if (j > 16)
            {
                i++;
                j = 0;
            }
            Valor += this.mapa[i, j];
            j++;
            if (j > 16)
            {
                i++;
                j = 0;
            }
            j++;
            Valor += this.mapa[i, j];

            return Valor;
        }

        internal string BuscaFinal(string m)
        {
            long cp = MetodosAuxiliares.hexadecimalADecimal(m);
            string Valor = "";
            int i = (int)ObtenFila(m);
            int j = (int)MetodosAuxiliares.hexadecimalADecimal(m.Last().ToString());
            Valor = this.mapa[i, j];

            return Valor;
        }

        #endregion


    }
}
