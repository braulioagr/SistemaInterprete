namespace Practica8
{
    partial class Ejecucion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ejecucion));
            this.toolStripMapa = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewMapaDeMemoria = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileObj = new System.Windows.Forms.OpenFileDialog();
            this.toolStripMapa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMapaDeMemoria)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMapa
            // 
            this.toolStripMapa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAbrir,
            this.toolStripSeparator1,
            this.toolStripButton2});
            this.toolStripMapa.Location = new System.Drawing.Point(0, 0);
            this.toolStripMapa.Name = "toolStripMapa";
            this.toolStripMapa.Size = new System.Drawing.Size(800, 37);
            this.toolStripMapa.TabIndex = 1;
            this.toolStripMapa.Text = "toolStrip1";
            this.toolStripMapa.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMapa_ItemClicked);
            // 
            // toolStripButtonAbrir
            // 
            this.toolStripButtonAbrir.AccessibleName = "Abrir";
            this.toolStripButtonAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbrir.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbrir.Image")));
            this.toolStripButtonAbrir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbrir.Name = "toolStripButtonAbrir";
            this.toolStripButtonAbrir.Size = new System.Drawing.Size(34, 34);
            this.toolStripButtonAbrir.Text = "Abrir archivo obj";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 34);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // dataGridViewMapaDeMemoria
            // 
            this.dataGridViewMapaDeMemoria.AllowUserToAddRows = false;
            this.dataGridViewMapaDeMemoria.AllowUserToDeleteRows = false;
            this.dataGridViewMapaDeMemoria.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewMapaDeMemoria.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewMapaDeMemoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMapaDeMemoria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17});
            this.dataGridViewMapaDeMemoria.Location = new System.Drawing.Point(12, 40);
            this.dataGridViewMapaDeMemoria.Name = "dataGridViewMapaDeMemoria";
            this.dataGridViewMapaDeMemoria.RowHeadersVisible = false;
            this.dataGridViewMapaDeMemoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMapaDeMemoria.Size = new System.Drawing.Size(776, 398);
            this.dataGridViewMapaDeMemoria.TabIndex = 2;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Direccion";
            this.Column4.Name = "Column4";
            this.Column4.Width = 77;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "0";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 38;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "1";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 38;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "2";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 38;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "3";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 38;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "4";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 38;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "5";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 38;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "6";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 38;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "7";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 38;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "8";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 38;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "9";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 38;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "A";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 39;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "B";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 39;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "C";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 39;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "D";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 40;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "E";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 39;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "F";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 38;
            // 
            // openFileObj
            // 
            this.openFileObj.FileName = "openFileDialog1";
            this.openFileObj.Filter = "(*.obj) | *.obj";
            // 
            // Ejecucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewMapaDeMemoria);
            this.Controls.Add(this.toolStripMapa);
            this.Name = "Ejecucion";
            this.Text = "Form1";
            this.toolStripMapa.ResumeLayout(false);
            this.toolStripMapa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMapaDeMemoria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMapa;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridView dataGridViewMapaDeMemoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.OpenFileDialog openFileObj;
    }
}

