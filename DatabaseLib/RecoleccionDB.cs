using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public static class RecoleccionDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = Properties.Settings.Default.Recoleccion1ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            
            return connection;
        }
    }
}
