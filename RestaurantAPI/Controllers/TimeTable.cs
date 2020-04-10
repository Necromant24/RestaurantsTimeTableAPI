﻿using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantAPI.Models;
using RestaurantAPI.otherClasses;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeTable : Controller
    {
        

        [HttpGet]
        public JsonResult GetRaspisanie([FromQuery]Raspisanie raspisanie)
        {
            //var datetime = new DateTime(date);
            var dt = DateTime.Parse(raspisanie.Date);
            
            Console.WriteLine(raspisanie.DaysCount);
            Console.WriteLine(raspisanie.Date);
            Console.WriteLine(raspisanie.Restaurant);
            Console.WriteLine(raspisanie.CoockType);
            
            var table = DataBase.DB.GetWorkersTable(
                raspisanie.Date,raspisanie.DaysCount,
                raspisanie.CoockType,raspisanie.Restaurant);

            return Json(table);
        }
        
        
        [HttpPut]
        public string PutWorkerInTable([FromBody]TableWorkers tableWorker)
        {
            
            DataBase.DB.AddWorkerInTable(tableWorker.CoockType,
                tableWorker.Restaurant, tableWorker.Date,
                tableWorker.Workers);
            return "ok";
        }
    }
}