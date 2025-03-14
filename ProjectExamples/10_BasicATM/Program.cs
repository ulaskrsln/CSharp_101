using System;

class Program
{
    static decimal bakiye = 1000; // Başlangıç bakiyesi

    static void Main(string[] args)
    {
        bool devam = true;

        while (devam)
        {
            Console.WriteLine("\n--- ATM Uygulaması ---");
            Console.WriteLine("1 - Bakiye Görüntüle");
            Console.WriteLine("2 - Para Yatır");
            Console.WriteLine("3 - Para Çek");
            Console.WriteLine("4 - Çıkış");
            Console.Write("Seçiminizi yapın: ");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    BakiyeGoruntule();
                    break;
                case "2":
                    ParaYatir();
                    break;
                case "3":
                    ParaCek();
                    break;
                case "4":
                    devam = false;
                    Console.WriteLine("Çıkış yapıldı. İyi günler!");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                    break;
            }
        }
    }

    static void BakiyeGoruntule()
    {
        Console.WriteLine($"Güncel Bakiyeniz: {bakiye} TL");
    }

    static void ParaYatir()
    {
        Console.Write("Yatırmak istediğiniz tutarı girin: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal tutar) && tutar > 0)
        {
            bakiye += tutar;
            Console.WriteLine($"{tutar} TL başarıyla yatırıldı. Güncel bakiyeniz: {bakiye} TL");
        }
        else
        {
            Console.WriteLine("Geçersiz tutar girdiniz!");
        }
    }

    static void ParaCek()
    {
        Console.Write("Çekmek istediğiniz tutarı girin: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal tutar) && tutar > 0)
        {
            if (tutar <= bakiye)
            {
                bakiye -= tutar;
                Console.WriteLine($"{tutar} TL başarıyla çekildi. Güncel bakiyeniz: {bakiye} TL");
            }
            else
            {
                Console.WriteLine("Yetersiz bakiye! İşlem gerçekleştirilemedi.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz tutar girdiniz!");
        }
    }
}
