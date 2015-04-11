using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace ValidationLib
{
    public class Validator
    {
        public Validator()
        {
        }
        public string _message = string.Empty;
        //
        //ie no. de camion, N/S, cantidad de tambos
        //folio
        public bool ValidateNumberInt(string str_num, string str_option = "??")
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(str_num);
            }
            catch
            {
                _message = String.Format("El texto en {0} debe ser un entero en el rango 0 - 9999999", str_option);
                return false;
            }
            if (num >= 0 && num <= 9999999)
                return true;
            else
            {
                _message = String.Format("El texto en {0} debe ser un entero en el rango 0 - 9999999", str_option);
                return false;
            }
        }

        // allowed hours hh:mm , hh:[0,23] and mm:[0,59]
        public bool ValidateHour(string hour)
        {
            int _hour = 99;
            int _minute = 99;
            if (hour.Length != 5 && hour.Length != 4)
            {
                if (hour.Length < 4)
                    _message = "La hora tiene digitos de menos";
                if (hour.Length > 5)
                    _message = "La hora tiene digitos de mas";

                return false;
            }
            else
            {
                try
                {
                    if (hour.Length == 5)
                    {
                        _hour = Convert.ToInt32(hour.Substring(0, 2));
                        _minute = Convert.ToInt32(hour.Substring(3, 2));
                    }
                    else
                    {
                        _hour = Convert.ToInt32(hour.Substring(0, 1));
                        _minute = Convert.ToInt32(hour.Substring(2, 2));
                    }
                }
                catch
                {
                    return false;
                }
                if (_hour >= 0 && _hour <= 23 && _minute >= 0 && _minute <= 59)
                {

                    return true;
                }
                else
                {
                    _message = "El rango de horas permitidas es 0 - 23\n" +
                               "El rando de minutos permitidos es 00 - 59";
                }
            }
            return false;
        }
        // la hora de salida tiene que ser despues de la hora de entrada
        public bool ValidateHourDiff(string hour_initial, string hour_final)
        {
            TimeSpan initial = new TimeSpan();
            TimeSpan final = new TimeSpan();
            try
            {
                initial = TimeSpan.Parse(hour_initial);
                final = TimeSpan.Parse(hour_final);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            int value = TimeSpan.Compare(initial, final);
            if (value >= 0)
            {
                _message = "La hora de salida no puede ser mas temprano que la hora de entrada";
                return false;
            }
            else
                return true;
        }
        public bool IsPresent(TextBox textBox, string name,bool verbose = true)
        {
            if (textBox.Text == "")
            {
                if(verbose)
                    MessageBox.Show(name + " no ha sido capturado(a) ", "Faltan Datos");
                textBox.Focus();
                return false;
            }
            return true;
        }
    }
}
