using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test : ControllerBase
    {
        
        
        
        
        [HttpGet]
        public string Get()
        {
            return "hello world!";
        }
    }
}