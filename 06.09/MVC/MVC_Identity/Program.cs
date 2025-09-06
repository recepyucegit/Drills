using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Identity.Models.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//AddDbContext
builder.Services.AddDbContext<ProjectContext>();


//Services i�erisinde Identity k�t�phanesinin kullan�m� dahil edildi.
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ProjectContext>();


//Cookie (�erez) Services: �erezler browser i�inde bar�n�rlar.
builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie = new CookieBuilder
    {
        Name = "Yzl3447_Login_Cookie"
    };
    x.LoginPath = new PathString("/Home/Login");
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(1);

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
