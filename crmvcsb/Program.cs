using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace crmvcsb
{
    public class Program
    {
     
        public static void Main(string[] args)
        {
            try
            {
                KATAS.Miscellaneous.HeapSortArr.GO();
                
                KATAS.Miscellaneous.SortingTests.GO();
                InfrastructureCheckers.Buss.GO();
                NetPlatformCheckers.StringsCheck.GO();
                LINQtoObjectsCheck.LinqCheck.GO();
                KATAS.BraketsChecker.GO();
                NetPlatformCheckers.Operators.GO();
                var bld = CreateWebHostBuilder(args).Build();
                bld.Run();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            })
            //http host for Fidler http test
            .UseUrls("http://localhost:5002")
            .UseStartup<Startup>();

    }
}
