using Microsoft.Extensions.Configuration;
using System;
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

            cdb.AddCourier(1, "David", "Russo", "d@gmail.com", "dogPWD");
        }
    }
}
