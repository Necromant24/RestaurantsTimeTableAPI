using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.otherClasses;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : Controller
    {
        [HttpGet]
        public JsonResult GetAllWorkers()
        {
            var data = new Dictionary<string, Worker[]>();
            data["data"] = DataBase.DB.GetAllWorkers();

            return Json(data);
        }

        [HttpPut]
        public string PutWorker([FromBody]Worker worker)
        {
            Console.WriteLine(worker.FirstName + " shkjhlsgdlw");
            DataBase.DB.AddWorker(worker);
            return "ok";
        }

        [HttpDelete("{name}")]
        public string DeleteWorker(string name)
        {
            Console.WriteLine(name+" to delete");
            DataBase.DB.DeleteWorker(name);
            return "ok";
        }
        
        
    }
}