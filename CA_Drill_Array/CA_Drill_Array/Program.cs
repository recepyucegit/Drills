string[] cities = new string[3];
    cities[0]="Berlin";
cities[1] = "Paris";
cities[2] = "Roma";

string[] sehirler = { "İstanbul", "Ankara", "İzmir", "Bayburt", "Bilecik" };

//try
//{
//Console.WriteLine(sehirler[0]);
//int i = 0;
//while( i < sehirler.Length)
//{
//    Console.WriteLine(sehirler[i]);
//    i++;
//}
//int i = 0;
//do
//{
//    Console.WriteLine(cities[i]);
//    i++;
//} while (i < 3);
//    for(int i=0; i<3; i++)
//    {
//        Console.WriteLine("Şehir: "+ sehirler[i]+ " City: "+ cities[i]);
//    }
//}
//catch (IndexOutOfRangeException ex)
//{
//    Console.WriteLine(ex.Message);
//}
//123,54,4532,23,734,77,83,45,190

//yukarıdaki sayıalrdan ikiye tam bölünenleri "ikiye tam bölüneneler" havuzuna ikiye tam bölünmeyenleri "ikiye tam bölünmeyenler" havuzuna dahil edin. Hem ikiye hem de üçe tam bölünenlerin adedini console'da gösterin.


string ikiyeTamBolunenler = "";
string ikiyeTamBolunmeyenler = "";
int adet = 0;

int[] sayilar = { 123, 54, 4532, 23, 734, 77, 83, 45, 190 };

for(int i = 0; i<sayilar.Length; i++)
{
    if (sayilar[i]%2==0)
    {
        ikiyeTamBolunenler += sayilar[i] + " ";

    }
    else
    {
        ikiyeTamBolunmeyenler += sayilar[i] + "";
    }
    if(sayilar[i]%2==0 && sayilar[i]%3==0)
    {
        adet++;
        Console.WriteLine("Hem ikiye hem de üçe bölünenler: " + sayilar[i]);

        
    }
}
Console.WriteLine("ikiye tam bölünenler: "+ ikiyeTamBolunenler);
Console.WriteLine("ikiye tam bölünmeyenler: "+ ikiyeTamBolunmeyenler);
Console.WriteLine("Adet: "+ adet);