using GGMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GGMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Action([FromBody] string test) 
        {
            Console.WriteLine(test);


            return Ok(" <div class=\"card\">\r\n        <img src=\"https://via.placeholder.com/150\" alt=\"Card Image\" class=\"card-img\">\r\n        <div class=\"card-content\">\r\n            <h2 class=\"card-title\">Card Title</h2>\r\n            <p class=\"card-description\">This is a description of the card. It provides some information about the content of the card.</p>\r\n            <button class=\"card-button\">Click Me</button>\r\n        </div>\r\n    </div>");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
