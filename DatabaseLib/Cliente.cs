using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLib
{
    public class Cliente
    {
        private string nombre;
        private string clienteID;
        private string direccion;
        private string ciudad;
        private string codigoPostal;
        private string telefono;
        private string rFC;
        //public List<string> choferes = new List<string>();
        public List<Chofer> choferes = new List<Chofer>();
        public List<Productor> productores = new List<Productor>();

        public Cliente()
        {
        }
        public string ClienteID
        {
            get { return clienteID; }
            set { clienteID = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Direction
        {
            get { return direccion; }
            set { direccion = value; }
        }
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }
        public string CodigoPostal
        {
            get { return codigoPostal; }
            set { codigoPostal = value; }
        }
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        public string RFC
        {
            get { return rFC; }
            set { rFC = value; }
        }


    }
}
