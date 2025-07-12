//ForEach:
/*
 * Foreach bir koleksiyon döngüsü olarak adlandırlır. Diğer döngülerden en büyük farkı iterasyon ve koşul barındırmaz!
 * 
 * 

 
 */

//string[] sehirler = { "İstanbul ", "Ankara", "İzmir ","Bayburt", "Bilecik" };

//foreach (string sehir in sehirler)
//{
//    Console.WriteLine(sehir);
//}

//string[] students = new string[5];

Random  rnd= new Random();

string[] students = { "Hakan", "Arda", "Ali", "Muhammet", "Kenan" };
int rastgele= rnd.Next(students.Length);

Console.WriteLine(students[rastgele]);
