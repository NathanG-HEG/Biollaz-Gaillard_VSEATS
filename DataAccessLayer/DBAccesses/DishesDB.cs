using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DBAccesses
{
    /// <summary>
    /// DishesDB is used to manage the sql operations related to the dishes.
    /// </summary>
    public class DishesDB : IDishesDB
    {
        private IConfiguration Configuration { get; }
        public DishesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Adds a new dish in Dishes table.
        /// </summary>
        /// <param name="idRestaurant">Restaurant's primary key</param>
        /// <param name="name">Dish name</param>
        /// <param name="price">Dish price</param>
        /// <returns>The numbers of rows affected</returns>
        public int AddDish(int idRestaurant, string name, int price)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString)) 
                {
                    string query = "INSERT INTO Dishes (ID_restaurant, name, price, isAvailable) VALUES (@idRestaurant, @name, @price, @isAvailable)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@isAvailable", true);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while adding dish "+ name + ": " + e.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets all dishes related to a specified restaurant.
        /// </summary>
        /// <param name="idRestaurant">Restaurant's primary key</param>
        /// <returns>A list of dish object</returns>
        public List<Dish> GetAllDishesByRestaurant(int idRestaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Dish> dishes = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dishes WHERE ID_Restaurant=@idRestaurant;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dishes == null)
                                dishes = new List<Dish>();

                            Dish dish = new Dish();

                            dish.IdDish = (int) dr["ID_dish"];
                            dish.IdRestaurant = (int) dr["ID_restaurant"];
                            dish.Name = (string) dr["name"];
                            dish.Price = (int) dr["price"];
                            dish.IsAvailable = (bool) dr["isAvailable"];

                            if (dr["image"] != DBNull.Value)
                                dish.Image = (string) dr["image"];

                            dishes.Add(dish);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing dishes: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return dishes;
        }

        /// <summary>
        /// Gets a dish with a specified id.
        /// </summary>
        /// <param name="idDish">Dish id</param>
        /// <returns>A dish object</returns>
        public Dish GetDishById(int idDish)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Dish dish = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dishes WHERE ID_dish=@idDish;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idDish", idDish);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            dish = new Dish();

                            dish.IdDish = (int)dr["ID_dish"];
                            dish.IdRestaurant = (int)dr["ID_restaurant"];
                            dish.Name = (string)dr["name"];
                            dish.Price = (int)dr["price"];
                            dish.IsAvailable = (bool)dr["isAvailable"];

                            if (dr["image"] != DBNull.Value)
                                dish.Image = (string) dr["image"];
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing dish " + idDish + ": " + e.Message);
            }

            return dish;
        }

        /// <summary>
        /// Sets a dish availability (true of false).
        /// </summary>
        /// <param name="idDish">Dish id</param>
        /// <param name="isAvailable">Dish availability (true of false)</param>
        /// <returns>The number of row affected</returns>
        public int SetAvailability(int idDish, bool isAvailable)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Dishes " +
                                   "SET IsAvailable = @isAvailable " +
                                   "WHERE ID_dish = @idDish;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idDish", idDish);
                    cmd.Parameters.AddWithValue("@isAvailable", isAvailable);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while setting dish status: " + e.Message);
            }
            return result;
        }

        /// <summary>
        /// Sets the price of a dish. Be careful, prices are stored in cents.
        /// </summary>
        /// <param name="idDish">Dish id</param>
        /// <param name="price">Dish price in cents</param>
        /// <returns></returns>
        public int SetPrice(int idDish, int price)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Dishes " +
                                   "SET price = @price " +
                                   "WHERE ID_dish = @idDish;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idDish", idDish);
                    cmd.Parameters.AddWithValue("@price", price);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while setting dish price: " + e.Message);
            }
            return result;
        }
    }
}
