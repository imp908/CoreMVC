
using Microsoft.Extensions.Configuration;
using System.IO;

namespace crmvcsb.Infrastructure.InMemmory
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
