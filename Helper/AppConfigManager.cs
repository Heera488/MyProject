using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FMS.Helpers
{
    public class AppConfigManager
    {
        public static string DbConnectionString;
     
        public AppConfigManager()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
          
            DbConnectionString = root.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
          
        }
     
        public string getConnectionString
        {
            get => DbConnectionString;
        }
  

    }
}
