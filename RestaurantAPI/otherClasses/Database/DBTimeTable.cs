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
        
        //delegate string[] weekForDate = delegate(string date) { };

        IEnumerable<string> weekForDate(string date)
        {
            var startDate = DateTime.Parse(date);
            for (int i = 0; i < 7; i++)
            {
                yield return reformatDate(startDate.AddDays(i).ToString());
            }
        }

        public dynamic GetWorkersTable(string date, int count, string typeCoock, string restaurant)
        {




            var week = weekForDate(date).ToArray();
            
            
            
            
            
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
            
            
            Console.WriteLine(dateWorkersStr.Keys.Count);
            Console.WriteLine(" - getted from sql");
            
            reader.Close();
            
            Dictionary<string,Dictionary<string,string>> myVar = new Dictionary<string, Dictionary<string,string>>();

            Dictionary<string,DateData> column = new Dictionary<string, DateData>();
            
            foreach (var val in dateWorkersStr.Keys)
            {
                //Console.WriteLine(val);
                //Console.WriteLine(JsonConvert.DeserializeObject<Dictionary<string,string>>(dateWorkersStr[val]));
                var kv = JsonConvert.DeserializeObject<Dictionary<string, string>>(dateWorkersStr[val]);
                myVar[val] = kv;
                
                column[val]=new DateData(){Workers = kv.Keys.ToArray(),WeekDay = DateTime.Parse(val).Date.DayOfWeek.ToString()};
                
            }

            //Console.WriteLine(DateTime.Parse(date).ToString()+ " - is ref date raw");
            //Console.WriteLine(reformatDate(DateTime.Parse(date).ToString())+ " - is ref date");

            for (int i = 0; i < 7; i++)
            {
                if (!column.ContainsKey(week[i]))
                {
                    column[week[i]]=new DateData(){WeekDay = DateTime.Parse(date).AddDays(i).Date.DayOfWeek.ToString(),Workers = new string[0]};
                }
            }
            
            
            Console.WriteLine(column.Keys.Count);
            Console.WriteLine(" - column count");
            

            return column;
        }

        string reformatDate(string date)
        {
            var nums = date.Split(" ")[0].Trim();
            var date2 = nums[6..10] + "-" + nums[3..5] + "-" + nums[0..2];
            return date2;
        }
        
        
        
        
        
        
        public void AddWorkerInTable(string coockType,string restaurant,string date,string workers)
        {
            Tests.myParams["date"] = date;
            Tests.myParams["workers"] = workers;
            Tests.myParams["restaurant"] = restaurant;
            Tests.myParams["coockType"] = coockType;
            
            var sql = Tests.CreateSQLStr(Tests.testsql, Tests.myParams);
            Console.WriteLine(sql+ " -is created sql in add worker");
            cmd.CommandText = sql;

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
    }
}