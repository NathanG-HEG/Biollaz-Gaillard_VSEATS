using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DBAccesses
{
    public class RestaurantsDB : IRestaurantsDB
    {

        public IConfiguration IConfiguration { get; set; }
        public RestaurantsDB(IConfiguration iConfiguration)
        {
            IConfiguration = iConfiguration;
        }

        public int AddRestaurant(int idArea, string name, string emailAddress, string password)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Restaurants (ID_area, name, emailAddress, password) VALUES (@idArea, @name, @emailAddress, @password)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idArea", idArea);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@password", password);

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
                            restaurant.Password = (string)dr["password"];

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

        public Restaurant GetRestaurantByLogin(string emailAddress, string password)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            Restaurant restaurant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants WHERE emailAddress=@emailAddress AND password=@password;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@password", password);

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
                            restaurant.Password = (string)dr["password"];

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

        public List<Restaurant> GetAllRestaurants()
        {
            //string connectionStrings = Connection.GetConnectionString();
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
                            restaurant.Password = (string)dr["password"];
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
                            restaurant.Password = (string)dr["password"];

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

        public int UpdateImage(string path, int idRestaurant)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE restaurants " +
                                   "SET image = @path " +
                                   "WHERE id_restaurant = @idRestaurant;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);
                    cmd.Parameters.AddWithValue("@path", path);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while setting image path: " + e.Message);
            }
            return result;
        }

        public int UpdateLogo(string path, int idRestaurant)
        {
            string connectionString = IConfiguration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE restaurants " +
                                   "SET logo = @path " +
                                   "WHERE id_restaurant = @idRestaurant;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);
                    cmd.Parameters.AddWithValue("@path", path);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while setting logo path: " + e.Message);
            }
            return result;
        }

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
                            restaurant.Password = (string)dr["password"];

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
    }
}
