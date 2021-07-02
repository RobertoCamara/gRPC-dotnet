using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace POC.GrpcService.ApiServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    //options.ConfigureHttpsDefaults(https =>
                    //{
                    //    https.ServerCertificate = new X509Certificate2();
                    //    //https.ServerCertificate = new X509Certificate2(fileName: @"C:\Users\rober\.aspnet\https\core5-website.pfx", password: "teste");
                    //});

                    options.Listen(IPAddress.Any, 5001, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;                        
                        listenOptions.UseHttps("grpc.pfx", "grpc");                        
                    });                    
                });
                webBuilder.UseStartup<Startup>();
            });

    }
}
