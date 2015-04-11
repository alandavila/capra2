using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;

namespace DatabaseLib
{
    public class Bitacora
    {
        private int dia;
        private string diaDigit;
        private int mes;
        private string mesDigit;
        private int year;
        private string empresa;
        private string chofer;
        private string horaSalida;
        private string horaEntrada;
        private int numTambos;
        private int nS;
        private string observaciones;
        private int numCamion;
        static int folio;
        private string folioDigit;
        private int clienteID;
        private int choferID;
        private int bitacoraID;
        private double precioUnitario;
        private double subtotal;
        private double iva;
        private double total;
        private DateTime fecha;

        public Bitacora()
        {
            folio++;
            folioDigit = String.Format("{0:000}", folio);
        }
        public DateTime Fecha 
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public int BitacoraID
        {
            get { return bitacoraID; }
            set { bitacoraID = value; }
        }
        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }
        public string FolioDigit
        {
            get { return folioDigit; }
            set { folioDigit = value; }
        }
        public int ClienteID
        {
            get { return clienteID; }
            set { clienteID = value; }
        }
        public int ChoferID
        {
            get { return choferID; }
            set { choferID = value; }
        }
        public double PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }
        public double Iva
        {
            get { return iva; }
            set { iva = value; }
        }
        public double Total
        {
            get { return total; }
            set { total = value; }
        }
        public double Subotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }
        public int Dia
        {
            get
            {
                return dia;
            }
            set
            {
                dia = value;
            }
        }
        public int Mes
        {
            get
            {
                return mes;
            }
            set
            {
                mes = value;
            }

        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }

        }
        public string Empresa
        {
            get
            {
                return empresa;
            }
            set
            {
                empresa = value;
            }

        }
        public string Chofer
        {

            get
            {
                return chofer;
            }
            set
            {
                chofer = value;
            }

        }
        public string HoraSalida
        {
            get
            {
                return horaSalida;
            }
            set
            {
                horaSalida = value;
            }
        }
        public string HoraEntrada
        {
            get
            {
                return horaEntrada;
            }
            set
            {
                horaEntrada = value;
            }
        }
        public int NumTambos
        {
            get
            {
                return numTambos;
            }
            set
            {
                numTambos = value;
            }
        }
        public int NS
        {
            get
            {
                return nS;
            }
            set
            {
                nS = value;
            }
        }
        public string Observaciones
        {
            get
            {
                return observaciones;
            }
            set
            {
                observaciones = value;
            }
        }
        public int NumCamion
        {
            get
            {
                return numCamion;
            }
            set
            {
                numCamion = value;
            }
        }

        public string InformationBitacora()
        {

            return "INFORMACION DE LA BITACORA:\n\n" +
            "Folio: " + folioDigit +
            "\nEmpresa: " + empresa +
            "\nNumero de Camion: " + numCamion +
            "\nFecha: " + dia + "/" + mes + "/" + year +
            "\nChofer: " + chofer +
            "\nHora Entrada: " + horaEntrada +
            "\nHora Salida: " + horaSalida +
            "\nNumero de Tambos: " + numTambos +
            "\nNS: " + nS +
            "\nObservaciones: " + observaciones +
            "\nPrecio Unitario: " + precioUnitario +
            "\nIVA: " + iva +
            "\nTotal: " + total + "\n";
        }

        public void CreateTxtFile()
        {
            string directoryPath = Properties.Settings.Default.FolderReciclados;
            if (dia >= 10)
                diaDigit = Convert.ToString(dia);
            else
                diaDigit = "0" + Convert.ToString(dia);
            if (mes <= 9)
                mesDigit = "0" + Convert.ToString(mes);
            else
                mesDigit = Convert.ToString(mes);
            //001 cambiara a numero de folio en cuando lo implemente
            string filePath = @directoryPath + "Bitacora_" + diaDigit + mesDigit + year + "_" + folioDigit + ".txt";
            FileStream fs = null;
            StreamWriter textOut = null;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            try
            {
                fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
                textOut = new StreamWriter(fs);
                //este sera el folio, .. implementar cuando implemente
                //la base de datos o antes para differenciar archivos
                textOut.WriteLine("Folio:                " + folioDigit);
                textOut.WriteLine("Empresa:              " + empresa);
                textOut.WriteLine("Numero de Camion:     " + numCamion);
                textOut.WriteLine("Fecha:                " + dia + "/" + mes + "/" + year);
                textOut.WriteLine("Chofer:               " + chofer);
                textOut.WriteLine("Hora Entrada:         " + horaEntrada);
                textOut.WriteLine("Hora Salida:          " + horaSalida);
                textOut.WriteLine("Numero de Tambos:     " + numTambos);
                textOut.WriteLine("NS:                   " + nS);
                textOut.WriteLine("Observaciones:        " + observaciones);
                textOut.WriteLine("Precio Unitario:      " + precioUnitario);
                textOut.WriteLine("IVA:                  " + iva);
                textOut.WriteLine("Total:                " + total);
                textOut.Close();
            }
            catch (FileNotFoundException)
            {
                System.Windows.Forms.MessageBox.Show(filePath + " no fue encontrado.", "Archivo No Encontrado");

            }
            catch (DirectoryNotFoundException)
            {
                System.Windows.Forms.MessageBox.Show(directoryPath + " no fue encontrado.", "Directorio No Enconstrado");
            }
            catch (IOException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "Problemas con la creacion del archivo.", "IOException: Problemas con la creacion del archivo");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }


        }

    }
}
