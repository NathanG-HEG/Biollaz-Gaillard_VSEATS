using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DBAccesses
{
    public class CustomersDB : ICustomersDB
    {
        public int AddCustomer(string firstname, string lastname, string emailAddress, string password)
        {
            string connectionString = Connection.GetConnectionString();
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Customers (firstName, lastName, emailAddress, password) VALUES (@firstName, @lastName, @emailAddress, @password)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@firstName", firstname);
                    cmd.Parameters.AddWithValue("@lastName", lastname);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@password", password);

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

        public Customer GetCustomerById(int idCustomer)
        {
            string connectionStrings = Connection.GetConnectionString();
            Customer customer = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
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
                            customer.Password = (string)dr["password"];

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing courier " + idCustomer + ": " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return customer;
        }

        public Customer GetCustomerByLogin(string emailAddress, string password)
        {
            string connectionStrings = Connection.GetConnectionString();
            Customer customer = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM Customers WHERE password=@password AND emailAddress = @emailAddress;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@password", password);

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
                            customer.Password = (string)dr["password"];
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
