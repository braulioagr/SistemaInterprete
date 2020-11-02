using Practica6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica8
{
    public partial class Ejecucion : Form
    {
        #region Variables de instancia
        SIC sicEstandar;
        #endregion
        public Ejecucion()
        {
            InitializeComponent();
        }

        private void toolStripMapa_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "Abrir":
                    if (openFileObj.ShowDialog().Equals(DialogResult.OK))
                    {
                        sicEstandar = new SIC(openFileObj.FileName);
                        this.llenaDataGrid();
                    }
                    break;
            }
        }
        private void limpiaDatagrid()
        {
            this.dataGridViewMapaDeMemoria.Rows.Clear();
        }
        private void llenaDataGrid()
        {
            this.dataGridViewMapaDeMemoria.Rows.Clear();
            string[] fila;
            fila = new string[17];
            for (int i = 0; i < sicEstandar.Memoria.Filas; i++)
            {
                fila[0] = MetodosAuxiliares.decimalAHexadecimal(MetodosAuxiliares.hexadecimalADecimal(sicEstandar.Memoria.Inicio) + (i * 16));
                for (int j = 0; j < sicEstandar.Memoria.Columnas; j++)
                {
                    fila[j + 1] = sicEstandar.Memoria.Mapa[i, j];
                }
                this.dataGridViewMapaDeMemoria.Rows.Add(fila);
            }
        }
    }
}
