





using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC_Identity.Models.Context
{
    public class ProjectContext:IdentityDbContext
    {

        //DbSet<>: generic olan bu tip içerisine almış olduğu class'ı veritabanında tablo haline dönüştürür.

        //public DbSet<AppUser> Users { get; set; }

        //OnConfiguring => Veritabanı ayarlama işlemleri
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=DESKTOP-J4PTH70;database=MVCIdentityDB;uid=sa;pwd=123;TrustServerCertificate=True;";
            //Eğer veritabanı tanımlaması yapılmamış ise o zaman tanımla gerçekleştir.
            if (!optionsBuilder.IsConfigured)
            {
                //SqlServer için connectionstring tanımlaması.
                optionsBuilder.UseSqlServer(connectionString);
            }


            base.OnConfiguring(optionsBuilder);
        }




        //OnModelCreating=> veritabanı oluşturulurken ne gibi işlemler dahil edilecek?
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    //Model veritabanında oluşturulurken aşağıdaki kullanıcılar ile birlikte oluşturulsun.

        //    List<AppUser> users = new List<AppUser>()
        //    {
        //        new AppUser{ID=1, Username="andrew",Email="andrew@northwind.com",Password="1234"},
        //        new AppUser{ID=2, Username="steven",Email="steven@northwind.com",Password="1234"},
        //        new AppUser{ID=3, Username="nancy",Email="nancy@northwind.com",Password="1234"},

        //    };

        //    modelBuilder.Entity<AppUser>().HasData(users);


        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
