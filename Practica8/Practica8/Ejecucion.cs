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
                    }
                    break;
            }
        }
        private void limpiaDatagrid()
        {
            this.dataGridViewMapaDeMemoria.Rows.Clear();
        }

        private void Ejecucion_Load(object sender, EventArgs e)
        {

        }
    }
}
