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
        private bool extendida;
        #endregion

        #region Constructores
        public EditorDeCodigo(bool extendida)
        {
            this.extendida = extendida;
            InitializeComponent();
        }


        private void EditorDeCodigo_Load(object sender, EventArgs e)
        {
            this.openFileSIC.InitialDirectory = Environment.CurrentDirectory;
            this.saveFileSIC.InitialDirectory = Environment.CurrentDirectory;
            if (extendida)
            {
                //Formato1
                this.comboBoxOperacion.Items.Add("FIX");
                this.comboBoxOperacion.Items.Add("FLOAT");
                this.comboBoxOperacion.Items.Add("HIO");
                this.comboBoxOperacion.Items.Add("NORM");
                this.comboBoxOperacion.Items.Add("SIO");
                this.comboBoxOperacion.Items.Add("TIO");
                //Formato2
                this.comboBoxOperacion.Items.Add("ADDR");
                this.comboBoxOperacion.Items.Add("CLEAR");
                this.comboBoxOperacion.Items.Add("COMPR");
                this.comboBoxOperacion.Items.Add("DIVR");
                this.comboBoxOperacion.Items.Add("MULR");
                this.comboBoxOperacion.Items.Add("RMO");
                this.comboBoxOperacion.Items.Add("SHIFTL");
                this.comboBoxOperacion.Items.Add("SHIFTR");
                this.comboBoxOperacion.Items.Add("SUBR");
                this.comboBoxOperacion.Items.Add("SVC");
                this.comboBoxOperacion.Items.Add("TIXR");
                //Formato3/4
                this.comboBoxOperacion.Items.Add("ADD");
                this.comboBoxOperacion.Items.Add("ADDF");
                this.comboBoxOperacion.Items.Add("AND");
                this.comboBoxOperacion.Items.Add("COMP");
                this.comboBoxOperacion.Items.Add("COMPF");
                this.comboBoxOperacion.Items.Add("DIV");
                this.comboBoxOperacion.Items.Add("DIVF");
                this.comboBoxOperacion.Items.Add("J");
                this.comboBoxOperacion.Items.Add("JEQ");
                this.comboBoxOperacion.Items.Add("JGT");
                this.comboBoxOperacion.Items.Add("JLT");
                this.comboBoxOperacion.Items.Add("JSUB");
                this.comboBoxOperacion.Items.Add("LDA");
                this.comboBoxOperacion.Items.Add("LDB");
                this.comboBoxOperacion.Items.Add("LDCH");
                this.comboBoxOperacion.Items.Add("LDF");
                this.comboBoxOperacion.Items.Add("LDL");
                this.comboBoxOperacion.Items.Add("LDS");
                this.comboBoxOperacion.Items.Add("LDT");
                this.comboBoxOperacion.Items.Add("LDX");
                this.comboBoxOperacion.Items.Add("LPS");
                this.comboBoxOperacion.Items.Add("MUL");
                this.comboBoxOperacion.Items.Add("MULF");
                this.comboBoxOperacion.Items.Add("OR");
                this.comboBoxOperacion.Items.Add("RD");
                this.comboBoxOperacion.Items.Add("RSUB");
                this.comboBoxOperacion.Items.Add("SSK");
                this.comboBoxOperacion.Items.Add("STA");
                this.comboBoxOperacion.Items.Add("STB");
                this.comboBoxOperacion.Items.Add("STCH");
                this.comboBoxOperacion.Items.Add("STF");
                this.comboBoxOperacion.Items.Add("STI");
                this.comboBoxOperacion.Items.Add("STL");
                this.comboBoxOperacion.Items.Add("STS");
                this.comboBoxOperacion.Items.Add("STSW");
                this.comboBoxOperacion.Items.Add("STT");
                this.comboBoxOperacion.Items.Add("STX");
                this.comboBoxOperacion.Items.Add("SUB");
                this.comboBoxOperacion.Items.Add("SUBF");
                this.comboBoxOperacion.Items.Add("TD");
                this.comboBoxOperacion.Items.Add("TIX");
                this.comboBoxOperacion.Items.Add("WD");
                //
                //Formato3/4
                this.comboBoxOperacion.Items.Add("+ADD");
                this.comboBoxOperacion.Items.Add("+ADDF");
                this.comboBoxOperacion.Items.Add("+AND");
                this.comboBoxOperacion.Items.Add("+COMP");
                this.comboBoxOperacion.Items.Add("+COMPF");
                this.comboBoxOperacion.Items.Add("+DIV");
                this.comboBoxOperacion.Items.Add("+DIVF");
                this.comboBoxOperacion.Items.Add("+J");
                this.comboBoxOperacion.Items.Add("+JEQ");
                this.comboBoxOperacion.Items.Add("+JGT");
                this.comboBoxOperacion.Items.Add("+JLT");
                this.comboBoxOperacion.Items.Add("+JSUB");
                this.comboBoxOperacion.Items.Add("+LDA");
                this.comboBoxOperacion.Items.Add("+LDB");
                this.comboBoxOperacion.Items.Add("+LDCH");
                this.comboBoxOperacion.Items.Add("+LDF");
                this.comboBoxOperacion.Items.Add("+LDL");
                this.comboBoxOperacion.Items.Add("+LDS");
                this.comboBoxOperacion.Items.Add("+LDT");
                this.comboBoxOperacion.Items.Add("+LDX");
                this.comboBoxOperacion.Items.Add("+LPS");
                this.comboBoxOperacion.Items.Add("+MUL");
                this.comboBoxOperacion.Items.Add("+MULF");
                this.comboBoxOperacion.Items.Add("+OR");
                this.comboBoxOperacion.Items.Add("+RD");
                this.comboBoxOperacion.Items.Add("+RSUB");
                this.comboBoxOperacion.Items.Add("+SSK");
                this.comboBoxOperacion.Items.Add("+STA");
                this.comboBoxOperacion.Items.Add("+STB");
                this.comboBoxOperacion.Items.Add("+STCH");
                this.comboBoxOperacion.Items.Add("+STF");
                this.comboBoxOperacion.Items.Add("+STI");
                this.comboBoxOperacion.Items.Add("+STL");
                this.comboBoxOperacion.Items.Add("+STS");
                this.comboBoxOperacion.Items.Add("+STSW");
                this.comboBoxOperacion.Items.Add("+STT");
                this.comboBoxOperacion.Items.Add("+STX");
                this.comboBoxOperacion.Items.Add("+SUB");
                this.comboBoxOperacion.Items.Add("+SUBF");
                this.comboBoxOperacion.Items.Add("+TD");
                this.comboBoxOperacion.Items.Add("+TIX");
                this.comboBoxOperacion.Items.Add("+WD");
                this.comboBoxOperacion.Items.Add("START");
                this.comboBoxOperacion.Items.Add("END");
                this.openFileSIC.Filter = "SICExtendida (*.sx)|*.sx";
                this.saveFileSIC.Filter = "SICExtendida (*.sx)|*.sx";
            }
            else
            {
                this.comboBoxOperacion.Items.Add("ADD");
                this.comboBoxOperacion.Items.Add("AND");
                this.comboBoxOperacion.Items.Add("COMP");
                this.comboBoxOperacion.Items.Add("DIV");
                this.comboBoxOperacion.Items.Add("J");
                this.comboBoxOperacion.Items.Add("JEQ");
                this.comboBoxOperacion.Items.Add("JGT");
                this.comboBoxOperacion.Items.Add("JLT");
                this.comboBoxOperacion.Items.Add("JSUB");
                this.comboBoxOperacion.Items.Add("LDA");
                this.comboBoxOperacion.Items.Add("LDCH");
                this.comboBoxOperacion.Items.Add("LDL");
                this.comboBoxOperacion.Items.Add("LDX");
                this.comboBoxOperacion.Items.Add("MUL");
                this.comboBoxOperacion.Items.Add("OR");
                this.comboBoxOperacion.Items.Add("RD");
                this.comboBoxOperacion.Items.Add("RSUB");
                this.comboBoxOperacion.Items.Add("STA");
                this.comboBoxOperacion.Items.Add("STCH");
                this.comboBoxOperacion.Items.Add("STL");
                this.comboBoxOperacion.Items.Add("STSW");
                this.comboBoxOperacion.Items.Add("STX");
                this.comboBoxOperacion.Items.Add("SUB");
                this.comboBoxOperacion.Items.Add("TD");
                this.comboBoxOperacion.Items.Add("TIX");
                this.comboBoxOperacion.Items.Add("WD");
                this.comboBoxOperacion.Items.Add("BYTE");
                this.comboBoxOperacion.Items.Add("WORD");
                this.comboBoxOperacion.Items.Add("RESW");
                this.comboBoxOperacion.Items.Add("RESB");
                this.comboBoxOperacion.Items.Add("START");
                this.comboBoxOperacion.Items.Add("END");
                this.openFileSIC.Filter = "SICEstandar (*.s)|*.s";
                this.saveFileSIC.Filter = "SICEstandar (*.s)|*.s";
            }
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