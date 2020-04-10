using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.otherClasses
{
    public class Tests
    {
        
        static Dictionary<DayOfWeek,string> weekDays = new Dictionary<DayOfWeek,string>()
        {
            {DayOfWeek.Monday,"Monday"},
            {DayOfWeek.Tuesday,"Tuesday"},
            {DayOfWeek.Wednesday,"Wednesday"},
            {DayOfWeek.Thursday,"Thursday"},
            {DayOfWeek.Friday,"Friday"},
            {DayOfWeek.Saturday,"Saturday"},
            {DayOfWeek.Sunday,"Sunday"}
        };
        
        
        public static string getDayOfWeek(int year,int month,int day)
        {
            var dateTime = new DateTime(year,month,day);
            return weekDays[dateTime.DayOfWeek];
        }
        
        public static string getDayOfWeek(string date)
        {
            int year, month, day;
            year = Convert.ToInt32(date[0].ToString() + date[1] + date[2] + date[3]);
            month = Convert.ToInt32(date[5].ToString() + date[6]);
            day = Convert.ToInt32(date[8].ToString() + date[9]);
            string weekDay = Tests.getDayOfWeek(year, month, day);
            var dateTime = new DateTime(year,month,day);
            return weekDays[dateTime.DayOfWeek];
            
        }

        public static string CreateSQLStr(string sql, Dictionary<string, string> param)
        {
            var list = param.Keys.ToArray();
            foreach (var key in list)
            {
                var old = "@" + key;
                Console.WriteLine(old);
                sql = sql.Replace(old, param[key]);
            }
            

            return sql;
        }

        public static string testsql =
            "UPDATE @coockType SET workers='@workers' WHERE date_week='@date' AND restaurant='@restaurant'; INSERT INTO rus (restaurant, date_week, workers) SELECT '@restaurant','@date','@workers' WHERE NOT EXISTS(SELECT 1 FROM rus WHERE date_week='@date');";

        public static Dictionary<string,string> testParams = new Dictionary<string, string>()
        {
            {"workers","{\"dominator\":\"5/3\"}"},
            {"restaurant","loc"},
            {"date","2020-04-01"}
        };
        
        public static Dictionary<string,string> myParams = new Dictionary<string, string>()
        {
            {"workers",""},
            {"restaurant",""},
            {"date",""},
            {"coockType",""}
        };


    }
}