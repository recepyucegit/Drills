using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("📓 Günlük Uygulaması");
        Console.WriteLine("--------------------\n");

        // Şükür Günlüğü
        Console.WriteLine("🙏 Şükür Günlüğü (3 madde yazınız):");
        string[] sukurs = new string[3];
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"{i + 1}. Şükür: ");
            sukurs[i] = Console.ReadLine();
        }

        // Stoic Günlük
        Console.WriteLine("\n🧘 Stoic Günlük");
        Console.Write("1) Bugün neleri yanlış yaptım?: ");
        string yanlislar = Console.ReadLine();

        Console.Write("2) Neleri nasıl düzeltebilirdim?: ");
        string duzeltmeler = Console.ReadLine();

        Console.Write("3) Neleri doğru yaptım?: ");
        string dogrular = Console.ReadLine();

        Console.Write("4) Gün içinde neler yaptım?: ");
        string gunIcinde = Console.ReadLine();

        // Kayıt
        string tarih = DateTime.Now.ToString("yyyy-MM-dd");
        string dosyaAdi = $"Gunluk_{tarih}.txt";

        using (StreamWriter sw = new StreamWriter(dosyaAdi, true))
        {
            sw.WriteLine("📓 Günlük - " + tarih);
            sw.WriteLine("\n--- Şükür Günlüğü ---");
            for (int i = 0; i < sukurs.Length; i++)
                sw.WriteLine($"{i + 1}. {sukurs[i]}");

            sw.WriteLine("\n--- Stoic Günlük ---");
            sw.WriteLine("Yanlışlarım: " + yanlislar);
            sw.WriteLine("Düzeltmelerim: " + duzeltmeler);
            sw.WriteLine("Doğrularım: " + dogrular);
            sw.WriteLine("Gün İçinde Yaptıklarım: " + gunIcinde);
            sw.WriteLine("\n-----------------------------\n");
        }

        Console.WriteLine($"\n✅ Günlük kaydedildi: {dosyaAdi}");
    }
}