using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestaurantAPI.Controllers;
using RestaurantAPI.Models;

namespace RestaurantAPI.otherClasses
{
    public partial class DataBase
    {
        
        struct WorkerTimeTable
        {
            public string TimeTable;
            public string Name;
        }

        public dynamic GetWorkersTable(string date, int count, string typeCoock, string restaurant)
        {
            
            
            var dateWorkers = new Dictionary<string,WorkerTimeTable[]>();
            var dateWorkersStr = new Dictionary<string,string>();
            
            var datetime = DateTime.Parse(date);
            var startDate = datetime.Date.ToString();
            var endDate = datetime.AddDays(count).Date.ToString();


            startDate = startDate.Split()[0].Replace('.', '-');
            endDate = endDate.Split()[0].Replace('.', '-');
            
            
            var sql =
                "SELECT date_week,workers FROM @typeCoock WHERE restaurant='@restaurant' AND date_week BETWEEN '@startDate' AND '@endDate'";
            
            
            var myParams = new Dictionary<string,string>()
            {
                {"typeCoock", typeCoock},
                {"restaurant", restaurant},
                {"startDate", startDate},
                {"endDate", endDate}
            };

            sql = Tests.CreateSQLStr(sql, myParams);
            
            Console.WriteLine(sql);

            cmd.CommandText = sql;
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                dateWorkersStr[reader.GetDate(0).ToString()] =
                    reader.GetString(1);
            }
            
            reader.Close();
            
            Dictionary<string,Dictionary<string,string>> myVar = new Dictionary<string, Dictionary<string,string>>();

            Dictionary<string,DateData> column = new Dictionary<string, DateData>();
            
            foreach (var val in dateWorkersStr.Keys)
            {
                Console.WriteLine(val);
                Console.WriteLine(JsonConvert.DeserializeObject<Dictionary<string,string>>(dateWorkersStr[val]));
                var kv = JsonConvert.DeserializeObject<Dictionary<string, string>>(dateWorkersStr[val]);
                myVar[val] = kv;
                
                column[val]=new DateData(){Workers = kv.Keys.ToArray(),WeekDay = DateTime.Parse(val).Date.DayOfWeek.ToString()};
                
            }

            return column;
        }
        
        public void AddWorkerInTable(string coockType,string restaurant,string date,string workers)
        {
            Tests.myParams["date"] = date;
            Tests.myParams["workers"] = workers;
            Tests.myParams["restaurant"] = restaurant;
            Tests.myParams["coockType"] = coockType;
            
            var sql = Tests.CreateSQLStr(Tests.testsql, Tests.myParams);
            cmd.CommandText = sql;

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
    }
}