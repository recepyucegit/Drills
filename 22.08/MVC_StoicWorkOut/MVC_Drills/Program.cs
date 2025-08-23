//AspNet Core Empty

//Services
var builder = WebApplication.CreateBuilder(args);
//Ekstra eklemek istediðimiz servisler olacaksa o halde builder.Services.Add() metodu kullanarak birden fazla servis dahil edebiliriz.


//MVC Servisi
//.AddMvc();
//.AddControllersWithViews();
builder.Services.AddControllersWithViews();

//Applications
var app = builder.Build();

//Route
//app.MapGet("/", ()=>
//{

//    //Log kayýtlarý
//    Console.WriteLine("sayfa açýldý!");
//});

//app.MapGet("/about", () =>
//{

//    //Log kayýtlarý
//    Console.WriteLine("hakkýmýzda sayfa açýldý!");
//});

//app.MapGet("/contact", () =>
//{

//    //Log kayýtlarý
//    Console.WriteLine("iletiþim sayfasý açýldý!");
//});


//Routing
app.UseRouting(); //url isteklerini ilgili controller'lara yönlendirmek için

app.UseEndpoints(x =>
{
    x.MapDefaultControllerRoute();
    //https://localhost:7036/Home/Index
    //https://localhost:7036/Product/Index

    //ilk parametre Controller parametresidir.
    //ikinci parametre metot parametresidir.


});


app.Run();

//todo: User klasöründe Create iþlemini düzenle ve  Ýndex sayfasýný deðiþtir (Hocanýn yaptýðý gibi deðiþtir).
