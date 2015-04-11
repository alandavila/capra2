namespace BitacoraCAPRA
{
    partial class BitacoraProductorLink
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
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lvListView = new System.Windows.Forms.ListView();
            this.colTambos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGenerador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(98, 304);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(75, 23);
            this.btnAsignar.TabIndex = 0;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(202, 304);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lvListView
            // 
            this.lvListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTambos,
            this.colGenerador});
            this.lvListView.GridLines = true;
            this.lvListView.LabelEdit = true;
            this.lvListView.Location = new System.Drawing.Point(58, 41);
            this.lvListView.Name = "lvListView";
            this.lvListView.Size = new System.Drawing.Size(264, 231);
            this.lvListView.TabIndex = 2;
            this.lvListView.UseCompatibleStateImageBehavior = false;
            this.lvListView.View = System.Windows.Forms.View.Details;
            // 
            // colTambos
            // 
            this.colTambos.Text = "Tambos";
            this.colTambos.Width = 176;
            // 
            // colGenerador
            // 
            this.colGenerador.Text = "Generador";
            this.colGenerador.Width = 67;
            // 
            // BitacoraProductorLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 352);
            this.Controls.Add(this.lvListView);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAsignar);
            this.Name = "BitacoraProductorLink";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.ListView lvListView;
        public System.Windows.Forms.ColumnHeader colGenerador;
        public System.Windows.Forms.ColumnHeader colTambos;
    }
}