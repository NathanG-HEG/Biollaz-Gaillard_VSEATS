using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.DBAccesses
{
    public class OrdersDB : IOrdersDB
    {
        public int AddOrder(int idCustomer, int idCourier, int idArea, DateTime expectedDeliveryTime, string deliveryAddress)
        {
            string connectionString = Connection.GetConnectionString();
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Orders (ID_customer, ID_Courrier, ID_area, ExpectedDeliveryTime, TimeOfOrder, Delivery_Address)" +
                                   " VALUES (@idCustomer, @idCourier, @idArea, @expectedDeliveryTime, @timeOfOrder, @deliveryAddress);";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idCustomer", idCustomer);
                    cmd.Parameters.AddWithValue("@idCourier", idCourier);
                    cmd.Parameters.AddWithValue("@idArea", idArea);
                    cmd.Parameters.AddWithValue("@expectedDeliveryTime", expectedDeliveryTime);
                    cmd.Parameters.AddWithValue("@timeOfOrder", DateTime.Now);
                    cmd.Parameters.AddWithValue("@deliveryAddress", deliveryAddress);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while adding order: " + e.Message);
            }

            return result;
        }

        public Order GetOrderById(int orderID)
        {
            string connectionStrings = Connection.GetConnectionString();
            Order order = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
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

                            if (dr["timeOfDelivery"] != null)
                            {
                                order.TimeOfDelivery = (DateTime?)dr["timeOfDelivery"];
                            }
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            if (dr["orderTotal"] != null)
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
            throw new NotImplementedException();
        }

        public int SetOrderToDelivered(int idOrder)
        {
            string connectionString = Connection.GetConnectionString();
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



        public List<Order> GetAllOrdersByCustomer(int idCustomer)
        {
            string connectionStrings = Connection.GetConnectionString();
            List<Order> orders = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM ORDER WHERE ID_customer=@idCustomer;";
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

                            if (dr["timeOfDelivery"] != null)
                            {
                                order.TimeOfDelivery = (DateTime?)dr["timeOfDelivery"];
                            }
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            if (dr["orderTotal"] != null)
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
            }

            return orders;
        }

        public List<Order> GetAllOrdersByCourier(int idCourier)
        {
            string connectionStrings = Connection.GetConnectionString();
            List<Order> orders = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM Orders WHERE ID_courrier=@idCourier;";
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

                            if (dr["timeOfDelivery"] != null)
                            {
                                order.TimeOfDelivery = (DateTime?)dr["timeOfDelivery"];
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
            string connectionStrings = Connection.GetConnectionString();
            List<Order> orders = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionStrings))
                {
                    string query = "SELECT * FROM Orders o " +
                                   "INNER JOIN Compose com ON o.ID_order = com.ID_order " +
                                   "INNER JOIN Dishes d ON d.ID_dish = com.ID_dish" +
                                   "WHERE d.ID_Restaurant = @idRestaurant";
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

                            if (dr["timeOfDelivery"] != null)
                            {
                                order.TimeOfDelivery = (DateTime?)dr["timeOfDelivery"];
                            }
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            if (dr["orderTotal"] != null)
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
            string connectionString = Connection.GetConnectionString();
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
