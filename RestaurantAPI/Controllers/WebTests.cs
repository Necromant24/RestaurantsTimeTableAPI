using System.Net.Mime;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebTests:Controller
    {
        [HttpGet]
        public ContentResult Index()
        {
            return new ContentResult{
                ContentType = "text/html",
                Content= System.IO.File.ReadAllText("C:/Users/асеr/RiderProjects/RestaurantAPI/RestaurantAPI/Views/WebTests/Test.html")
                };
        }
    }
}