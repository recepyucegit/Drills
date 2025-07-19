using CA_EFSorgular.Models.Northwind;

namespace CA_EFSorgular
{
    class Program
    {
        static void Main(string[] args)
        {

            //Northwind veritabanı'nın yanasımasını prjeye dahil edilmesi.

            //Scaffold-DbContext: varolan bir veritabanı'nın yansımasını projeye aktarır.

            //Scaffold-DbContext "connectionstring" Microsoft.EntityFrameworkCore.SqlServer -o Models
            NorthwindContext context = new NorthwindContext();

            //EF Sorgular => Linq Queries


            #region Soru-1
            //1-SQL Soru: Siparişler tablosu müşteri şirket adı, çalışan adı ve soyadı ile birlikte listeleyin. 
            //T-SQL:
            /*
             select c.CompanyName,e.FirstName,e.LastName from Orders o
            inner join Customers c on o.CustomerID=c.CustomerID
            inner join Employees e on e.EmployeeID=o.EmployeeID
             */

            #endregion

            #region Linq to Sql
            //Linq to sql: tsql sorgularına oldukça yakın bir syntax'a sahiptir. Sorgulamaları nesne tarafından gerçekleştirmemize olanak sağlar.

            //var result = from o in context.Orders
            //             select new
            //             {
            //                 o.Employee.FirstName,
            //                 o.Employee.LastName,
            //                 o.Customer.CompanyName
            //             };

            //var listResult = result.ToList();

            //foreach (var item in listResult)
            //{
            //    Console.WriteLine(item.FirstName + " " + item.LastName);
            //} 


            //Linq to entity: Sorgulamaları genişletilmiş metotlar kullanılarak gerçekleştiren işlemdir. 

            //lambda
            //x=>x

            // var result = context.Orders.Select(o => new
            // {
            //     o.Employee.FirstName,
            //     o.Employee.LastName,
            //     o.Customer.CompanyName
            // }).ToList();

            //// var result = context.Orders.Select(x=>x);

            // foreach (var item in result)
            // {
            //     Console.WriteLine(item.CompanyName);
            // }
            /////////////Drills///////////////////////////
            // var result = context.Orders.Select(o => new
            //{
            // o.Employee.FirstName,
            // o.Employee.LastName,
            // o.Customer.CompanyName
            //}).ToList();

            // var result= context.Orders.Select(x=>x);
            // foreach(var item in result)
            //{
            //Console.WriteLine(item.Customer.CompanyName);
            //}

            /////////////Drills///////////////////////////
            // var result =context.Orders.Select(o=>new
            // {
            //o.Employee.FirstName,
            // o.Employee.LastName,
            //o.Customer.CompanyName
            //}).ToList();
            // var result=context.Orders.Select(x=>x);
            // foreach(var item in result)
            //{
            // Console.WriteLine(item.Customer.CompanyName);
            //}
            /////////////Drills///////////////////////////
            // var result= context.Orders.Select(o=>new
            //{
            // o.Employee.FirstName,
            // o.Employee.LastName,
            // o.Customer.CompanyName
            //}).ToList();

            // var result = context.Orders.Select(x=>x);
            // foreach(var item in result)
            //{
            // Console.WriteLine(item.Customer.CompanyName);
            //}

            #endregion

            #region Soru-2
            //Soru-2: Fiyatı 10 ile 30 arasında olan ürünlerin Id, ürün adı, fiyatı ve stok miktarlarını listeleyin.

            //T-SQL:
            /*
             select ProductID,ProductName,UnitPrice,UnitsInStock from Products where UnitPrice>10 and UnitPrice<30
             */

            //Linq to sql:
            //var resultQuery = (from p in context.Products
            //                  where p.UnitPrice > 10 && p.UnitPrice < 30
            //                  select new
            //                  {
            //                      p.ProductId,
            //                      p.ProductName,
            //                      p.UnitPrice,
            //                      p.UnitsInStock
            //                  }).ToList();

            //foreach (var item in resultQuery)
            //{
            //    Console.WriteLine(item);
            //}


            //Linq to entity:

            //var resultEntity = context.Products
            //    .Where(p => p.UnitPrice > 10 && p.UnitPrice < 30)
            //    .Select(x => new
            //    {
            //        x.ProductId,
            //        x.ProductName,
            //        x.UnitPrice,
            //        x.UnitsInStock
            //    }).ToList();

            //foreach (var item in resultEntity)
            //{
            //    Console.WriteLine(item);
            //} 

            /////////////Drills///////////////////////////
            // var resultQuery =(from p in context.Products
            //where p.UnitPrice > 10 && p.UnitPrice<30
            // select new
            // {
            // p.ProductId,
            // p.ProductName,
            // p.UnitPrice,
            // p.UnitsInStock
            //}).ToList();
            //foreach (var item in resultQuery)
            //{
            //Console.WriteLine(item);
            //}

            //var resultEntity=context.Products
            //.Where(p=>p.UnitPriceZ>10 && p.UnitPrice<30)
            //.Select(x=>new
            //{
            //x.ProductId,
            //x.ProductName,
            //x.UnitPrice,
            //x.UnitsInStock
            //}).ToList();
            //foreach (var item in resultEntity)
            //{
            // Console.WriteLine(item);
            //}
            /////////////Drills///////////////////////////
            // var resultQuery= (from p in context.Products
            //where p.UnitPrice > 10&& p.UnitPrice<30
            // select new
            // {
            // p.ProductId,
            // p.ProductName,
            // p.UnitPrice,
            //p.UnitsInStock
            //}).ToList();

            //foreach(var item in resultQuery)
            //   {
            // Console.WriteLine(item);
            // }
            // var resultEntity= context.Products
            //.where(p=>p.UnitPrice>10&&p.UnitPrice<30)
            //.Select(x=>new
            //{
            //x.ProductId,
            //x.ProductName,
            //x.UnitPrice,
            //x.UnitsInStock
            //}).ToList();
            // foreach(var item in resultEntity)
            // {
            // Console.WriteLine(item);
            //}


            #endregion


            #region Soru-3
            //Soru 3: Müşteri şirket adında "restaurant" geçen müşterileri listeleyin.

            //Linq to sql: 
            //var resultQuery = (from c in context.Customers
            //                   where c.CompanyName.Contains("restaurant")
            //                   select c).ToList();

            ////for (int i = 0; i < resultQuery.Count; i++)
            ////{
            ////    Console.WriteLine(resultQuery[i].CompanyName);
            ////}

            ////Linq to entity:
            //var resultEntity = context.Customers.Where(c => c.CompanyName.Contains("restaurant")).ToList();

            //foreach (var item in resultEntity)
            //{
            //    Console.WriteLine(item.CompanyName);
            //} 
            #endregion

            #region Soru-4
            //Soru-4: Kategori adı "beverages" olan kategorinin Id'si nedir?

            //Linq to sql:
            //var resultQuery=from c in context.Categories
            //                where c.CategoryName=="beverages" select c.CategoryId;

            //foreach (var item in resultQuery)
            //{
            //    Console.WriteLine(item);
            //}

            ////Linq to entity:
            ////First();  => koşulu olduğu gibi kontrol eder eğer ilgili veri varsa satır değerini aktarırır.
            ////FirstOrDefault(); =>koşulu olduğu gibi kontrol eder eğer ilgili veri varsa satır değerini aktarırır. Veri yoksa varsayılan değere bakar onu aktarır.




            //var resultEntity = context.Categories.FirstOrDefault(c => c.CategoryName == "asdadadad");



            //Console.WriteLine(resultEntity.CategoryId); 
            #endregion


            // Kategori adı

            //Kategori adı "condiments" olan ve ürün adı "Gözlük" fiyatı 5000 stok miktarı 20 olan ürünü veritabanına ekleyin.

            //1-Bir ürün eklenecek. Ancak bu ürünün CategoryId'si adı condiments olan kategorinin Id'sı olacak.
            //int condimentsCategoryId = context.Categories
            //    .Where(x => x.CategoryName == "condiments")
            //    .FirstOrDefault().CategoryId;

            int catId = context.Categories
                .FirstOrDefault(x => x.CategoryName == "condiments").CategoryId;

            //2-Ürün bilgileri için Product nesnesinin bir örneği (instance) alınacak.,
            Product product = new Product();

            //3-Alınan instance içerisine bilgiler aktarılacak.
            product.CategoryId = catId;
            product.ProductName = "Gözlük";
            product.UnitPrice = 5000;
            product.UnitsInStock = 20;
            //4-Context içerisinde bulunan Products'a alınan instance eklenecek.
            context.Products.Add(product);
            //5-Context kaydedilecek.
            //  context.SaveChanges(); //SaveChanges() geriye -1 (hatalı) ya da 1 (başarılı) döner
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine("Ürün eklendi");
            }
            else
            {
                Console.WriteLine("bir hata meydana geldi!");
            }
            ///////////////Drills///////////////////////////
            //



        }
    }
}
