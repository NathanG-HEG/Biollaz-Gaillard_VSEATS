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
            CouriersDB cdb = new CouriersDB(Configuration);

            //cdb.AddCourier(1, "jl", "Ruço", "d@gmail.com", "dogPWD");

           // List <Courier> couriers = cdb.GetAllCouriersByArea(1);

            //foreach (var c in couriers)
            //{
              //  Console.WriteLine(c);
            //}

            Courier c = cdb.GetCourierByLogin("d@gmail.com", "dogPWD");
            Console.WriteLine(c);

        }
    }
}
