// For

/*
 * for döngüsü while döngüsü ile aynı mantığa sahip olsa da birbirilerinden ayrılan farklı özelliklere sahiptir. Bu fark ise while döngüsünde kullanılan iterasyon, koşul be iterasyon artışı/ azalış ilemleri for döngüsünde parametre olarak tanımlanır. 
 
 */

//for(int i =1;  i <= 10; i++)
//{
//    Console.WriteLine(i);
//}

// a' dan z 'ye alfabeyi console' da gösterin.

char karakter = 'a';

int karakter2 = karakter;
Console.WriteLine(karakter2);

for(int i=97; i<=122;  i++)
{
    char c= Convert.ToChar(i);
    Console.WriteLine(c+" "+i);
}