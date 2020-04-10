using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantAPI.Models;
using RestaurantAPI.otherClasses;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Restaurants : Controller
    {
        [HttpGet]
        public JsonResult GetAllRestaurants()
        {
            var data = new Dictionary<string,string[]>();
            data["restaurants"] = DataBase.DB.GetAllRestaurants();
            var jsonedData = Json(data);
            return jsonedData;
        }

        
        [HttpPut]
        public string PutRestaurant([FromBody]Restaurant restaurant)
        {
            Console.WriteLine(restaurant);
            Console.WriteLine("rest req");
            Console.WriteLine(restaurant.RestName);
            DataBase.DB.AddRestaurant(restaurant.RestName);
            return "ok";
        }

        [HttpDelete]
        public string DeleteRestaurant([FromBody]Restaurant restaurant)
        {
            Console.WriteLine("deleting - "+restaurant.RestName);
            DataBase.DB.DeleteRestaurant(restaurant.RestName);
            return "ok";
        }
        
        
        
    }
}