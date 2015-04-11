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
using EmpresaMaintenance;
using ChoferMaintenance;
using ReporteRecoleccion;
using ProductorMaintenance;

namespace BitacoraCAPRA
{
    public partial class BitacoraCAPRA : Form
    {
        //variables a guardar de la bitacora
        DateTime fechaBitacora = new DateTime();
        int _clienteID;
        int _choferID;
        SortedList<string,Cliente> listaClientes = new SortedList<string,Cliente>();
        List<Cliente> clientes = new List<Cliente>();
        List<Chofer> choferes = new List<Chofer>();
        List<Par> productores_Numtambos;

        public BitacoraCAPRA()
        {
            InitializeComponent();
            //fill clientes combo box
            this.LoadEmpresaComboBox();
        }        

        public bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " no ha sido capturado(a) ", "Faltan Datos");
                textBox.Focus();
                return false;
            }
            return true;
        }
        //overload IsPresent
        public bool IsPresent(ComboBox comboBox, string name)
        {
            if (comboBox.Text  == "")
            {
                MessageBox.Show(name + " no ha sido capturado(a) ", "Faltan Datos");
                comboBox.Focus();
                return false;
            }
            return true;
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
                MessageBox.Show(ex.Message,ex.GetType().ToString()+": error en LoadEmpresaComboBox");
 
            }
        }

        private void txtFecha_Leave(object sender, EventArgs e)
        {
            //validar aqui la fecha tiene que ser dia/mes/anio
            
            string fechaUsuario = this.cmbDia.Text;
            fechaUsuario = fechaUsuario.Trim();
            string[] fechaArray = fechaUsuario.Split('/');


            DateTime today = DateTime.Now;
            fechaBitacora = today;
            if(fechaArray.Length >=2)
            MessageBox.Show(fechaArray[1], "Fecha");

            ////la bitacora tiene que tener fecha
            //if (this.txtFecha.Text == "") 
            //{
            //    MessageBox.Show("La bitacora requiere fecha.","Faltan Datos");      
            //    txtFecha.Focus();
            //}

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Asegurando que la bitacora tiene TODOS los datos necesarios
            if (this.IsPresent(this.cmbDia, "dia") == true)
            if (this.IsPresent(this.cmbMes, "mes") == true)
            if (this.IsPresent(this.cmbYear, "año") == true)
            if (this.IsPresent(this.cmbEmpresa, "EMPRESA") == true)
            if (this.IsPresent(this.cmbChofer, "CHOFER") == true)
            if (this.IsPresent(this.txtNoCamion, "NUMERO DE CAMION") == true)
            if (this.IsPresent(this.txtHrEntrada, "HORA DE ENTRADA") == true)
            if (this.IsPresent(this.txtHrSalida, "HORA DE SALIDA") == true)
            if (this.IsPresent(this.txtNS, "N/S") == true)
            if (this.IsPresent(this.txtCantidadTambos, "CANTIDAD DE TAMBOS") == true) {
            //Llenar la clase bitacora con la informacion en bitacoraCapra
                Bitacora bitacora = new Bitacora();
                bitacora.Folio = 0;//por ahora
                //in development...
               // int comboboxEmpresaInx = this.cmbEmpresa.SelectedIndex;
                //_clienteID = Convert.ToInt32((List<Cliente>)cmbEmpresa.DataSource[comboboxEmpresaInx].);
                bitacora.ClienteID = _clienteID;
                bitacora.ChoferID = _choferID;

                bitacora.NS = Convert.ToInt32(this.txtNS.Text);
                bitacora.Dia = Convert.ToInt32(this.cmbDia.Text);
                bitacora.Mes = Convert.ToInt32(this.cmbMes.Text);
                bitacora.Year = Convert.ToInt32(this.cmbYear.Text);
                bitacora.Empresa = this.cmbEmpresa.Text;
                bitacora.Chofer = this.cmbChofer.Text;
                bitacora.NumCamion = Convert.ToInt32(this.txtNoCamion.Text);
                bitacora.HoraEntrada = this.txtHrEntrada.Text;
                bitacora.HoraSalida = this.txtHrSalida.Text;
                bitacora.NumTambos = Convert.ToInt32(this.txtCantidadTambos.Text);
                bitacora.Observaciones = this.txtObservaciones.Text;
                String lafechastr = this.cmbMes.Text.Trim() + "/"+ this.cmbDia.Text.Trim()+"/"+this.cmbYear.Text.Trim()+" "+this.txtHrSalida.Text;
                DateTime lafecha = DateTime.Parse(lafechastr);
                bitacora.Fecha = lafecha;
                //Imprimir la informacion
                MessageBox.Show((string)bitacora.InformationBitacora());
                //Guardar la informacion en un archivo de texto
                bitacora.CreateTxtFile();
                //Guardar bitacora en base de datos
                //if valid data... crear la clase de validacion de datos
                try
                {
                    bitacora.BitacoraID = DatabaseLib.BitacoraDB.AddBitacora(bitacora);
                    this.DialogResult = DialogResult.OK;
                    if (productores_Numtambos !=null && productores_Numtambos.Count >= 1) 
                    {
                        BitacoraProductorDB.AddEntry(bitacora.BitacoraID, _clienteID, _choferID, productores_Numtambos);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("en BitacoraCAPRA.cs,btnGuardar_Click " + ex.Message, ex.GetType().ToString());
                }
                                    
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
                //get the index of the client selected in combo box
                int comboboxEmpresaIndx = cmbEmpresa.SelectedIndex;
                //use index to accsess clienteID of list item
                this._clienteID = Convert.ToInt32(clientes[comboboxEmpresaIndx].ClienteID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());

            }
        }
        private void cmbChofer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if choferes not null do this:
                foreach (Chofer chofi in choferes)
                {
                    if (chofi.Nombre == this.cmbChofer.Text)
                        this._choferID = Convert.ToInt32(chofi.ChoferID);
                }
            }
            catch /*(Exception ex)*/
            {
                MessageBox.Show(String.Format("La empresa {0} aun no contine choferes assignados",this.cmbEmpresa.Text.Trim()),"Faltan datos");
                //MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void cmbEmpresa_Leave(object sender, EventArgs e)
        {

            this.LoadChoferComboBox();
            //MessageBox.Show("elegiste: "+cmbEmpresa.Text,"informacion");
        }

        private void txtHrEntrada_Leave(object sender, EventArgs e)
        {
            Validator validator = new Validator();
            if(!validator.ValidateHour(this.txtHrEntrada.Text))
            {
                MessageBox.Show("La hora de entrada requiere el formato:\n"+
                "HH:MM\n\n"+validator._message,"Cuidado!");
                this.txtHrEntrada.Text = "00:00";
            }
        }

        private void txtHrSalida_Leave(object sender, EventArgs e)
        {
            Validator validator = new Validator();
            if (!validator.ValidateHour(this.txtHrSalida.Text))
            {
                MessageBox.Show("La hora de salida requiere el formato:\n" +
                "HH:MM\n\n" + validator._message, "Cuidado!");
                this.txtHrSalida.Text = "00:00";
            }
            if (!validator.ValidateHour(this.txtHrEntrada.Text))
            {
                MessageBox.Show("La hora de entrada requiere el formato:\n" +
                "HH:MM\n\n" + validator._message, "Cuidado!");
                this.txtHrEntrada.Text = "00:00";
            }
            if (!validator.ValidateHourDiff(this.txtHrEntrada.Text, this.txtHrSalida.Text))
            {
                MessageBox.Show("Verifique que el formato y horas de entrada y salida"+
                "sea correcto: "+validator._message, "Cuidado!");
            }

        }

        private void txtNoCamion_Leave(object sender, EventArgs e)
        {
            Validator validator = new Validator();
            if (!validator.ValidateNumberInt(this.txtNoCamion.Text,this.lblCamion.Text))
            {
                MessageBox.Show("El numero de camion no es valido:\n"+ validator._message, "Cuidado!");
                this.txtNoCamion.Text = "0";
            }
        }

        private void txtCantidadTambos_Leave(object sender, EventArgs e)
        {
            Validator validator = new Validator();
            if (!validator.ValidateNumberInt(this.txtCantidadTambos.Text, this.lblCantidadTambos.Text))
            {
                MessageBox.Show("El numero de tambos no es valido:\n" + validator._message, "Cuidado!");
                this.txtCantidadTambos.Text = "0";
            }            
        }

        private void txtNS_Leave(object sender, EventArgs e)
        {
            Validator validator = new Validator();
            if (!validator.ValidateNumberInt(this.txtNS.Text, this.lblNS.Text))
            {
                MessageBox.Show("El NS no es valido:\n" + validator._message, "Cuidado!");
                this.txtNS.Text = "0";
            }            

        }

        private void btnNuevaEmpresa_Click(object sender, EventArgs e)
        {
            EmpresaMaintenance.EmpresaMaintenance frmEmpresaMaintenance = new EmpresaMaintenance.EmpresaMaintenance();
            this.AddOwnedForm(frmEmpresaMaintenance);
            DialogResult result =  frmEmpresaMaintenance.ShowDialog(this);
            if (result == DialogResult.OK) 
            {
                this.LoadEmpresaComboBox();
            }
        }

        private void BitacoraCAPRA_Enter(object sender, EventArgs e)
        {
            this.LoadEmpresaComboBox();
        }

        private void btnNuevoChofer_Click(object sender, EventArgs e)
        {
            NuevoChofer frmChofer = new NuevoChofer();
            this.AddOwnedForm(frmChofer);
            DialogResult result  = frmChofer.ShowDialog(this);

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            frmReporteRecoleccion frmRecolection = new frmReporteRecoleccion();
            this.AddOwnedForm(frmRecolection);
            DialogResult result = frmRecolection.ShowDialog(this);
        }

        private void btnNuevoProductor_Click(object sender, EventArgs e)
        {
            ProductorMaintenance.ProductorMaintenance frmProductorMaintenance = new ProductorMaintenance.ProductorMaintenance();
            this.AddOwnedForm(frmProductorMaintenance);
            DialogResult result = frmProductorMaintenance.ShowDialog(this);
        }

        private void mnuMaintenimientoNuevaEmpresa_Click(object sender, EventArgs e)
        {
            EmpresaMaintenance.EmpresaMaintenance frmEmpresaMaintenance = new EmpresaMaintenance.EmpresaMaintenance();
            this.AddOwnedForm(frmEmpresaMaintenance);
            DialogResult result = frmEmpresaMaintenance.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.LoadEmpresaComboBox();
            }
        }

        private void mnuMantenimientoNuevoChofer_Click(object sender, EventArgs e)
        {
            NuevoChofer frmChofer = new NuevoChofer();
            this.AddOwnedForm(frmChofer);
            DialogResult result = frmChofer.ShowDialog(this);
        }

        private void mnuMantenimientoNuevoProductor_Click(object sender, EventArgs e)
        {
            ProductorMaintenance.ProductorMaintenance frmProductorMaintenance = new ProductorMaintenance.ProductorMaintenance();
            this.AddOwnedForm(frmProductorMaintenance);
            DialogResult result = frmProductorMaintenance.ShowDialog(this);
        }

        private void assignarProductorEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmpresaMaintenance.EmpresaProductorLink frmEmpresaProductorLink = new EmpresaMaintenance.EmpresaProductorLink();
            this.AddOwnedForm(frmEmpresaProductorLink);
            DialogResult result = frmEmpresaProductorLink.ShowDialog(this);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //verificat que un Cliente y chofer han sido seleccionados:
            if (this.IsPresent(this.cmbEmpresa, "EMPRESA") == true && this.IsPresent(this.cmbChofer, "CHOFER") == true)
            {
                BitacoraProductorLink frmBitacoraProdLink = new BitacoraProductorLink();
                //variable filled from database
                List<Productor> productores = new List<Productor>();
                this.AddOwnedForm(frmBitacoraProdLink);
                //frmBitacoraProdLink.ClienteID = int.Parse(this.cmbEmpresa.SelectedValue.ToString());
                //frmBitacoraProdLink.ChoferID = int.Parse(this.cmbChofer.SelectedValue.ToString());

                //get productores from database, _clienteID has to be set before
                productores = ProductoresDB.GetProductores(_clienteID);
                //fill list view of frmBitacoraProdLink:
                int i = 0;
                foreach (Productor prod in productores)
                {
                    //default value of first column is cero, user can override it
                    frmBitacoraProdLink.lvListView.Items.Add("0");
                    frmBitacoraProdLink.lvListView.Items[i].SubItems.Add(prod.Nombre.Trim());
                    frmBitacoraProdLink._ProdID.Add(int.Parse(prod.ProductorID));
                    i++;
                }
                
                DialogResult result = frmBitacoraProdLink.ShowDialog(this);
                //get the values enterd in the form by the user
                productores_Numtambos = frmBitacoraProdLink._Prod_tambos;
            }
            else 
            {
                MessageBox.Show("Favor de seleccionar un Chofer y una Empresa.");
            }

        }

        private void bitacorasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuReporteBitacoras_Click(object sender, EventArgs e)
        {
            frmReporteRecoleccion frmRecolection = new frmReporteRecoleccion();
            this.AddOwnedForm(frmRecolection);
            DialogResult result = frmRecolection.ShowDialog(this);

        }

        private void mnuArchivoExportarEXCEL_Click(object sender, EventArgs e)
        {
            ExportFiles.frmExportExcel frmExportExcel = new ExportFiles.frmExportExcel();
            this.AddOwnedForm(frmExportExcel);
            DialogResult result = frmExportExcel.ShowDialog(this);
        }


    }
}