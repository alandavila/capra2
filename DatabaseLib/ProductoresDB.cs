using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public static class ProductoresDB
    {
        public static int AddProductor(Productor productor)
        {
            SqlConnection connection = RecoleccionDB.GetConnection();
            string strInsert = "INSERT tblProductores "
                + "(Nombre, Direccion, CodigoPostal, Ciudad, Telefono, RFC)"
                + " VALUES (@Nombre,@Direccion,@CodigoPostal,@Ciudad,@Telefono,@RFC)";
            SqlCommand insertCommand = new SqlCommand(strInsert, connection);
            insertCommand.Parameters.AddWithValue("@Nombre", productor.Nombre);
            insertCommand.Parameters.AddWithValue("@Direccion",productor.Direccion );
            insertCommand.Parameters.AddWithValue("@CodigoPostal", Convert.ToInt32(productor.CodigoPostal));
            insertCommand.Parameters.AddWithValue("@Ciudad",productor.Ciudad );
            insertCommand.Parameters.AddWithValue("@Telefono",productor.Telefono );
            insertCommand.Parameters.AddWithValue("@RFC", productor.RFC);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string strSelect = "SELECT IDENT_CURRENT('tblProductores') FROM tblProductores";
                SqlCommand selectCommand = new SqlCommand(strSelect, connection);
                int productorID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return productorID;

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
        //returns a list of productores
        public static List<Productor> GetProductores()
        {
            List<Productor> Productores = new List<Productor>();
            SqlConnection connection = RecoleccionDB.GetConnection();
            //obtener productores 
            string selectStatement = "SELECT tblProductores.ProductorID, tblProductores.Nombre,tblProductores.Direccion,tblProductores.CodigoPostal,tblProductores.Ciudad,tblProductores.Telefono, tblProductores.RFC"
                                    + " FROM tblProductores "
                                    + "ORDER BY tblProductores.Nombre";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Productor productor = new Productor();
                while (reader.Read())
                {
                    productor = new Productor();
                    productor.ProductorID = reader["ProductorID"].ToString();
                    productor.Nombre = reader["Nombre"].ToString();
                    productor.Direccion = reader["Direccion"].ToString();
                    productor.CodigoPostal = reader["CodigoPostal"].ToString();
                    productor.Ciudad = reader["Ciudad"].ToString();
                    productor.Telefono = reader["Telefono"].ToString();
                    productor.RFC = reader["RFC"].ToString();
                    Productores.Add(productor);
                }
                reader.Close();
               
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

            return Productores;
        }
        //returns a list of producoter that are linked to a particular cliente
        public static List<Productor> GetProductores(int clienteID)
        {
            List<Productor> Productores = new List<Productor>();
            SqlConnection connection = RecoleccionDB.GetConnection();
            //obtener productores 
            string selectStatement = "SELECT tblProductores.ProductorID, tblProductores.Nombre,tblProductores.Direccion,tblProductores.CodigoPostal,tblProductores.Ciudad,tblProductores.Telefono, tblProductores.RFC"
                                    + " ,tblClientes.ClientesID "
                                    + " FROM tblProductores "
                                    + " INNER JOIN tbljuncClientProd ON tblProductores.ProductorID = tbljuncClientProd.ProductorID "
                                    + " INNER JOIN tblClientes ON tblClientes.ClientesID = tbljuncClientProd.ClientesID "
                                    + " WHERE tblClientes.ClientesID = @clientesID "
                                    + " ORDER BY tblProductores.Nombre";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@clientesID", clienteID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Productor productor = new Productor();
                while (reader.Read())
                {
                    productor = new Productor();
                    productor.ProductorID = reader["ProductorID"].ToString();
                    productor.Nombre = reader["Nombre"].ToString();
                    productor.Direccion = reader["Direccion"].ToString();
                    productor.CodigoPostal = reader["CodigoPostal"].ToString();
                    productor.Ciudad = reader["Ciudad"].ToString();
                    productor.Telefono = reader["Telefono"].ToString();
                    productor.RFC = reader["RFC"].ToString();
                    Productores.Add(productor);
                }
                reader.Close();

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

            return Productores;
        }
    }
}
