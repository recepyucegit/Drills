using Microsoft.AspNetCore.Mvc;
using MVC_Tekrar.Models.Northwind;

namespace MVC_Tekrar.Controllers
{
    public class ProductController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }




        //Ürün listeleme sayfası
        public IActionResult Index()
        {
            ViewData["Title"] = "Product/Index";

           
            var products = _context.Products.ToList();
            return View(products);
        }


        //Ürün oluşturma sayfası
        public IActionResult Create()
        {
            ViewData["Title"] = "Product/Create";
            return View();
        }


        //Ürün güncelleme sayfası
        public IActionResult Update()
        {
            ViewData["Title"] = "Product/Update";
            return View();
        }

        //Ürün silme işlemi
        public IActionResult Delete()
        {
            return View();
        }

    }
}
