/*
 //Kahve Sipariş Uygulaması
//Filtre 90 TL, Latte 120 TL, Americano 110TL, Espresso 150 TL

//Uygulama çalıştığında sisteme kullanıcı giriş yapabilmeli. Giriş başarılı şekilde gerçekleştirildikten sonra kullanıcnın hesabına maksimum 3 adet sipariş verebilmesi için hak tanımlanmalı.

//Sisteme hoşgeldiniz. Maksimum 3 adet sipariş verebilirsiniz.
//1-Sipariş Oluştur
//2-Sipariş Adet Güncelle
//3-Çıkış.
seçim: 1
**Kahvelerimiz***
1-Filtre 90 TL
2-Latte 120 TL
3-Americano 110 TL
4-Espresso 150 TL
secim: 2
Adet: 2
Sipariş işlemini bilgilerinizi girin:
Ad: Fatih
Soyad: Günalp
Adres: Kadıköy
-------------
Siparişiniz oluşturuldu ödemeniz gereken toplam tutar 240 TL
Kalan sipariş hakkınız 2
--------------------------
Opsiyonel: Alınan siparişlerin özetleri


 */

#region Global Alan
using System.Security.Cryptography;

int secim = -1;
string[] kahveMenu = { "Filtre", "Latte", "Americano", "Espreesso" };
decimal[] kahveFiyatlari = { 90, 120, 150, 150 };
decimal toplamTutar = 0;
int adet = 0;
string musteriBilgisi = "";
string[] siparisler = new string[3];
string tanimliKullaniciAd = "admin";
string tanimliSifre = "1234";
string gelenKullaniciAd = "";
string gelenSifre = "";
int jeton = 0;

#endregion
int z = 0;
if (gelenKullaniciAd==tanimliKullaniciAd && gelenSifre==tanimliSifre)
{

    do
    {

        for (int i=0;i<kahveMenu.Length; i++)
        {
            string format = $"{i + 1}- {kahveMenu[i]}-{kahveFiyatlari[i]} ";
            Console.WriteLine(format);
        }

        try
        {
            Console.WriteLine("seçim: ");
            secim = int.Parse(Console.ReadLine());
            //kullanıcın seçimi kahvelerin index'i dışında ise kullanıcıya mesaj verin.
            if (secim <= 0 || secim - 1 > kahveMenu.Length)
            {
                Console.WriteLine($"Lütfen 1 ile {kahveMenu.Length} aralığında bir değer girin.");
            }
            else
            {
                string gelenKahve = kahveMenu[secim - 1];
                decimal gelenFiyat = kahveFiyatlari[secim - 1];

                Console.WriteLine("Adet: ");
                adet = int.Parse(Console.ReadLine());
                toplamTutar = adet * gelenFiyat;
                Console.WriteLine("Ad: ");
                musteriBilgisi += Console.ReadLine() + " ";
                Console.WriteLine("Soyad: ");
                musteriBilgisi += Console.ReadLine() + " ";
                Console.WriteLine("Adres ");
                musteriBilgisi += Console.ReadLine() + " ";

                musteriBilgisi += $"Seçili Kahve : {gelenKahve} Birim Fiyat:{gelenFiyat} Adet:{adet} Toplam Tutar: {toplamTutar}";
                Console.WriteLine(musteriBilgisi);
                siparisler[z] = musteriBilgisi;
                z++;

            }
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    } while (z < 3);
    Console.WriteLine("***Alınan Siparişler****");
   foreach(string s in siparisler)
    {
        Console.WriteLine(s);
    }
}
else
{
    Console.WriteLine("Kullanıcı bilgileriniz hatalı!!!");
}

