using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class js : Controller
    {
        // GET
        [HttpGet("{url}")]
        public ContentResult Js(string url)
        {
            Console.WriteLine(url+" is url");
            HttpWebRequest myReq =
                (HttpWebRequest)WebRequest.Create("http://localhost:8080/js/"+url);

            var content = new StreamReader(myReq.GetResponse().GetResponseStream()).ReadToEnd();

            return new ContentResult 
            {
                ContentType = "text/plain",
                Content = content
            };
        }
    }
}