using Microsoft.AspNetCore.Mvc;
using MVC_Drills.Models.StreetliftingModels;

namespace MVC_Drills.Controllers
{
    public class UserController : Controller
    {
        StreetliftingContext _contex=new StreetliftingContext();
        public IActionResult Index()
        {
            var users= _contex.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Create(User user)
        //{
        //    //Burada veritabanına kayıt işlemi yapılacak
        //   _contex.Users.Add(user);
        //      _contex.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        //[HttpGet]
        //public IActionResult GetUserById()
        //{
        //    return View();
        //}




        //[HttpPost]
        //public IActionResult GetUserById(int id)
        //{
        //    var user = _contex.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}




    }
}
