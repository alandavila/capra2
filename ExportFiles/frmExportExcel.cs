using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExportLib;
using DatabaseLib;

namespace ExportFiles
{
    public partial class frmExportExcel : Form
    {
        private DateTime _initialDate;
        private DateTime _finalDate;
        public frmExportExcel()
        {
            InitializeComponent();
            _initialDate = DateTime.Now;
            _finalDate = DateTime.Now;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            XportExcel xportExcel = new ExportLib.XportExcel();
            List<Cliente> Clientes = ClientesDB.GetClients();
            List<String> listaClientes = new List<string>();
            int i = 0;
            foreach (Cliente cliente in Clientes) 
            {
                listaClientes.Add(cliente.Nombre.Trim());
                i++;
            }

            _initialDate = this.fechaInicial.Value.Date;
            _finalDate = this.fechaFinal.Value.Date;
            xportExcel.CreateReportSkeleton(_initialDate, _finalDate, listaClientes);
        }
    }
}
