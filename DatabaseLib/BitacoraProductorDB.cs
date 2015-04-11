using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public  static class BitacoraProductorDB
    {
        public static int AddEntry(int bitacoraID, int ClienteID, int choferID, List<Par> prod_tambos)
        {
            int rowsaffected = 0;
            SqlConnection connection = RecoleccionDB.GetConnection();
 
            foreach (Par productor_numtambos in prod_tambos)
            {
                string strInsert = "INSERT tbljuncBitaProd "
                    + "(bitacoraID,clientesID,choferID,productorID,CantidadTambos)"
                    + " VALUES (@bitacoraID,@clientesID,@choferID,@productorID,@cantidadTambos)";
                SqlCommand insertCommand = new SqlCommand(strInsert, connection);
                insertCommand.Parameters.AddWithValue("@bitacoraID", bitacoraID);
                insertCommand.Parameters.AddWithValue("@clientesID", ClienteID);
                insertCommand.Parameters.AddWithValue("@choferID", choferID);
                insertCommand.Parameters.AddWithValue("@productorID", productor_numtambos.a);
                insertCommand.Parameters.AddWithValue("@cantidadTambos", productor_numtambos.b);
                try
                {
                    connection.Open();
                    rowsaffected += insertCommand.ExecuteNonQuery();
                    
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
            return rowsaffected;

        }
    }
}
