using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.Home
{
    public class HomeController : Controller
    {
        [HttpGet("/home")]
        [AllowAnonymous]
        public IActionResult Index(string username)
        {
            return View();
        }
    }
}