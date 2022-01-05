using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DBAccesses
{
    /// <summary>
    /// RestaurantsDB is used to manage the sql operations related to the restaurants.
    /// </summary>
    public class RestaurantsDB : IRestaurantsDB
    {

        public IConfiguration IConfiguration { get; set; }
        public RestaurantsDB(IConfiguration iConfiguration)
        {
            IConfiguration = iConfiguration;
        }
        /// <summary>
        /// Adds a restaurant in Restaurants table.
        /// </summary>
        /// <param name="idArea">Delivery area's primary key</param>
        /// <param name="name">Restaurant's name</param>
        /// <param name="emailAddress">Restaurant's email address</param>
        /// <param name="pwdHash">Password's hash key</param>
        /// <param name="salt">Salt used to hash this restaurant's password</param>
        /// <returns>The number of rows affected</returns>
        public int AddRestaurant(int idArea, string name, string emailAddress, string pwdHash, string salt)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Restaurants (ID_area, name, emailAddress, pwdHash, salt)" +
                                   " VALUES (@idArea, @name, @emailAddress, @pwdHash, @salt)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idArea", idArea);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@pwdHash", pwdHash);
                    cmd.Parameters.AddWithValue("@salt", salt);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while adding restaurant: " + e.Message);
            }

            return result;
        }

        /// <summary>
        /// Gets a restaurant with the given name.
        /// </summary>
        /// <param name="name">Restaurant's name</param>
        /// <returns>A restaurant object</returns>
        public Restaurant GetRestaurantByName(string name)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            Restaurant restaurant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants WHERE name=@name;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@name", name);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["ID_restaurant"];
                            restaurant.IdArea = (int)dr["ID_area"];
                            restaurant.Name = (string)dr["name"];
                            restaurant.EmailAddress = (string)dr["emailAddress"];
                            restaurant.PwdHash = (string)dr["pwdHash"];
                            if (dr["image"] != DBNull.Value)
                                restaurant.Image = (string)dr["image"];
                            if (dr["logo"] != DBNull.Value)
                                restaurant.Logo = (string)dr["logo"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing restaurant " + name + ": " + e.Message);
            }

            return restaurant;
        }

        /// <summary>
        /// Gets a restaurant using its email address and password's hash key.
        /// </summary>
        /// <param name="emailAddress">Restaurant's email address</param>
        /// <param name="pwdHash">Password's hash key</param>
        /// <returns>A restaurant object</returns>
        public Restaurant GetRestaurantByLogin(string emailAddress, string pwdHash)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            Restaurant restaurant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants WHERE emailAddress=@emailAddress AND pwdHash=@pwdHash;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@pwdHash", pwdHash);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["ID_restaurant"];
                            restaurant.IdArea = (int)dr["ID_area"];
                            restaurant.Name = (string)dr["name"];
                            restaurant.EmailAddress = (string)dr["emailAddress"];
                            restaurant.PwdHash = (string)dr["pwdHash"];
                            restaurant.Salt = (string) dr["salt"];
                            if (dr["image"] != DBNull.Value)
                                restaurant.Image = (string)dr["image"];
                            if (dr["logo"] != DBNull.Value)
                                restaurant.Logo = (string)dr["logo"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing restaurant " + emailAddress + ": " + e.Message);
            }

            return restaurant;
        }

        /// <summary>
        /// Gets all restaurants.
        /// </summary>
        /// <returns>A list of restaurants</returns>
        public List<Restaurant> GetAllRestaurants()
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            List<Restaurant> restaurants = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants;";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (restaurants == null)
                                restaurants = new List<Restaurant>();

                            Restaurant restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["ID_restaurant"];
                            restaurant.IdArea = (int)dr["ID_area"];
                            restaurant.Name = (string)dr["name"];
                            restaurant.EmailAddress = (string)dr["emailAddress"];
                            restaurant.PwdHash = (string)dr["pwdHash"];
                            restaurant.Salt = (string) dr["salt"];
                            if (dr["image"] != DBNull.Value)
                                restaurant.Image = (string) dr["image"];
                            if (dr["logo"] != DBNull.Value)
                                restaurant.Logo = (string) dr["logo"];

                            restaurants.Add(restaurant);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing all restaurants: " + e.Message);
            }

            return restaurants;
        }

        /// <summary>
        /// Gets all restaurants related to an area.
        /// </summary>
        /// <param name="idArea">Delivery area's primary key</param>
        /// <returns>A list of restaurant object</returns>
        public List<Restaurant> GetAllRestaurantsByArea(int idArea)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            List<Restaurant> restaurants = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants WHERE @idArea = id_Area;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idArea", idArea);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (restaurants == null)
                                restaurants = new List<Restaurant>();

                            Restaurant restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["ID_restaurant"];
                            restaurant.IdArea = (int)dr["ID_area"];
                            restaurant.Name = (string)dr["name"];
                            restaurant.EmailAddress = (string)dr["emailAddress"];
                            restaurant.PwdHash = (string)dr["pwdHash"];
                            if (dr["image"] != DBNull.Value)
                                restaurant.Image = (string)dr["image"];
                            if (dr["logo"] != DBNull.Value)
                                restaurant.Logo = (string)dr["logo"];

                            restaurants.Add(restaurant);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing restaurants in area "+idArea+": " + e.Message);
            }

            return restaurants;
        }
        /// <summary>
        /// Gets the restaurant with the given id.
        /// </summary>
        /// <param name="id">Restaurant's primary key</param>
        /// <returns>A restaurant object</returns>
        public Restaurant GetRestaurantById(int id)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            Restaurant restaurant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants WHERE ID_Restaurant=@id;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["ID_restaurant"];
                            restaurant.IdArea = (int)dr["ID_area"];
                            restaurant.Name = (string)dr["name"];
                            restaurant.EmailAddress = (string)dr["emailAddress"];
                            restaurant.PwdHash = (string)dr["pwdHash"];
                            if (dr["image"] != DBNull.Value)
                                restaurant.Image = (string)dr["image"];
                            if (dr["logo"] != DBNull.Value)
                                restaurant.Logo = (string)dr["logo"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing restaurant " + id + ": " + e.Message);
            }

            return restaurant;
        }

        /// <summary>
        /// Gets a restaurant using an order's primary key.
        /// </summary>
        /// <param name="idOrder">Order's primary key</param>
        /// <returns>A restaurant object</returns>
        public Restaurant GetRestaurantByOrder(int idOrder)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            Restaurant restaurant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query =
                        "SELECT DISTINCT(r.ID_RESTAURANT), r.EMAILADDRESS, r.ID_AREA,  r.IMAGE, r.LOGO, r.NAME, r.pwdHash FROM Restaurants r " +
                        "INNER JOIN Dishes d ON d.ID_Restaurant = r.ID_Restaurant " +
                        "INNER JOIN Compose c ON c.ID_Dish = d.ID_Dish " +
                        "INNER JOIN Orders o ON c.ID_Order = o.ID_Order " +
                        "WHERE o.ID_Order = @idOrder;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["ID_restaurant"];
                            restaurant.IdArea = (int)dr["ID_area"];
                            restaurant.Name = (string)dr["name"];
                            restaurant.EmailAddress = (string)dr["emailAddress"];
                            restaurant.PwdHash = (string)dr["pwdHash"];
                            if (dr["image"] != DBNull.Value)
                                restaurant.Image = (string)dr["image"];
                            if (dr["logo"] != DBNull.Value)
                                restaurant.Logo = (string)dr["logo"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing restaurant using order " + idOrder + ": " + e.Message);
            }
            return restaurant;
        }
    }
}
