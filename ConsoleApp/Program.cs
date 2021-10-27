﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using DataAccessLayer;
using DataAccessLayer.DBAccesses;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)

        {

            /*
             * Sets the total of an order using the 'compose' table.
             * ID of order here, '1'.
             */
            CompositionDB cdb = new CompositionDB();
            DishesDB ddb = new DishesDB();
            OrdersDB odb = new OrdersDB();
            //cdb.AddComposition(1, 1, 5);
            //cdb.AddComposition(2, 1, 1);
            //List<Composition> compositions = cdb.GetCompositionsByOrder(1);

            /*
            int total = 0;
            foreach (var c in compositions)
            {
                Dish dish = ddb.GetDishById(c.ID_Dish);
                total += dish.Price * c.Quantity;
            }
            odb.SetTotal(1, total);*/

            string emailAddress = "bbiollaz@gmail.com";
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex rg = new Regex(pattern);

            Console.WriteLine(rg.IsMatch(emailAddress));

        }
    }
}
