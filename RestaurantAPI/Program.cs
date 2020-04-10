using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantAPI.otherClasses;

namespace RestaurantAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Console.WriteLine(Tests.CreateSQLStr(Tests.testsql,Tests.testParams));
            //return;
            
            Console.WriteLine(Tests.getDayOfWeek(2020,04,02));
            DataBase.DB.Connect();
            DataBase.DB.InitDB();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}