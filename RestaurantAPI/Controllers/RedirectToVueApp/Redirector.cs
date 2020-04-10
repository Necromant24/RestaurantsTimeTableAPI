using System.IO;
using System.Net;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedirectorController:Controller
    {
        [HttpGet]
        public ContentResult Index()
        {
            HttpWebRequest myReq =
                (HttpWebRequest)WebRequest.Create("http://localhost:8080");

            var content = new StreamReader(myReq.GetResponse().GetResponseStream()).ReadToEnd();

            return new ContentResult 
            {
                ContentType = "text/html",
                Content = content
            };
        }

        string MakeRequest(string url)
        {
            HttpWebRequest myReq =
                (HttpWebRequest)WebRequest.Create(url);

            var content = new StreamReader(myReq.GetResponse().GetResponseStream()).ReadToEnd();
            return content;
        }

        private string vueHost = "http://localhost:8080";

    }
}