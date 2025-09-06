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

    //Kullan�c� Kay�t Sayfas�
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
     
        //kullan�c� kay�t i�lemi i�in _userManager kullan�lacak.
      var result=await _userManager.CreateAsync(user,registerUser.Password);

        //e�er cevap ba�ar�l� sonu� veriyorsa/vermiyorsa
        if (result.Succeeded)
        {
            TempData["success"] = "kullan�c� olu�turuldu!";
            return RedirectToAction("Index", "Home");
        }
        TempData["error"]="hata meydana geldi";

        return View();
    }

    //Kullan�c� Giri� Sayfas�
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        //�ncelikle gele nkullan�c� ad� veritaban�nda var m�?
      var user=await  _userManager.FindByEmailAsync(loginVM.Email);

        if (user != null)
        {
            //e�er varsa �ifresi do�ru mu?
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
