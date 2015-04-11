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

namespace ProductorMaintenance
{
    public partial class ProductorMaintenance : Form
    {
        public ProductorMaintenance()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Validator validator = new Validator();
            if (validator.IsPresent(this.txtNombre, "Nombre ") && validator.IsPresent(this.txtDireccion, "Direccion "))
            {
                Productor nuevo_productor = new Productor();
                nuevo_productor.Nombre = this.txtNombre.Text;
                nuevo_productor.Direccion = this.txtDireccion.Text;
                if (validator.IsPresent(this.txtCiudad, "Ciudad ", false))
                    nuevo_productor.Ciudad = this.txtCiudad.Text;
                else
                    nuevo_productor.Ciudad = "NA";
                if (validator.IsPresent(this.txtCodigoPostal, "Codigo Postal ", false) && validator.ValidateNumberInt(this.txtCodigoPostal.Text))
                    nuevo_productor.CodigoPostal = this.txtCodigoPostal.Text;
                else
                    nuevo_productor.CodigoPostal = "0";
                if (validator.IsPresent(this.txtRFC, "RFC ", false))
                    nuevo_productor.RFC = this.txtRFC.Text;
                else
                    nuevo_productor.RFC = "NA";
                if (validator.IsPresent(this.txtTelefono, "Telefone ", false))
                    nuevo_productor.Telefono = this.txtTelefono.Text;
                else
                    nuevo_productor.Telefono = "NA";

                nuevo_productor.ProductorID = DatabaseLib.ProductoresDB.AddProductor(nuevo_productor).ToString();

            }
            else
            {
                MessageBox.Show("La empresa generadora de organicos requiere al menos nombre y direccion para ser guardada.", "Faltan Datos");
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
