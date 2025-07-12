// Do While
/*
 * Do While döngüsü while döngüsü ile aynı prensibe sahiptir. Ancak, tek farkı do while döngüsü koşul her ne olursa olsun en az bir kere çalışır.
 * 
 */
// 1' den 10'a kadar console' da sayıları yazdırın.

//int i=1;
//while (i <=10)
//{

//    Console.WriteLine(i);
//    i++;

//}

//do
//{
//    Console.WriteLine(i);
//    i++;
//} while (i <=10);

// 1000 'den 1 'e kadar sayıları console'da yazdırın.

//int i = 1000;
//do
//{
//    Console.WriteLine(i);
//    i--;
//} while (i >= 0);

// Kullanıcıdan 5 kez değer alın alınan değerleri en sonda console'da gösterin.

//string deger = "";

//int i = 0;
//do
//{
//    Console.WriteLine("Değer girin: ");
//    deger += Console.ReadLine() + " ";
//    i++;

//} while (i < 5);
//Console.WriteLine(deger);

// Uygulama çalıştırığında kullanıcı adı "admin" şifre "1234" olan bilgiler girildiğinde console' da bir sayı alınsın. sayının tek mi çift mi olduğu tekrar console'da gösterilsin.
// 

// kullanıcı adı:
// admin
// şifre:
// 1234

// sayı :5
// sayı tek
// tekrar sayı girilsin mi?(e/h)
using System.ComponentModel;

/// aşağıda ki işlen bir kez.
/// aşağıdaki işlem tekrarlı gerçekleşecek.
//string kullaniciAd = "admin";
//string sifre = "1234";

//string gelenKullaniciAd = "";
//string gelenSifre = "";

//int gelenSayi = 0;
//Console.WriteLine("kullanıcı ad: ");
//gelenKullaniciAd =Console.ReadLine();
//Console.WriteLine(" Şifre: ");
//gelenSifre=Console.ReadLine();

//bool devam =false;

//if(gelenKullaniciAd==kullaniciAd && gelenSifre ==sifre)
//{
//    do
//    {
//        Console.WriteLine("sayı sayı girin: ");
//        gelenSayi=int.Parse(Console.ReadLine());
//        if (gelenSayi %2 ==0)
//        {
//            Console.WriteLine("Sayı çift");

//        }
//        else
//        {
//            Console.WriteLine("Sayı Tek");

//        }
//        Console.WriteLine("İşlem devam etsin mi?(e/h)");
//        string secim =Console.ReadLine();
//        if (secim=="e")
//        {
//            devam=true;
//        }
//        else
//        {
//            devam= false;
//        }

//    }while (devam);

//}
//else
//{
//    Console.WriteLine("bilgileriniz yanlış!")
//        ;
//}

/*
 * 1-Bakiye görüntüle
 * 2- Para yükle
 * 3- Para çek
 * 4- Çıkış
 * secim: 2
 

 */
// kullanıcıdan bir seçim değeri alınacak. Bu değere göre işlemler   do while döngü kullanılarak gerçekletirilecek. Bakiye 1000TL olarak tanımlanacak
decimal bakiye = 1000;

string menu = "1- Bakiye Görüntüle \n 2- Para yükle \n 3-Para çek \n 4- Çıkış";
string secim = "";
decimal gelenDeger = 0;

do
{
    Console.WriteLine(menu);
    secim= Console.ReadLine();
    // karar yapısı
    switch (secim)
    {
        case "1":
            Console.WriteLine("Bakiyeniz"+ bakiye);
            break;
            case "2":
            Console.WriteLine("deger:");
            gelenDeger= decimal.Parse(Console.ReadLine());
            bakiye += gelenDeger;
            Console.WriteLine("Bakiyeniz: "+bakiye);
            break;
            case "3":
            Console.WriteLine("değer: ");
            gelenDeger=decimal.Parse(Console.ReadLine());

           if(gelenDeger<=bakiye)
            {
                bakiye -=gelenDeger;
                Console.WriteLine("Bakiyeniz: "+ bakiye);

            }
            else
            {
                Console.WriteLine("Bakiyeniz yetersiz !!!");

            }
                break;
            
            case "4":
            Console.WriteLine("Çıkış yapılıyor ...");

            break;
        default:
            Console.WriteLine("Geçersiz işlem. Lütfen 1 ile 4 arasıdna değer girin");
            break;
    }
        
        
} while (secim != "4");
Console.WriteLine("Uygulama sonlandırıldı");

