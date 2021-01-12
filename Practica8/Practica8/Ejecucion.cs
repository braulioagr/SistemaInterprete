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
            this.dataGridViewRegistros.Rows.Clear();
        }
        private void llenaDataGrid()
        {
            this.limpiaDatagrid();
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
            this.dataGridViewRegistros.Rows.Add("CP", MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.CP)), "");
            this.dataGridViewRegistros.Rows.Add("A", MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.A), "");
            this.dataGridViewRegistros.Rows.Add("X", MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.X), "");
            this.dataGridViewRegistros.Rows.Add("L", MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.L), "");
            this.dataGridViewRegistros.Rows.Add("CC", this.sicEstandar.CC, "");
            this.label1.Text = "Tamaño del programa:" + this.sicEstandar.Memoria.Tamaño;
        }

        private void Ejecucion_Load(object sender, EventArgs e)
        {

        }

    }
}
