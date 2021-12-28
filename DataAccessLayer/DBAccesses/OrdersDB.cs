using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DataAccessLayer.DBAccesses
{
    public class OrdersDB : IOrdersDB
    {
        private IConfiguration Configuration { get; }
        public OrdersDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int AddOrder(int idCustomer, int idCourier, int idArea, DateTime expectedDeliveryTime, string deliveryAddress)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int idOrder = -1;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Orders (ID_customer, ID_Courrier, ID_area, ExpectedDeliveryTime, TimeOfOrder, Delivery_Address)" +
                                   " VALUES (@idCustomer, @idCourier, @idArea, @expectedDeliveryTime, @timeOfOrder, @deliveryAddress); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCustomer", idCustomer);
                    cmd.Parameters.AddWithValue("@idCourier", idCourier);
                    cmd.Parameters.AddWithValue("@idArea", idArea);
                    cmd.Parameters.AddWithValue("@expectedDeliveryTime", expectedDeliveryTime);
                    cmd.Parameters.AddWithValue("@timeOfOrder", DateTime.Now);
                    cmd.Parameters.AddWithValue("@deliveryAddress", deliveryAddress);

                    cn.Open();

                    idOrder = Convert.ToInt32(cmd.ExecuteScalar());


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while adding order: " + e.Message);
            }

            return idOrder;
        }

        public Order GetOrderById(int orderID)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Order order = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders WHERE ID_Order=@orderID;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@orderID", orderID);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            order = new Order();

                            order.IdOrder = (int)dr["ID_order"];
                            order.IdCustomer = (int)dr["ID_customer"];
                            order.IdCourier = (int)dr["ID_courrier"];
                            order.IdArea = (int)dr["ID_area"];
                            order.ExpectedDeliveryTime = (DateTime)dr["expectedDeliveryTime"];
                            order.TimeOfOrder = (DateTime)dr["timeOfOrder"];

                            if (dr["timeOfDelivery"] != DBNull.Value)
                            {
                                order.TimeOfDelivery = (DateTime)dr["timeOfDelivery"];
                            }
                            else
                            {
                                order.TimeOfDelivery = DateTime.MinValue;
                            }
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            if (dr["orderTotal"] != DBNull.Value)
                            {
                                order.OrderTotal = (int)dr["orderTotal"];
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing order " + orderID + ": " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return order;
        }

        public int DeleteOrder(int idOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE Orders WHERE @idOrder = ID_order;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while removing composition by order: " + e.Message);
            }

            return result;

        }

        public int SetOrderToDelivered(int idOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Orders " +
                                   "SET timeOfDelivery = @timeOfDelivery " +
                                   "WHERE ID_Order = @idOrder;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);
                    cmd.Parameters.AddWithValue("@timeOfDelivery", DateTime.Now);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while setting order status: " + e.Message);
            }
            return result;
        }

        public int SetOrderToUnDelivered(int idOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Orders " +
                                   "SET timeOfDelivery = @timeOfDelivery " +
                                   "WHERE ID_Order = @idOrder;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);
                    cmd.Parameters.AddWithValue("@timeOfDelivery", DBNull.Value);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while setting order status: " + e.Message);
            }
            return result;
        }



        public List<Order> GetAllOrdersByCustomer(int idCustomer)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Order> orders = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ORDERS WHERE ID_customer=@idCustomer ORDER BY EXPECTEDDELIVERYTIME DESC;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCustomer", idCustomer);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (orders == null)
                                orders = new List<Order>();

                            Order order = new Order();
                            order.IdOrder = (int)dr["ID_order"];
                            order.IdCustomer = (int)dr["ID_customer"];
                            order.IdCourier = (int)dr["ID_courrier"];
                            order.IdArea = (int)dr["ID_area"];
                            order.ExpectedDeliveryTime = (DateTime)dr["expectedDeliveryTime"];
                            order.TimeOfOrder = (DateTime)dr["timeOfOrder"];

                            if (dr["timeOfDelivery"] != DBNull.Value)
                            {
                                order.TimeOfDelivery = (DateTime)dr["timeOfDelivery"];
                            }
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            if (dr["orderTotal"] != DBNull.Value)
                            {
                                order.OrderTotal = (int)dr["orderTotal"];
                            }

                            orders.Add(order);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing orders for customer " + idCustomer + ": " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return orders;
        }

        public List<Order> GetAllOrdersByCourier(int idCourier)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Order> orders = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders WHERE ID_courrier=@idCourier ORDER BY EXPECTEDDELIVERYTIME DESC;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCourier", idCourier);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (orders == null)
                                orders = new List<Order>();

                            Order order = new Order();

                            order.IdOrder = (int)dr["ID_order"];
                            order.IdCustomer = (int)dr["ID_customer"];
                            order.IdCourier = (int)dr["ID_courrier"];
                            order.IdArea = (int)dr["ID_area"];
                            order.ExpectedDeliveryTime = (DateTime)dr["expectedDeliveryTime"];
                            order.TimeOfOrder = (DateTime)dr["timeOfOrder"];


                            if (dr["timeOfDelivery"] != DBNull.Value)
                            {
                                order.TimeOfDelivery = (DateTime)dr["timeOfDelivery"];
                            }

                            order.DeliveryAddress = (string)dr["delivery_address"];
                            if (dr["orderTotal"] != DBNull.Value)
                            {
                                order.OrderTotal = (int)dr["orderTotal"];
                            }

                            orders.Add(order);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing orders: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return orders;
        }

        public List<Order> GetAllOrdersByRestaurant(int idRestaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Order> orders = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders o " +
                                   "INNER JOIN Compose com ON o.ID_order = com.ID_order " +
                                   "INNER JOIN Dishes d ON d.ID_dish = com.ID_dish " +
                                   "WHERE d.ID_Restaurant = @idRestaurant;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (orders == null)
                                orders = new List<Order>();

                            Order order = new Order();

                            order.IdOrder = (int)dr["ID_order"];
                            order.IdCustomer = (int)dr["ID_customer"];
                            order.IdCourier = (int)dr["ID_courrier"];
                            order.IdArea = (int)dr["ID_area"];
                            order.ExpectedDeliveryTime = (DateTime)dr["expectedDeliveryTime"];
                            order.TimeOfOrder = (DateTime)dr["timeOfOrder"];

                            if (dr["timeOfDelivery"] != DBNull.Value)
                            {
                                order.TimeOfDelivery = (DateTime)dr["timeOfDelivery"];
                            }
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            if (dr["orderTotal"] != DBNull.Value)
                            {
                                order.OrderTotal = (int)dr["orderTotal"];
                            }

                            orders.Add(order);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing orders: " + e.Message);
            }

            return orders;
        }

        public int SetTotal(int idOrder, int total)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Orders " +
                                   "SET orderTotal = @total " +
                                   "WHERE ID_Order = @idOrder;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@idOrder", idOrder);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while setting order total: " + e.Message);
            }
            return result;
        }

    }
}
