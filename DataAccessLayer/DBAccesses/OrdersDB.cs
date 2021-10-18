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
        public int AddOrder(int idCustomer, int idCourier, int idArea, List<Dish> dishes, string deliveryAddress)
        {
            throw new NotImplementedException();
        }

        public void SetOrderToDelivered(int idOrder)
        {
            throw new NotImplementedException();
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
                            order.IdCustomer = (int) dr["ID_customer"];
                            order.IdCourier = (int) dr["ID_courrier"];
                            order.IdArea = (int)dr["ID_area"];
                            order.ExpectedDeliveryTime = (DateTime)dr["expectedDeliverTime"];
                            order.TimeOfOrder = (DateTime)dr["timeOfOrder"];
                            order.TimeOfDelivery = (DateTime)dr["timeOfDelivery"];
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            order.OrderTotal = (int) dr["orderTotal"];

                            orders.Add(order);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing orders for customer "+idCustomer+": " + e.Message);
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
                    string query = "SELECT * FROM ORDER WHERE ID_courrier=@idCourier;";
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
                            order.ExpectedDeliveryTime = (DateTime)dr["expectedDeliverTime"];
                            order.TimeOfOrder = (DateTime)dr["timeOfOrder"];
                            order.TimeOfDelivery = (DateTime)dr["timeOfDelivery"];
                            order.DeliveryAddress = (string)dr["delivery_address"];
                            order.OrderTotal = (int)dr["orderTotal"];

                            orders.Add(order);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while accessing couriers: " + e.Message);
            }

            return orders;
        }

        public List<Order> GetAllOrdersByRestaurant(int idRestaurant)
        {
            throw new NotImplementedException();
        }
    }
}
