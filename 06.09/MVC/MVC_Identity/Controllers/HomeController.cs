using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Identity.Models;
using MVC_Identity.Models.ViewModels;

namespace MVC_Identity.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    //Kullanýcý Kayýt Sayfasý
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM  registerUser)
    {

        IdentityUser user = new IdentityUser();
        user.UserName = registerUser.Username;
        user.Email = registerUser.Email;
     
        //kullanýcý kayýt iþlemi için _userManager kullanýlacak.
      var result=await _userManager.CreateAsync(user,registerUser.Password);

        //eðer cevap baþarýlý sonuç veriyorsa/vermiyorsa
        if (result.Succeeded)
        {
            TempData["success"] = "kullanýcý oluþturuldu!";
            return RedirectToAction("Index", "Home");
        }
        TempData["error"]="hata meydana geldi";

        return View();
    }

    //Kullanýcý Giriþ Sayfasý
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        //öncelikle gele nkullanýcý adý veritabanýnda var mý?
      var user=await  _userManager.FindByEmailAsync(loginVM.Email);

        if (user != null)
        {
            //eðer varsa þifresi doðru mu?
          var result= await _signInManager.PasswordSignInAsync(user, loginVM.Password, false,false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }
        else
        {
            return View();
        }
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
