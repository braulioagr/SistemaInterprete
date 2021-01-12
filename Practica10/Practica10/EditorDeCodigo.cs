using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica10
{
    public partial class EditorDeCodigo : Form
    {
        #region Variables de Instancia
        private List<string> archivo;
        private int idLinea;
        private string nombre;
        #endregion

        #region Constructores
        public EditorDeCodigo()
        {
            InitializeComponent();
        }


        private void EditorDeCodigo_Load(object sender, EventArgs e)
        {
            this.openFileSIC.InitialDirectory = Environment.CurrentDirectory;
            this.saveFileSIC.InitialDirectory = Environment.CurrentDirectory;
            idLinea = -1;
        }
        #endregion

        #region Eventos
        private void tool_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "Nuevo":
                    if (saveFileSIC.ShowDialog().Equals(DialogResult.OK))
                    {
                        this.nombre = saveFileSIC.FileName;
                        this.archivo = new List<string>();
                        MetodosAuxiliares.grabaArchivoSIC(this.nombre, this.archivo);
                    }
                break;
                case "Abrir":
                    if (openFileSIC.ShowDialog().Equals(DialogResult.OK))
                    {
                        this.nombre = openFileSIC.FileName;
                        this.archivo = MetodosAuxiliares.leeArchivo(nombre);
                        this.actualizaDataGrid();
                    }
                    break;
                case "Guardar":
                    if (this.archivo != null)
                    {
                        MetodosAuxiliares.grabaArchivoSIC(this.nombre, this.archivo);
                    }
                break;
            }
        }

        private void actualizaDataGrid()
        {
            string[] codigo;
            this.dataGridViewCodigo.Rows.Clear();
            for (int i = 0; i < this.archivo.Count; i++)
            {
                codigo = this.archivo[i].Split('\t');
                if (codigo.Length == 3)
                {
                    this.dataGridViewCodigo.Rows.Add(i + 1, codigo[0], codigo[1], codigo[2]);
                }
                else if (codigo.Length == 2)
                {
                    this.dataGridViewCodigo.Rows.Add(i + 1, codigo[0], codigo[1], "");
                }
                else
                {
                    this.dataGridViewCodigo.Rows.Add(i + 1, "", "", "");
                }

            }
        }

        private void general_Click(object sender, EventArgs e)
        {
            if (this.archivo != null)
            {
                string linea;
                switch (((Button)sender).AccessibleName)
                {
                    case "Agregar":
                        linea = this.textBoxEtiqueta.Text + "\t" + this.comboBoxOperacion.Text + "\t" + this.textBoxOperando.Text;
                        this.archivo.Add(linea);
                    break;
                    case "Actualizar":
                        if (idLinea != -1)
                        {
                            linea = this.textBoxEtiqueta.Text + "\t" + this.comboBoxOperacion.Text + "\t" + this.textBoxOperando.Text;
                            this.archivo[idLinea] = linea;
                        }
                    break;
                    case "Eliminar":
                        if (idLinea != -1)
                        {
                            this.archivo.Remove(this.archivo[idLinea]);
                        }
                    break;
                }
                this.actualizaDataGrid();
            }
        }
        #endregion

        private void dataGridViewCodigo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.idLinea = int.Parse(this.dataGridViewCodigo.CurrentRow.Cells[0].Value.ToString())-1;
            this.textBoxEtiqueta.Text = this.dataGridViewCodigo.CurrentRow.Cells[1].Value.ToString();
            this.comboBoxOperacion.Text = this.dataGridViewCodigo.CurrentRow.Cells[2].Value.ToString();
            this.textBoxOperando.Text = this.dataGridViewCodigo.CurrentRow.Cells[3].Value.ToString();
        }

        private void limpia()
        {
            this.textBoxEtiqueta.Text = "";
            this.textBoxOperando.Text = "";
            this.comboBoxOperacion.Text = "ADD";
        }
    }
}