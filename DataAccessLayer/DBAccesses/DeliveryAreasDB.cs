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
    public class DeliveryAreasDB : IDeliveryAreasDB
    {
        private IConfiguration Configuration { get; }
        public DeliveryAreasDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int AddDeliveryArea(string name, int postcode)
        {
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            DeliveryArea deliveryArea = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            DeliveryArea deliveryArea = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
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

        public List<DeliveryArea> GetAllDeliveryAreas()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<DeliveryArea> deliveryAreas = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Delivery_Areas;";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (deliveryAreas == null)
                                deliveryAreas = new List<DeliveryArea>();

                            DeliveryArea deliveryArea = new DeliveryArea();

                            deliveryArea.IdArea = (int)dr["ID_area"];
                            deliveryArea.Name = (string)dr["name"];
                            deliveryArea.Postcode = (int)dr["postcode"];

                            deliveryAreas.Add(deliveryArea);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing all delivery areas: " + e.Message);
            }

            return deliveryAreas;
        }

        public DeliveryArea GetDeliveryAreaById(int id)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            DeliveryArea deliveryArea = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Delivery_Areas WHERE ID_AREA = @id;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

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
                Console.WriteLine("Exception occurred while accessing deliveryArea " + id + ": " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return deliveryArea;
        }
    }
}
