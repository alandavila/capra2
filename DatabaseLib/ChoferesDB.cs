using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public static class ChoferesDB
    {
        public static int AddChofer(Chofer chofer,int Id_cliente)
        {
            SqlConnection connection = RecoleccionDB.GetConnection();
            string strInsert = "INSERT tblChoferes "
                + "(Nombre,ClientesID)"
                + " VALUES (@Nombre,@ClientesID)";
            SqlCommand insertCommand = new SqlCommand(strInsert, connection);
            insertCommand.Parameters.AddWithValue("@Nombre", chofer.Nombre);
            insertCommand.Parameters.AddWithValue("@ClientesID",Id_cliente );
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string strSelect = "SELECT IDENT_CURRENT('tblChoferes') FROM tblChoferes";
                SqlCommand selectCommand = new SqlCommand(strSelect, connection);
                int choferID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return choferID;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

        }
        //returns a list of choferes
        public static List<Chofer> GetChoferes()
        {
            List<Chofer> Choferes = new List<Chofer>();
            SqlConnection connection = RecoleccionDB.GetConnection();
            //obtener choferes 
            string selectStatement = "SELECT tblChoferes.ClientesID, tblChoferes.Nombre,"
                                    + " tblChoferes.ChoferID"
                                    + " FROM tblChoferes "
                                    + "ORDER BY tblChoferes.Nombre";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Chofer chofer = new Chofer();
                while (reader.Read())
                {
                    chofer = new Chofer();
                    chofer.ChoferID = reader["ChoferID"].ToString();
                    chofer.Nombre = reader["Nombre"].ToString();
                    Choferes.Add(chofer);
                }
                reader.Close();
                Choferes.Add(chofer);
            }
            catch (SqlException ex)
            {
                //exception will be handled by the code where this class is used
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return Choferes;
        }
    }
}
