using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using DataAccessLayer.DBAccesses;

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

            DishesDB ddb = new DishesDB();

            

        }
    }
}
