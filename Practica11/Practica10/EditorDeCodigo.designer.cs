namespace Practica10
{
    partial class EditorDeCodigo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorDeCodigo));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.nuevoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.abrirToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.guardarToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openFileSIC = new System.Windows.Forms.OpenFileDialog();
            this.saveFileSIC = new System.Windows.Forms.SaveFileDialog();
            this.dataGridViewCodigo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxEtiqueta = new System.Windows.Forms.TextBox();
            this.comboBoxOperacion = new System.Windows.Forms.ComboBox();
            this.textBoxOperando = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodigo)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripButton,
            this.abrirToolStripButton,
            this.guardarToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(671, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tool_Clicked);
            // 
            // nuevoToolStripButton
            // 
            this.nuevoToolStripButton.AccessibleName = "Nuevo";
            this.nuevoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nuevoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("nuevoToolStripButton.Image")));
            this.nuevoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nuevoToolStripButton.Name = "nuevoToolStripButton";
            this.nuevoToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.nuevoToolStripButton.Text = "&Nuevo";
            // 
            // abrirToolStripButton
            // 
            this.abrirToolStripButton.AccessibleName = "Abrir";
            this.abrirToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.abrirToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripButton.Image")));
            this.abrirToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrirToolStripButton.Name = "abrirToolStripButton";
            this.abrirToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.abrirToolStripButton.Text = "&Abrir";
            // 
            // guardarToolStripButton
            // 
            this.guardarToolStripButton.AccessibleName = "Guardar";
            this.guardarToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.guardarToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("guardarToolStripButton.Image")));
            this.guardarToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.guardarToolStripButton.Name = "guardarToolStripButton";
            this.guardarToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.guardarToolStripButton.Text = "&Guardar";
            // 
            // openFileSIC
            // 
            this.openFileSIC.FileName = "openFileDialog1";
            this.openFileSIC.Filter = "(*.s) | *.s";
            // 
            // saveFileSIC
            // 
            this.saveFileSIC.Filter = "(*.s) | *.s";
            // 
            // dataGridViewCodigo
            // 
            this.dataGridViewCodigo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewCodigo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewCodigo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCodigo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridViewCodigo.Location = new System.Drawing.Point(12, 28);
            this.dataGridViewCodigo.Name = "dataGridViewCodigo";
            this.dataGridViewCodigo.RowHeadersVisible = false;
            this.dataGridViewCodigo.Size = new System.Drawing.Size(345, 344);
            this.dataGridViewCodigo.TabIndex = 1;
            this.dataGridViewCodigo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCodigo_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Linea";
            this.Column1.Name = "Column1";
            this.Column1.Width = 58;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Etiqueta";
            this.Column2.Name = "Column2";
            this.Column2.Width = 71;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Instruccion/Directiva";
            this.Column3.Name = "Column3";
            this.Column3.Width = 131;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Operando";
            this.Column4.Name = "Column4";
            this.Column4.Width = 79;
            // 
            // textBoxEtiqueta
            // 
            this.textBoxEtiqueta.Location = new System.Drawing.Point(369, 56);
            this.textBoxEtiqueta.Name = "textBoxEtiqueta";
            this.textBoxEtiqueta.Size = new System.Drawing.Size(124, 20);
            this.textBoxEtiqueta.TabIndex = 2;
            // 
            // comboBoxOperacion
            // 
            this.comboBoxOperacion.FormattingEnabled = true;
            this.comboBoxOperacion.Location = new System.Drawing.Point(516, 56);
            this.comboBoxOperacion.Name = "comboBoxOperacion";
            this.comboBoxOperacion.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOperacion.TabIndex = 3;
            this.comboBoxOperacion.Text = "START";
            // 
            // textBoxOperando
            // 
            this.textBoxOperando.Location = new System.Drawing.Point(369, 109);
            this.textBoxOperando.Name = "textBoxOperando";
            this.textBoxOperando.Size = new System.Drawing.Size(124, 20);
            this.textBoxOperando.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.AccessibleName = "Agregar";
            this.button1.Location = new System.Drawing.Point(369, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.general_Click);
            // 
            // button2
            // 
            this.button2.AccessibleName = "Actualizar";
            this.button2.Location = new System.Drawing.Point(476, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Actualizar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.general_Click);
            // 
            // button3
            // 
            this.button3.AccessibleName = "Eliminar";
            this.button3.Location = new System.Drawing.Point(578, 171);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Eliminar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.general_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Etiqueta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(513, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Instruccion/Directiva";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Operando";
            // 
            // EditorDeCodigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 384);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxOperando);
            this.Controls.Add(this.comboBoxOperacion);
            this.Controls.Add(this.textBoxEtiqueta);
            this.Controls.Add(this.dataGridViewCodigo);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditorDeCodigo";
            this.Text = "EditorDeCodigo";
            this.Load += new System.EventHandler(this.EditorDeCodigo_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodigo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton nuevoToolStripButton;
        private System.Windows.Forms.ToolStripButton abrirToolStripButton;
        private System.Windows.Forms.ToolStripButton guardarToolStripButton;
        private System.Windows.Forms.OpenFileDialog openFileSIC;
        private System.Windows.Forms.SaveFileDialog saveFileSIC;
        private System.Windows.Forms.DataGridView dataGridViewCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TextBox textBoxEtiqueta;
        private System.Windows.Forms.ComboBox comboBoxOperacion;
        private System.Windows.Forms.TextBox textBoxOperando;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}