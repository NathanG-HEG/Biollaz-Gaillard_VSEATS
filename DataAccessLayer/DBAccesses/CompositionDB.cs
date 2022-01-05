using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DataAccessLayer.DBAccesses
{
    /// <summary>
    /// CompositionDB is used to manage the sql operations related to the compositions. Compositions comport a reference to a dish, a quantity and a reference to an order.
    /// </summary>
    public class CompositionDB : ICompositionDB
    {
        private IConfiguration Configuration { get; }

        public CompositionDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// AddComposition is responsible for inserting one composition at a time into Compose table.
        /// </summary>
        /// <param name="idDish">The dish's primary key</param>
        /// <param name="idOrder">The order's primary key</param>
        /// <param name="quantity">The chosen dish's quantity</param>
        /// <returns>The number of rows affected</returns>
        public int AddComposition(int idDish, int idOrder, int quantity)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
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

        /// <summary>
        /// Deletes all compositions related to a specified order.
        /// </summary>
        /// <param name="idOrder">Order's primary key</param>
        /// <returns>The number of rows affected</returns>
        public int DeleteCompositionsByOrder(int idOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE Compose WHERE @idOrder = ID_order;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while removing composition by order: " + e.Message);
            }

            return result;
        }

        /// <summary>
        /// Gets all compositions related to a specified order.
        /// </summary>
        /// <param name="idOrder">Order's primary key</param>
        /// <returns>A list of composition object</returns>
        public List<Composition> GetCompositionsByOrder(int idOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Composition> compositions = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
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
                Console.WriteLine("Exception occurred while accessing compositions for " + idOrder + ": " + e.Message);
            }

            return compositions;
        }

    }
}
