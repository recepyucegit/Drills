//int i = 0;
//while(i<10)
//   {

//    Console.WriteLine("Merhaba Dünya");
//    i++;

//}

int tekToplam = 0;
int çiftToplam = 0;

int i = 1;

while( i<= 100)
{
    if( i%2==0)
    {
        çiftToplam += i;

    }
    else
    {
        tekToplam += i;
    }
    i++;
}

Console.WriteLine("Tek Toplam: "+ tekToplam);
Console.WriteLine("Çift Toplam: "+ çiftToplam);
