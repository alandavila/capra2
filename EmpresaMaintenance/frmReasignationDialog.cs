using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLib;

namespace EmpresaMaintenance
{
    public partial class frmReasignationDialog : Form
    {
        private int _Oldproductor_indx;
        private int _Oldcliente_indx;
        private int _Newproductor_indx;
        private int _Newcliente_indx;

        public int OldProductor_indx 
        {
            get { return _Oldproductor_indx; }
            set { this._Oldproductor_indx = value; }
        }
        public int OldCliente_indx 
        {
            get { return _Oldcliente_indx; }
            set { this._Oldcliente_indx = value; }
        }
        public int NewProductor_indx
        {
            get { return _Newproductor_indx; }
            set { this._Newproductor_indx = value; }
        }
        public int NewCliente_indx
        {
            get { return _Newcliente_indx; }
            set { this._Newcliente_indx = value; }
        }
        public frmReasignationDialog()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ClienteProductorDB.DeletEntry(_Oldproductor_indx, _Oldcliente_indx);
            ClienteProductorDB.AddEntry(_Newproductor_indx, _Newcliente_indx);
            this.Close();
        }
    }
}
