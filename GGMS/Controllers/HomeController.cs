using GGMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GGMS.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
