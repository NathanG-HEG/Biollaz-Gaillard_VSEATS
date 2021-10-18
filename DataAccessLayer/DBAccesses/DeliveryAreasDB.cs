using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DBAccesses
{
    public class DeliveryAreasDB : IDeliveryAreasDB
    {
        public int AddDeliveryArea(string name, int postcode)
        {
            {
                string connectionString = Connection.GetConnectionString();
                int result = 0;

                try
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Delivery_Areas (name, postcode) VALUES (@name, @postcode)";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@postcode", postcode);
                        cn.Open();

                        result = cmd.ExecuteNonQuery();

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception caught while adding delivery area " + name + ": " + e.Message);
                }
                return result;
            }
        }

        public DeliveryArea GetDeliveryAreaByName(string name)
        {
            string connectionStrings = Connection.GetConnectionString();
            DeliveryArea deliveryArea = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM Delivery_Areas WHERE name = @name;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@name", name);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            deliveryArea = new DeliveryArea();

                            deliveryArea.IdArea = (int)dr["ID_area"];
                            deliveryArea.Name = (string)dr["name"];
                            deliveryArea.Postcode = (int)dr["postcode"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing deliveryArea " + name + ": " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return deliveryArea;
        }

        public DeliveryArea GetDeliveryAreaByPostcode(int postcode)
        {
            string connectionStrings = Connection.GetConnectionString();
            DeliveryArea deliveryArea = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM Delivery_Areas WHERE postcode = @postcode;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@postcode", postcode);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            deliveryArea = new DeliveryArea();

                            deliveryArea.IdArea = (int)dr["ID_area"];
                            deliveryArea.Name = (string)dr["name"];
                            deliveryArea.Postcode = (int)dr["postcode"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing deliveryArea " + postcode + ": " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return deliveryArea;
        }
    }
}
