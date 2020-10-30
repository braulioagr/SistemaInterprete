using Practica8;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica6
{
    public partial class SistemaInterprete : Form
    {

        #region Variables de Instancia
        Ensamblador ensamblador;
        #endregion

        #region Constructores
        public SistemaInterprete()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.openFileProgram.InitialDirectory = Environment.CurrentDirectory;
            this.richTextBoxErrores.ForeColor = Color.Green;
            dataGridViewFuente.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewIntermedio.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTabSim.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion

        #region Eventos
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string[] codigo;
            switch (e.ClickedItem.AccessibleName)
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
                            else
                            {
                                this.dataGridViewFuente.Rows.Add(i + 1, "", "", "");
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
                        this.ensamblador.paso1(this.dataGridViewIntermedio);
                        this.ensamblador.Tamaño = MetodosAuxiliares.calculaTamaño(this.ensamblador.Intermedio.First(), this.ensamblador.Intermedio.Last());
                        this.llenaTabSimYErrores();
                        MetodosAuxiliares.grabaTabSim(this.ensamblador.Nombre, this.ensamblador.TabSim);
                        MetodosAuxiliares.grabaIntermedio(this.ensamblador.Nombre, this.ensamblador.Intermedio);
                        if (this.ensamblador.Errores.Count != 0)
                        {
                            MetodosAuxiliares.grabaErrores(this.ensamblador.Nombre, this.ensamblador.Errores);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor abra un archivo primero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    #endregion
                    break;
                case "ArchivoObj":
                    #region Archivo Obj
                    if (this.ensamblador != null)
                    {
                        if(this.ensamblador.Paso1Logrado)
                        {
                            this.ensamblador.paso2(this.dataGridViewIntermedio);
                            this.richTextBoxObj.Text += "\n";
                            foreach (string linea in this.ensamblador.ArchivoObj)
                            {
                                this.richTextBoxObj.Text += linea + "\n";
                            }
                            MetodosAuxiliares.grabaObj(this.ensamblador.Nombre, this.ensamblador.ArchivoObj);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor abra un archivo primero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    #endregion
                break;
                case "Texto":
                    EditorDeCodigo editor;
                    editor = new EditorDeCodigo();
                    editor.ShowDialog();
                break;
                case "EjecutaObj":
                    Ejecucion ejecucion;
                    ejecucion = new Ejecucion();
                    ejecucion.ShowDialog();
                break;
            }
        }
        #endregion

        #region Metodos

        private void llenaTabSimYErrores()
        {
            this.dataGridViewTabSim.Rows.Clear();
            foreach (var simbolo in this.ensamblador.TabSim)
            {
                this.dataGridViewTabSim.Rows.Add(simbolo.Key, MetodosAuxiliares.decimalAHexadecimal(simbolo.Value));
            }
            this.richTextBoxErrores.Text = "";
            if (this.ensamblador.Errores.Count != 0)
            {
                foreach (string error in this.ensamblador.Errores)
                {
                    this.richTextBoxErrores.Text += error + "\n";
                }
            }
            this.richTextBoxObj.Text = "Tamaño del archivo: " + MetodosAuxiliares.decimalAHexadecimal(this.ensamblador.Tamaño) + "\n________________________________";
        }

        private void limpiaDataGrids()
        {
            this.dataGridViewFuente.Rows.Clear();
            this.dataGridViewIntermedio.Rows.Clear();
            this.dataGridViewTabSim.Rows.Clear();
            this.richTextBoxErrores.Text = "";
            this.richTextBoxObj.Text = "";
        }

        #endregion

    }
}
