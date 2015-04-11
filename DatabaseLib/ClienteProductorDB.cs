using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public static class ClienteProductorDB
    {
        public static int AddEntry(int productorID, int ClienteID)
        {
            SqlConnection connection = RecoleccionDB.GetConnection();
            string strInsert = "INSERT tbljuncClientProd "
                + "(ClientesID,ProductorID)"
                + " VALUES (@ClientesID,@ProductorID)";
            SqlCommand insertCommand = new SqlCommand(strInsert, connection);
            insertCommand.Parameters.AddWithValue("@ClientesID", ClienteID);
            insertCommand.Parameters.AddWithValue("@ProductorID",productorID );
            try
            {
                connection.Open();
                int rowsaffected = insertCommand.ExecuteNonQuery();
                return rowsaffected;

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
        public static bool DeletEntry(int productorID, int ClienteID) 
        {
            SqlConnection connection = RecoleccionDB.GetConnection();
            string strDelete = "DELETE FROM tbljuncClientProd "
                + "WHERE productorID = @productorID "
                + "AND ClientesID = @clientesID ";
            SqlCommand deletecommand = new SqlCommand(strDelete, connection);
            deletecommand.Parameters.AddWithValue("@productorID", productorID);
            deletecommand.Parameters.AddWithValue("@clientesID", ClienteID);
            try
            {
                connection.Open();
                int deletedRows = deletecommand.ExecuteNonQuery();
                if (deletedRows > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

        }
        //get list of cliente,productor in a list, a = clienteID, b = productorID
        //order by productorID
        public static List<Par> GetEntries() 
        {
            List<Par> pares = new List<Par>();
            SqlConnection connection = DatabaseLib.RecoleccionDB.GetConnection();
            string strSelect =
                  " SELECT tbljuncClientProd.ClientesID,tbljuncClientProd.ProductorID"
                + " FROM tbljuncClientProd"
                + " ORDER BY tbljuncClientProd.productorID";
            SqlCommand cmdSelect = new SqlCommand(strSelect, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = cmdSelect.ExecuteReader();

                while (reader.Read())
                {
                    Par cliente_productor = new Par();
                    cliente_productor.a = int.Parse(reader["ClientesID"].ToString());
                    cliente_productor.b = int.Parse(reader["productorID"].ToString());
                    pares.Add(cliente_productor);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //external code will take care of exception
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return pares;

        }
    }
}
