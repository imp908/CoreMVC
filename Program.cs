using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;



namespace mvccoresb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try{
//                NetPlatformCheckers.Check.GO();
                NetPlatformCheckers.AsyncCheck.GO();
                CreateWebHostBuilder(args).Build().Run();
            }
            catch(Exception e)
            {

            }
            // Task.Run(async () =>
            // {
            //     await NetPlatformCheckers.AsyncCheck.GO_async();
            // });
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //http host for Fidler http test
            .UseUrls("http://localhost:5002")
                .UseStartup<Startup>();
    }
}
