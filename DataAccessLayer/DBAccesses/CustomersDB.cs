using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DBAccesses
{
    /// <summary>
    /// CustomerDB is used to manage the sql operations related to the customers.
    /// </summary>
    public class CustomersDB : ICustomersDB
    {
        private IConfiguration Configuration { get; }

        public CustomersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Adds a customer to Customer table.
        /// </summary>
        /// <param name="firstname">Customer's first name</param>
        /// <param name="lastname">Customer's last name</param>
        /// <param name="emailAddress">Customer's email address</param>
        /// <param name="pwdHash">Customer password's hash key</param>
        /// <param name="salt">Salt used to hash this customer's password</param>
        /// <returns>The number of rows affected</returns>
        public int AddCustomer(string firstname, string lastname, string emailAddress, string pwdHash, string salt)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Customers (firstName, lastName, emailAddress, pwdHash, salt)" +
                                   " VALUES (@firstName, @lastName, @emailAddress, @pwdHash, @salt)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@firstName", firstname);
                    cmd.Parameters.AddWithValue("@lastName", lastname);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@pwdHash", pwdHash);
                    cmd.Parameters.AddWithValue("@salt", salt);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while adding customer: " + e.Message);
            }

            return result;
        }
        
        /// <summary>
        /// Gets a customer with a specified id.
        /// </summary>
        /// <param name="idCustomer">The requested customer's id</param>
        /// <returns>A customer object</returns>
        public Customer GetCustomerById(int idCustomer)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Customer customer = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers WHERE ID_customer = @idCustomer;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCustomer", idCustomer);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            customer = new Customer();

                            customer.IdCustomer = (int)dr["ID_customer"];
                            customer.FirstName = (string)dr["firstName"];
                            customer.LastName = (string)dr["lastName"];
                            customer.EmailAddress = (string)dr["emailAddress"];
                            customer.PwdHash = (string)dr["pwdHash"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing customer " + idCustomer + ": " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return customer;
        }

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>A list of customer</returns>
        public List<Customer> GetAllCustomers()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Customer> customers = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (customers == null)
                                customers = new List<Customer>();

                            Customer customer = new Customer();

                            customer.IdCustomer = (int)dr["ID_customer"];
                            customer.FirstName = (string)dr["firstname"];
                            customer.LastName = (string)dr["lastname"];
                            customer.EmailAddress = (string)dr["emailAddress"];
                            customer.PwdHash = (string)dr["pwdHash"];
                            customer.Salt = (string) dr["salt"];

                            customers.Add(customer);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing customers: " + e.Message);
            }

            return customers;
        }

        /// <summary>
        /// Gets a a customer using his email address and password's hash key.
        /// </summary>
        /// <param name="emailAddress">Customer's email address</param>
        /// <param name="pwdHash">Customer's password hash key</param>
        /// <returns></returns>
        public Customer GetCustomerByLogin(string emailAddress, string pwdHash)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Customer customer = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers WHERE pwdHash=@pwdHash AND emailAddress = @emailAddress;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@pwdHash", pwdHash);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            customer = new Customer();

                            customer.IdCustomer = (int)dr["ID_customer"];
                            customer.FirstName = (string)dr["firstName"];
                            customer.LastName = (string)dr["lastName"];
                            customer.EmailAddress = (string)dr["emailAddress"];
                            customer.PwdHash = (string)dr["pwdHash"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing customer " + emailAddress + ": " + e.Message);
            }

            return customer;
        }
    }
}
