/*
 ++Static => Durağan, sabit
++Constructor => yapıcı metot.
++Polymorphism => çok biçimlilik.
++Inheritance=> Kalıtım
++Encapsulation => bir class içerisinde tanımlı olan field'a yapılan istekleri kontrol altına alma işlemi. (Get, Set)
++Class => nesnenin özelliklerinin barındırıldığı alan.
 */

// Bilgisayar

using CA_OOPTekrar_Drill;

Anakart anakart = new Anakart();
anakart.Marka = "ASD";
anakart.ID = 1;
anakart.Fiyat = 5000;
decimal kampanyalıAnakartFiyat = anakart.Kampanya();

Ram ram = new Ram();

ram.Marka = "Kingston";
ram.ID = 1;
ram.Fiyat = 50000;
decimal kampanyalıRamFiyat= ram.Kampanya();

Console.WriteLine(anakart.Fiyat+" "+kampanyalıAnakartFiyat);
Console.WriteLine(ram.Fiyat+" "+kampanyalıRamFiyat);

PcBilesenData.BilesenListesi.Add(anakart);
PcBilesenData.BilesenListesi.Add(ram);
