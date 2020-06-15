using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace API.Models.Core
{
    public  class DataBaseConnection
    {
        public readonly static IConfiguration Configuration;

        static DataBaseConnection() 
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config/appsettings.json", optional: true)
                .Build();
        }
        internal static string SqlConnection
        {
            get { return Configuration.GetConnectionString("ConnString"); }
        }
    }
}
