using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DBAccesses
{
    public class CouriersDB : ICouriersDB
    {
        IConfiguration Configuration { get; }

        public CouriersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public int AddCourier(int idArea, string firstName, string lastName, string emailAddress, string password)
        {
          
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                //string connectionString = "Data Source = 153.109.124.35; Initial Catalog = Biollaz_vsEAT; Integrated Security = False; User Id = 6231db; Password = Pwd46231.; MultipleActiveResultSets = True";
            int result = 0;

                try
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Courriers (ID_area, firstName, lastName, emailAddress, password) VALUES (@idArea, @firstName, @lastName, @emailAddress, @password)";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@idArea", idArea);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                        cmd.Parameters.AddWithValue("@password", password);

                        cn.Open();

                        result = cmd.ExecuteNonQuery();

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception caught while adding courier: " + e.Message);
                }

                return result;
        }

        public List<Courier> GetAllCouriersByArea(int idArea)
        {

            string connectionStrings = Configuration.GetConnectionString("DefaultConnection");
            List<Courier> couriers = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM Courier WHERE ID_area=@idArea GO";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idArea", idArea);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (couriers == null)
                                couriers = new List<Courier>();

                            Courier courier = new Courier();

                            courier.IdCourier = (int)dr["ID_courrier"];
                            courier.IdArea = (int)dr["ID_area"];
                            courier.FirstName = (string)dr["firstName"];
                            courier.LastName = (string)dr["lastName"];
                            courier.EmailAddress = (string)dr["emailAddress"];
                            courier.Password = (string)dr["password"];

                            couriers.Add(courier);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing couriers: " + e.Message);
            }

            return couriers;
        }

    public Courier GetCourierById(int idCourier)
    {
        throw new NotImplementedException();
    }

    public Courier GetCourierByLogin(string emailAddress, string password)
    {
        throw new NotImplementedException();
    }
}
}
