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

namespace ReporteRecoleccion
{
    public partial class frmReporteRecoleccion : Form
    {

        private int _checkFolio;
        private int _checkFecha;
        private int _checkEmpresa;
        private int _checkChofer;
        SortedList<string, Cliente> listaClientes = new SortedList<string, Cliente>();
        List<Cliente> clientes = new List<Cliente>();
        List<Chofer>  choferes = new List<Chofer>();

        public frmReporteRecoleccion()
        {
            InitializeComponent();
            _checkFolio = 0;
            _checkFecha = 0;
            _checkEmpresa = 0;
            _checkChofer = 0;
            this.LoadEmpresaComboBox();
        }

        private void btnObtener_Click(object sender, EventArgs e)
        {
            List<Bitacora> bitacoras = new List<Bitacora>();
            bitacoras = DatabaseLib.BitacoraDB.GetBitacoras();
            List<Bitacora> queriedBitacoras =  new List<Bitacora>();
            Validator validator = new Validator();
            List<string> selectedEmpresa = new List<string>();
            List<string> selectedChofer = new List<string>();
            DateTime selectedInitialDate = new DateTime();
            DateTime selectedFinalDate = new DateTime();

            //assign values to selectedEmpressa list according to user
            //selections in the GUI for LINQ predicate filters at runtime
            if (this.chkEmpresa.Checked)
            {
                selectedEmpresa.Add(this.cmbEmpresa.Text.Trim());
            }
            else 
            {
                clientes = ClientesDB.GetClients();
                foreach (Cliente cliente in clientes) 
                {
                    selectedEmpresa.Add(cliente.Nombre.Trim());
                }
            }
            //assign values to selectedChofer list according to user
            //selections in the GUI for LINQ predicate filters at runtime
            if (this.chkChofer.Checked) 
            {
                selectedChofer.Add(this.cmbChofer.Text.Trim());
            }
            else
            {
                choferes = ChoferesDB.GetChoferes();
                foreach (Chofer chofer in choferes) 
                {
                    selectedChofer.Add(chofer.Nombre.Trim());
                }
            }
            //assign values to selectedInitialDate/selectedFinalDate according to user
            //selections in the GUI for LINQ predicate filters at runtime
            if (this.chkFecha.Checked)
            {
                selectedInitialDate = dtpFechaInicial.Value.Date;
                selectedFinalDate = dtpFechaFinal.Value.Date;
            }
            else 
            {
                selectedInitialDate = DateTime.Parse("01/01/1901");
                selectedFinalDate = DateTime.Parse("01/01/2901");
            }

            if (this.chkFolio.Checked == true) 
            {
                if(this.txtFolio.Text != string.Empty)
                {
                    int Numfolio = -1;
                    if (validator.ValidateNumberInt(this.txtFolio.Text))
                        Numfolio = int.Parse(this.txtFolio.Text);
                    else
                        MessageBox.Show("El numero de folio no es valido.", "Datos incorrectos");

                    queriedBitacoras = (from bitacora in bitacoras
                                        where bitacora.BitacoraID == Numfolio
                                        select bitacora).ToList<Bitacora>();
                }
                else
                {
                    MessageBox.Show("Favor de especificar un numero de folio","Faltan Datos");
                }
            }
            else
            {
                queriedBitacoras = (from bitacora in bitacoras
                                    let chofi = bitacora.Chofer.ToString().Trim()
                                    let empresa = bitacora.Empresa.ToString().Trim()
                                    where (
                                           selectedChofer.Contains(chofi)
                                        && selectedEmpresa.Contains(empresa)
                                        && bitacora.Fecha >= selectedInitialDate
                                        && bitacora.Fecha <= selectedFinalDate
                                        )
                                    select bitacora).ToList<Bitacora>();
            }

            int i = 0;
            frmReporteBitacoras frmReporteListView = new frmReporteBitacoras();
            this.AddOwnedForm(frmReporteListView);
            
            i = 0;
              foreach (Bitacora bitacora in queriedBitacoras)
            {
                frmReporteListView.lvListView.Items.Add(bitacora.BitacoraID.ToString());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.Empresa.Trim());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.Chofer.Trim());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.Fecha.ToString().Trim());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.NumTambos.ToString());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.PrecioUnitario.ToString().Trim());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.Subotal.ToString().Trim());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.Iva.ToString().Trim());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.Total.ToString().Trim());
                frmReporteListView.lvListView.Items[i].SubItems.Add(bitacora.Observaciones);
                i += 1;
            }

              if (queriedBitacoras.Count > 0)
              {
                  DialogResult result = frmReporteListView.ShowDialog(this);
              }
              else {
                  MessageBox.Show("No se encontro ninguna bitacora");
              }
       
        }

        private void chkFolio_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkFolio % 2 == 0)
            {
                this.pnlConditions.Enabled = false;
                this.txtFolio.Enabled = true;
            }
            else
            {
                this.pnlConditions.Enabled = true;
                this.LoadEmpresaComboBox();
                this.txtFolio.Enabled = false;
            }
            _checkFolio += 1;
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

        //populate Empresa combo box
        private void LoadChoferComboBox()
        {
                try
                {

                    choferes = listaClientes[cmbEmpresa.Text.Trim()].choferes;
                    cmbChofer.DataSource = choferes;
                    cmbChofer.DisplayMember = "Nombre";
                    cmbChofer.ValueMember = "ChoferID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }

        }

        private void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                //get clients from database and fill the list with them
                clientes = ClientesDB.GetClients();
                //get a sorted list of clients with key =  name to use in form
                listaClientes = ClientesDB.GetClientsList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());

            }
        }

        private void cmbEmpresa_Leave(object sender, EventArgs e)
        {
            this.LoadChoferComboBox();
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkFecha % 2 == 0)
            {
                this.dtpFechaFinal.Enabled = true;
                this.dtpFechaInicial.Enabled = true;
            }
            else
            {
                this.dtpFechaFinal.Enabled = false;
                this.dtpFechaInicial.Enabled = false;
            }
            _checkFecha += 1;
        }

        private void chkEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkEmpresa % 2 == 0)
            {
                this.cmbEmpresa.Enabled = true; 
            }
            else
            {
                this.cmbEmpresa.Enabled = false;
            }
            _checkEmpresa += 1;

        }

        private void chkChofer_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkChofer % 2 == 0)
            {
                this.cmbChofer.Enabled = true;
            }
            else
            {
                this.cmbChofer.Enabled = false;
            }
            _checkChofer += 1;

        }
    }
}
