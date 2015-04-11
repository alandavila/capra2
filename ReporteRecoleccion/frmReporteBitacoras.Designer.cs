namespace ReporteRecoleccion
{
    partial class frmReporteBitacoras
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
            this.lvListView = new System.Windows.Forms.ListView();
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmpresa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colChofer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNumTambos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrecioUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIVA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObservaciones = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvListView
            // 
            this.lvListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFolio,
            this.colEmpresa,
            this.colChofer,
            this.colFecha,
            this.colNumTambos,
            this.colPrecioUnitario,
            this.colSubTotal,
            this.colIVA,
            this.colTotal,
            this.colObservaciones});
            this.lvListView.Location = new System.Drawing.Point(32, 50);
            this.lvListView.Name = "lvListView";
            this.lvListView.Size = new System.Drawing.Size(671, 172);
            this.lvListView.TabIndex = 0;
            this.lvListView.UseCompatibleStateImageBehavior = false;
            this.lvListView.View = System.Windows.Forms.View.Details;
            // 
            // colFolio
            // 
            this.colFolio.Text = "Folio";
            // 
            // colEmpresa
            // 
            this.colEmpresa.Text = "Empresa";
            this.colEmpresa.Width = 102;
            // 
            // colChofer
            // 
            this.colChofer.Text = "Chofer";
            this.colChofer.Width = 111;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha";
            this.colFecha.Width = 96;
            // 
            // colNumTambos
            // 
            this.colNumTambos.Text = "# Tambos";
            // 
            // colPrecioUnitario
            // 
            this.colPrecioUnitario.Text = "Precio Unitario";
            this.colPrecioUnitario.Width = 82;
            // 
            // colSubTotal
            // 
            this.colSubTotal.Text = "Subtotal";
            // 
            // colIVA
            // 
            this.colIVA.Text = "IVA";
            this.colIVA.Width = 49;
            // 
            // colTotal
            // 
            this.colTotal.Text = "Total";
            // 
            // colObservaciones
            // 
            this.colObservaciones.Text = "Observaciones";
            this.colObservaciones.Width = 100;
            // 
            // frmReporteBitacoras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 325);
            this.Controls.Add(this.lvListView);
            this.Name = "frmReporteBitacoras";
            this.Text = "Reporte";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader colFolio;
        private System.Windows.Forms.ColumnHeader colEmpresa;
        private System.Windows.Forms.ColumnHeader colChofer;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colNumTambos;
        private System.Windows.Forms.ColumnHeader colPrecioUnitario;
        private System.Windows.Forms.ColumnHeader colSubTotal;
        private System.Windows.Forms.ColumnHeader colIVA;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.ColumnHeader colObservaciones;
        public System.Windows.Forms.ListView lvListView;
    }
}