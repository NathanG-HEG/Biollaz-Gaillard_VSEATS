using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DBAccesses
{
    public class CompositionDB : ICompositionDB
    {
        public int AddComposition(int idDish, int idOrder, int quantity)
        {
            string connectionString = Connection.GetConnectionString();
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Compose (id_Dish, id_Order, quantity) VALUES (@idDish, @idOrder, @quantity)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idDish", idDish);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);
                    cmd.Parameters.AddWithValue("@quantity", quantity);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while adding composition: " + e.Message);
            }

            return result;
        }

        public List<Composition> GetCompositionsByOrder(int idOrder)
        {
            string connectionStrings = Connection.GetConnectionString();
            List<Composition> compositions = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM compose WHERE ID_order=@idOrder;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (compositions == null)
                                compositions = new List<Composition>();

                            Composition composition = new Composition();

                            composition.ID_Dish = (int)dr["ID_dish"];
                            composition.ID_order = (int)dr["ID_order"];
                            composition.Quantity = (int)dr["quantity"];

                            compositions.Add(composition);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing compositions for "+idOrder+": " + e.Message);
            }

            return compositions;
        }
    }
}
