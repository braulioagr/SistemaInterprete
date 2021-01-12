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
        private SIC sicEstandar;
        private string CP;
        private string A;
        private string X;
        private string L;
        private string CC;
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
                        CP = MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.CP));
                        A = MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.A);
                        X = MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.X);
                        L = MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.L);
                        this.richTextBox1.Text = "";
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
            this.dataGridViewRegistros.Rows.Add("CP", CP, MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.CP)));
            this.dataGridViewRegistros.Rows.Add("A", A, MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.A)));
            this.dataGridViewRegistros.Rows.Add("X", X, MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.X)));
            this.dataGridViewRegistros.Rows.Add("L", L, MetodosAuxiliares.ajustaDireccion(MetodosAuxiliares.decimalAHexadecimal(this.sicEstandar.L)));
            this.dataGridViewRegistros.Rows.Add("CC", this.sicEstandar.CC, "");
            this.label1.Text = "Tamaño del programa:" + this.sicEstandar.Memoria.Tamaño;
        }

        private void Ejecucion_Load(object sender, EventArgs e)
        {

        }

        private void Ejecucion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F10)
            {
                this.button1_Click(null,null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sicEstandar != null)
            {
                for (int i = 0; i < this.numericUpDown1.Value; i++)
                {
                    this.richTextBox1.ScrollToCaret();
                    this.richTextBox1.Text += this.sicEstandar.ejecutaPaso() +"\n";
                    this.richTextBox1.Text += "___________________________" + "\n";
                    this.llenaDataGrid();
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }
    }
}
