using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DBAccesses
{
    public class CouriersDB : ICouriersDB
    {
        private IConfiguration Configuration { get; }
        public CouriersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int AddCourier(int idArea, string firstName, string lastName, string emailAddress, string pwdHash, string salt)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Courriers (ID_area, firstName, lastName, emailAddress, pwdHash, salt)" +
                                   " VALUES (@idArea, @firstName, @lastName, @emailAddress, @pwdHash, @salt)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idArea", idArea);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@pwdHash", pwdHash);
                    cmd.Parameters.AddWithValue("@salt", salt);

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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Courier> couriers = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Courriers WHERE ID_area=@idArea;";
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
                            courier.PwdHash = (string)dr["password"];

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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Courier courier = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Courriers WHERE ID_courrier=@idCourier;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCourier", idCourier);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            courier = new Courier();

                            courier.IdCourier = (int)dr["ID_courrier"];
                            courier.IdArea = (int)dr["ID_area"];
                            courier.FirstName = (string)dr["firstName"];
                            courier.LastName = (string)dr["lastName"];
                            courier.EmailAddress = (string)dr["emailAddress"];
                            courier.PwdHash = (string)dr["pwdHash"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing courier " + idCourier + ": " + e.Message);
            }

            return courier;
        }

        public List<Courier> GetAllCouriers()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Courier> couriers = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Courriers;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    
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
                            if(dr["pwdHash"]!=DBNull.Value)
                                courier.PwdHash = (string)dr["pwdHash"];
                            if (dr["salt"] != DBNull.Value)
                                courier.Salt = (string)dr["salt"];

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

        public Courier GetCourierByLogin(string emailAddress, string pwdHash)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Courier courier = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Courriers WHERE pwdHash=@pwdHash AND emailAddress = @emailAddress;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@pwdHash", pwdHash);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            courier = new Courier();

                            courier.IdCourier = (int)dr["ID_courrier"];
                            courier.IdArea = (int)dr["ID_area"];
                            courier.FirstName = (string)dr["firstName"];
                            courier.LastName = (string)dr["lastName"];
                            courier.EmailAddress = (string)dr["emailAddress"];
                            courier.PwdHash = (string)dr["pwdHash"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing courier " + emailAddress + ": " + e.Message);
            }

            return courier;
        }
    }
}
