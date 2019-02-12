﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SEO.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseUrls("http://localhost:60000/")
                .Build();

            host.Run();
        }
    }
}
