using Antlr4.Runtime;
using Practica3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica4
{
    public partial class SistemaInterprete : Form
    {
        Ensamblador ensamblador;

        #region Constructores
        public SistemaInterprete()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.openFileProgram.InitialDirectory = Environment.CurrentDirectory;
            dataGridViewFuente.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewIntermedio.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTabSim.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string[] codigo;
            switch(e.ClickedItem.AccessibleName)
            {
                case "Abrir":
                    #region Abrir
                    if (openFileProgram.ShowDialog().Equals(DialogResult.OK))
                    {
                        this.limpiaDataGrids();
                        this.ensamblador = new Ensamblador(openFileProgram.FileName);
                        for (int i = 0; i < this.ensamblador.Archivo.Count; i++)
                        {
                            codigo = this.ensamblador.Archivo[i].Split('\t');
                            if (codigo.Length == 3)
                            {
                                this.dataGridViewFuente.Rows.Add(i + 1, codigo[0], codigo[1], codigo[2]);
                            }
                            else if (codigo.Length == 2)
                            {
                                this.dataGridViewFuente.Rows.Add(i + 1, codigo[0], codigo[1], "");
                            }

                        }
                    }
                    #endregion
                break;
                case "Analizar":
                    #region Analizar
                    if (this.ensamblador != null)
                    {
                        this.dataGridViewIntermedio.Rows.Clear();
                        for (int i = 0; i < this.ensamblador.Archivo.Count; i++)
                        {
                            this.ensamblador.compila(this.dataGridViewIntermedio,i);
                        }
                        this.llenaTabSimYErrores();
                        MetodosAuxiliares.grabaTabSim(this.ensamblador.Nombre,this.ensamblador.TabSim);
                        if (this.ensamblador.Errores.Count != 0)
                        {
                            MetodosAuxiliares.grabaErrores(this.ensamblador.Nombre,this.ensamblador.Errores);
                        }
                    }
                    #endregion
                    break;
            }
        }

        private void llenaTabSimYErrores()
        {
            this.dataGridViewTabSim.Rows.Clear();
            foreach (var simbolo in this.ensamblador.TabSim)
            {
                this.dataGridViewTabSim.Rows.Add(simbolo.Key,MetodosAuxiliares.convierteHexadecimal(simbolo.Value));
            }
            this.richTextBoxErrores.Text = "";
            if (this.ensamblador.Errores.Count != 0)
            {
                foreach (string error in this.ensamblador.Errores)
                {
                    this.richTextBoxErrores.Text += error + "\n";
                }
            }
        }

        private void limpiaDataGrids()
        {
            this.dataGridViewFuente.Rows.Clear();
            this.dataGridViewIntermedio.Rows.Clear();
            this.dataGridViewTabSim.Rows.Clear();
            this.richTextBoxErrores.Text = "";
        }
    }
}
