using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ValidationLib;
using DatabaseLib;

namespace EmpresaMaintenance
{
    public partial class EmpresaProductorLink : Form
    {
        //local variables
        List<Cliente> clientes = new List<Cliente>();
        List<Productor> productores = new List<Productor>();
        List<Par> Cliente_Productor = new List<Par>();
        List<Par> Queried_Cliente_Productor = new List<Par>();

        public EmpresaProductorLink()
        {
            InitializeComponent();
            this.LoadEmpresaComboBox();
            this.LoadProductoresComboBox();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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
                MessageBox.Show(ex.Message, ex.GetType().ToString() + ": error en EmpresaProductorLink:LoadEmpresaComboBox");

            }
        }
        //populate Productores combo box
        private void LoadProductoresComboBox() 
        {
            try
            {
                productores = ProductoresDB.GetProductores();
                cmbProductor.DataSource = productores;
                cmbProductor.DisplayMember = "Nombre";
                cmbProductor.ValueMember ="ProductorID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString() + ": error en EmpresaProductorLink:LoadProductoresComboBox");
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            int productor_indx;
            int cliente_indx;
            Cliente_Productor = ClienteProductorDB.GetEntries();
            frmReasignationDialog frmModificar = new frmReasignationDialog();
            this.AddOwnedForm(frmModificar);
            //Make sure a Productor and a Generador de residues is selected in the comboboxes
            if( this.cmbEmpresa.Text.Trim() != String.Empty &&
                this.cmbProductor.Text.Trim() != String.Empty)
            {
                productor_indx = int.Parse(this.cmbProductor.SelectedValue.ToString());
                cliente_indx = int.Parse(this.cmbEmpresa.SelectedValue.ToString());
                frmModificar.NewCliente_indx = cliente_indx;
                frmModificar.NewProductor_indx = productor_indx;
                //If the Generator exists in the tbljuncClientProd table, then:
                //1) Warn the user and ask whether you want to reassign the Generador the residuos
                Queried_Cliente_Productor = (from par in Cliente_Productor
                                    where par.b == productor_indx
                                    select par).ToList<Par>();
                if (Queried_Cliente_Productor.Count >= 1)
                {
                    Par queriedClienteProductor = Queried_Cliente_Productor[0];
                    frmModificar.OldCliente_indx = queriedClienteProductor.a;
                    frmModificar.OldProductor_indx = queriedClienteProductor.b;
                    //2)    If yes : delete the current Productor-Generador assignation from the table and insert the new assignation
                    //      IF not : leave the table unchanged
                    DialogResult result = frmModificar.ShowDialog(this);
                }
                //If the Generator does not exist in the tbljuncClientProb table, then:
                // insert the assignation in the table
                else 
                {
                    ClienteProductorDB.AddEntry(productor_indx, cliente_indx);
                }

            }

        }
    }
}
