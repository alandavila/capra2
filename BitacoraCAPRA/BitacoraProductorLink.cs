using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLib;

namespace BitacoraCAPRA
{
    public partial class BitacoraProductorLink : Form
    {
        //vars to be gotten from BitacoraCAPRA form
        int _clienteID;
        int _choferID;
        //list will be filled with what is seen on list view
        public List<Par> _Prod_tambos = new List<Par>();
        public List<int> _ProdID = new List<int>();


        public int ClienteID
        {
            get { return _clienteID; }
            set { _clienteID = value; }
        }
        public int ChoferID 
        {
            get { return _choferID; }
            set { _choferID = value; }
        }
        public BitacoraProductorLink()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (ListViewItem item in lvListView.Items) 
            {
                Par prod_numtambs = new Par();
                prod_numtambs.a = _ProdID[i];
                prod_numtambs.b = int.Parse(item.Text);
                i++;
                _Prod_tambos.Add(prod_numtambs);
            }
            this.Close();
        }
    }
}
