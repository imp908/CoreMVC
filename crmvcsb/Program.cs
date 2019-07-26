﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;



namespace crmvcsb
{
    public class Program
    {
        public static void Main(string[] args)
        {      
            NewOrderInitializer.Initialize();
            NewOrderInitializer.CleanUp();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //http host for Fidler http test
            .UseUrls("http://localhost:5002")
                .UseStartup<Startup>();
    }
}