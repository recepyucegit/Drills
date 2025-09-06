using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Identity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }





        public async Task<IActionResult> Profile()
        {
            var user =await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.User = user;
            //todo: uygulamaya giriş yapan kullanıcının bilgileri.
            return View();
        }
    }
}
