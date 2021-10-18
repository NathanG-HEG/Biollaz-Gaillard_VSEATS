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
            throw new NotImplementedException();
        }
    }
}
