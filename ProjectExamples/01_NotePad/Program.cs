using System;
using System.Collections.Generic;
using System.IO; // Dosya işlemleri için

class Program
{
    static string dosyaYolu = "notlar.txt"; // Notları saklayacağımız dosya
    static List<string> notlar = new List<string>(); // Notları tutan liste

    static void Main()
    {
        NotlariYukle(); // Program başlarken dosyadan notları yükle

        while (true)
        {
            Console.WriteLine("\n--- NOT DEFTERİ ---");
            Console.WriteLine("1 - Not Ekle");
            Console.WriteLine("2 - Notları Listele");
            Console.WriteLine("3 - Not Sil");
            Console.WriteLine("4 - Çıkış");
            Console.Write("Seçiminizi yapın: ");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    NotEkle();
                    break;
                case "2":
                    NotlariListele();
                    break;
                case "3":
                    NotSil();
                    break;
                case "4":
                    NotlariKaydet(); // Çıkarken notları kaydet
                    Console.WriteLine("Notlar kaydedildi. Programdan çıkılıyor...");
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                    break;
            }
        }
    }

    static void NotEkle()
    {
        Console.Write("Eklemek istediğiniz notu yazın: ");
        string yeniNot = Console.ReadLine();
        notlar.Add(yeniNot);
        Console.WriteLine("Not başarıyla eklendi!");
    }

    static void NotlariListele()
    {
        if (notlar.Count == 0)
        {
            Console.WriteLine("Henüz hiç not eklenmemiş.");
            return;
        }

        Console.WriteLine("\n--- NOTLAR ---");
        for (int i = 0; i < notlar.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {notlar[i]}");
        }
    }

    static void NotSil()
    {
        NotlariListele();
        if (notlar.Count == 0) return;

        Console.Write("Silmek istediğiniz notun numarasını girin: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= notlar.Count)
        {
            notlar.RemoveAt(index - 1);
            Console.WriteLine("Not başarıyla silindi!");
        }
        else
        {
            Console.WriteLine("Geçersiz numara, tekrar deneyin.");
        }
    }

    static void NotlariKaydet()
    {
        File.WriteAllLines(dosyaYolu, notlar); // Listeyi dosyaya kaydet
    }

    static void NotlariYukle()
    {
        if (File.Exists(dosyaYolu)) // Eğer dosya varsa
        {
            notlar = new List<string>(File.ReadAllLines(dosyaYolu)); // Dosyadaki notları listeye aktar
        }
    }
}
