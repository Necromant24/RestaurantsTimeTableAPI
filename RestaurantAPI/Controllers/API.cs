using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RestaurantAPI.otherClasses;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class API : Controller
    {
        [HttpGet]
        public string GETDayOfWeek(string date)
        {
            Console.WriteLine(date+ " is date !!!!!!!!!!!");
            int year, month, day;
            year = Convert.ToInt32(date[0].ToString() + date[1] + date[2] + date[3]);
            month = Convert.ToInt32(date[5].ToString() + date[6]);
            day = Convert.ToInt32(date[8].ToString() + date[9]);
            string weekDay = Tests.getDayOfWeek(year, month, day);
            return "hello" + weekDay;
        }

        [HttpPost]
        public JsonResult POSTWeekDays([FromBody] Dates dates)
        {
            Console.WriteLine("POSTING...");
            var date_day = new Dictionary<string,string>();
            foreach (var date in dates.dates)
            {
                date_day[date] = Tests.getDayOfWeek(date);
            }

            return Json(date_day);
        }
        
    }

    public class Dates
    {
        public string[] dates { get; set; }
    }
    
}