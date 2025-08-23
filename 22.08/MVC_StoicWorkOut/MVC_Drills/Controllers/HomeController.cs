using Microsoft.AspNetCore.Mvc;

namespace MVC_Drills.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
       {
           return View();
        }




    }
}
