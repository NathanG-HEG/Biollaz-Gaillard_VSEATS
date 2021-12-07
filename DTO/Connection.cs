using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public abstract class Connection
    {
        private static readonly string connectionString = "Data Source = 153.109.124.35; Initial Catalog = Biollaz_vsEAT; Integrated Security = False; User Id = 6231db; Password = Pwd46231.; MultipleActiveResultSets = True";

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}
