using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLib
{
    public class Productor
    {
        private string nombre;
        private string productorID;
        private string direccion;
        private string ciudad;
        private string codigoPostal;
        private string telefono;
        private string rFC;

        public Productor() 
        {
        }

        public string Nombre 
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string ProductorID 
        {
            get { return productorID; }
            set {productorID = value;}
        }
        public string Direccion 
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
