using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DBAccesses
{
    /// <summary>
    /// CourierDB is used to manage the sql operations related to the delivery men.
    /// </summary>
    public class CouriersDB : ICouriersDB
    {
        private IConfiguration Configuration { get; }
        public CouriersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Inserts a courier into its table.
        /// </summary>
        /// <param name="idArea">Delivery area where the courier work</param>
        /// <param name="firstName">First name of the courier</param>
        /// <param name="lastName">Last name of the courier</param>
        /// <param name="emailAddress">Email address of the courier</param>
        /// <param name="pwdHash">The hashed key of the password</param>
        /// <param name="salt">The salt used to hash this courier's password</param>
        /// <returns>The number of rows affected</returns>
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
        /// <summary>
        /// Gets all courier related to a specified delivery area.
        /// </summary>
        /// <param name="idArea">Delivery area's primary key</param>
        /// <returns>A List of courier object</returns>
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
                            courier.PwdHash = (string)dr["pwdHash"];

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

        /// <summary>
        /// Gets a courier with a specified id.
        /// </summary>
        /// <param name="idCourier">The requested courier's id</param>
        /// <returns>A courier object</returns>
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

        /// <summary>
        /// Gets all courier
        /// </summary>
        /// <returns>A list of courier</returns>
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
                            courier.PwdHash = (string)dr["pwdHash"];
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

        /// <summary>
        /// Gets a courier using his password's hash key and email address
        /// </summary>
        /// <param name="emailAddress">The courier's email address</param>
        /// <param name="pwdHash">The courier's password's hash key</param>
        /// <returns>A courier object</returns>
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
