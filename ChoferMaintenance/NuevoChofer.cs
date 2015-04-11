using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLib;
using ValidationLib;

namespace ChoferMaintenance
{
    public partial class NuevoChofer : Form
    {
        List<Cliente> clientes = new List<Cliente>();
        int _ClienteID;
        Chofer chofer = new Chofer();

        public NuevoChofer()
        {
            InitializeComponent();
            this.txtNombre.Enabled = false;
            this.LoadEmpresaComboBox();
        }
        //populate Empresa combo box
        private void LoadEmpresaComboBox()
        {
            try
            {
                clientes = ClientesDB.GetClients();
                cmbEmpresa.DataSource = clientes;
                //tengo que usar un methodo (get) de Cliente
                //que es el elemento de la "list"
                cmbEmpresa.DisplayMember = "Nombre";
                cmbEmpresa.ValueMember = "ClienteID";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());

            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int comboBoxEmpresaIndx = this.cmbEmpresa.SelectedIndex;
            //use index to accsess clienteID of list item
            this._ClienteID = Convert.ToInt32(clientes[comboBoxEmpresaIndx].ClienteID);
            this.txtNombre.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidationLib.Validator validator = new ValidationLib.Validator();
            if (validator.IsPresent(this.txtNombre, "Nombre "))
            {
                chofer.Nombre = this.txtNombre.Text;
                int choferID = DatabaseLib.ChoferesDB.AddChofer(chofer, _ClienteID);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
