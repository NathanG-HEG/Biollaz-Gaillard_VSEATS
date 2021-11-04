using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;
using System.Text.RegularExpressions;
using BLL;

namespace ConsoleApp
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        private static Courier userCourier = null;
        private static Customer userCustomer = null;
        private static Restaurant userRestaurant = null;
        private static bool loggedIn = false;


        static void Main(string[] args)

        {
           
            bool running = true;

            Console.WriteLine("Welcome!\nType 'help' for help.");
            while (running)
            {

                string command = Console.ReadLine();
                switch (command)
                {
                    case "help":
                        Help();
                        break;
                    case "signUp":
                        if (!loggedIn)
                            SignUp();
                        else
                            Console.WriteLine("Already logged in!");
                        break;
                    case "login":
                        if (!loggedIn)
                            Login();
                        else
                            Console.WriteLine("Already logged in!");
                        break;
                    case "logout":
                        if (loggedIn)
                            Logout();
                        else
                            Console.WriteLine("Not logged in!");
                        break;
                    case "exit":
                        running = false;
                        break;
                    default:
                        Console.WriteLine(command + " not recognized as a command");
                        break;
                }
            }
        }

        private static void Logout()
        {
            userCourier = null;
            userRestaurant = null;
            userCustomer = null;
            loggedIn = false;
            Console.WriteLine("Logged out!");
        }

        static void SignUp()
        {
            Console.WriteLine("What type of account to create? 'courier', 'customer', 'restaurant'");
            string command = Console.ReadLine();
            switch (command)
            {
                case "courier":
                    NewCourier();
                    break;
                case "customer":
                    NewCustomer();
                    break;
                case "restaurant":
                    NewRestaurant();
                    break;
                default:
                    Console.WriteLine("Invalid command. Bye!");
                    break;
            }
        }

        static void Login()
        {
            Console.Write("Email address: ");
            string emailAddress = Console.ReadLine();
            Console.Write("\nPassword: ");
            string password = Console.ReadLine();
            try
            {
                CustomerManager cum = new CustomerManager();
                userCustomer = cum.GetCustomerByLogin(emailAddress, password);
                loggedIn = true;
                Console.WriteLine("Logged in as customer: "+userCustomer.FirstName+" "+userCustomer.LastName);
            }
            catch (Exception e)
            {
                try
                {
                    CourierManager com = new CourierManager();
                    userCourier = com.GetCourierByLogin(emailAddress, password);
                    loggedIn = true;
                    Console.WriteLine("Logged in as courier: "+userCourier.FirstName+" "+userCourier.LastName);
                }
                catch (Exception e2)
                {
                    try
                    {
                        RestaurantManager rem = new RestaurantManager();
                        userRestaurant = rem.GetRestaurantByLogin(emailAddress, password);
                        loggedIn = true;
                        Console.WriteLine("Logged in as restaurant: "+userRestaurant.Name);
                    }
                    catch (Exception e3)
                    {
                        Console.WriteLine("Incorrect email or password! Bye!");
                        loggedIn = false;
                    }
                }
            }
        }

        static void NewRestaurant()
        {
            Console.WriteLine("TO DO Bye!");
        }

        static void NewCustomer()
        {
            Console.WriteLine("TO DO Bye!");
        }

        static void NewCourier()
        {
            DeliveryAreaManager dam = new DeliveryAreaManager();
            CourierManager com = new CourierManager();
            Console.WriteLine("In what area are you delivering?");
            List<DeliveryArea> areas = dam.GetAllDeliveryAreas();
            foreach (var a in areas)
            {
                Console.WriteLine(a.IdArea + " | " + a.Postcode + " | " + a.Name);
            }
            Console.Write("Id area: ");
            int chosenArea = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nFirst name: ");
            string firstName = Console.ReadLine();
            Console.Write("\nLast name: ");
            string lastName = Console.ReadLine();
            Console.Write("\nEmail address: ");
            string emailAddress = Console.ReadLine();
            Console.Write("\nPassword: ");
            string password = Console.ReadLine();
            try
            {
                com.AddCourier(chosenArea, firstName, lastName, emailAddress, password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("Courier added successfully!");
        }

        static void Help()
        {
            Console.WriteLine("\n##### Miscellaneous #####");
            Console.WriteLine("'signUp': Create a courier, customer or restaurant account");
            Console.WriteLine("'login': Log in");
            Console.WriteLine("'logout' Log out");
            Console.WriteLine("'exit': Exit program");
            Console.WriteLine("\n##### Courier command #####");
            Console.WriteLine("'list': List all current orders");
            Console.WriteLine("'list-a': List all orders");
            Console.WriteLine("'setDelivered': Set an order to delivered");
            Console.WriteLine("\n##### Customer command #####");
            Console.WriteLine("'list': List all current orders");
            Console.WriteLine("'list-a': List all orders");
            Console.WriteLine("'order': Create a new order");
            Console.WriteLine("'cancelOrder': Cancel an order");
            Console.WriteLine("\n##### Restaurant command #####");
            Console.WriteLine("'updateImage': Update restaurant's image");
            Console.WriteLine("'updateLogo': Update restaurant's logo");
            Console.WriteLine("'setAvailability': Set a dish availability");
            Console.WriteLine("'updateDishImage': Update a dish's image");
            Console.WriteLine("'addDish': Add a new dish to your restaurant");
            Console.WriteLine("'list': List all your orders");
        }
    }
}
