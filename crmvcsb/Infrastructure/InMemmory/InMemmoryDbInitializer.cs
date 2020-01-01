using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using System.IO;

namespace mvccoresb.Infrastructure.InMemmory
{
    public class InMemmoryDbInitializer
    {
        public static IConfigurationRoot configuration { get; set; }
        
        public static void Initialize()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("CostControlDb");

        }
    }
}
