using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DBAccesses
{
    public class DishesDB : IDishesDB
    {
        public int AddDish(int idRestaurant, string name, int price)
        {
            string connectionString = Connection.GetConnectionString();
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

        public List<Dish> GetAllDishesByRestaurant(int idRestaurant)
        {
            throw new NotImplementedException();
        }

        public Dish GetDishById(int idDish)
        {
            throw new NotImplementedException();
        }

        public void SetAvailability(int idDish, bool isAvailable)
        {
            throw new NotImplementedException();
        }

        public void SetPrice(int idDish, int price)
        {
            throw new NotImplementedException();
        }
    }
}
