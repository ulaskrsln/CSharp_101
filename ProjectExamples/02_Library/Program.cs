using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Kitap
{
    public string Ad { get; set; }
    public string Yazar { get; set; }
    public string Tur { get; set; }
}

class Program
{
    static string dosyaYolu = "kitaplar.txt";
    static List<Kitap> kitaplar = new List<Kitap>();

    static void Main()
    {
        KitaplariYukle();

        while (true)
        {
            Console.WriteLine("\n--- KÜTÜPHANE SİSTEMİ ---");
            Console.WriteLine("1 - Kitap Ekle");
            Console.WriteLine("2 - Kitapları Listele");
            Console.WriteLine("3 - Kitap Ara");
            Console.WriteLine("4 - Kitap Sil");
            Console.WriteLine("5 - Çıkış");
            Console.Write("Seçiminizi yapın: ");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    KitapEkle();
                    break;
                case "2":
                    KitaplariListele();
                    break;
                case "3":
                    KitapAra();
                    break;
                case "4":
                    KitapSil();
                    break;
                case "5":
                    KitaplariKaydet();
                    Console.WriteLine("Kitaplar kaydedildi. Programdan çıkılıyor...");
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                    break;
            }
        }
    }

    static void KitapEkle()
    {
        Console.Write("Kitap adı: ");
        string ad = Console.ReadLine();
        Console.Write("Yazar: ");
        string yazar = Console.ReadLine();
        Console.Write("Tür: ");
        string tur = Console.ReadLine();

        kitaplar.Add(new Kitap { Ad = ad, Yazar = yazar, Tur = tur });
        Console.WriteLine("Kitap başarıyla eklendi!");
    }

    static void KitaplariListele()
    {
        if (!kitaplar.Any())
        {
            Console.WriteLine("Kütüphane boş.");
            return;
        }

        var gruplar = kitaplar.GroupBy(k => k.Tur);
        foreach (var grup in gruplar)
        {
            Console.WriteLine($"\nTür: {grup.Key}");
            foreach (var kitap in grup)
            {
                Console.WriteLine($"- {kitap.Ad} (Yazar: {kitap.Yazar})");
            }
        }
    }

    static void KitapAra()
    {
        Console.Write("Aramak istediğiniz kitap adı veya yazar: ");
        string arama = Console.ReadLine().ToLower();

        var bulunanlar = kitaplar.Where(k => k.Ad.ToLower().Contains(arama) || k.Yazar.ToLower().Contains(arama)).ToList();

        if (bulunanlar.Count == 0)
        {
            Console.WriteLine("Eşleşen kitap bulunamadı.");
        }
        else
        {
            Console.WriteLine("\n--- Arama Sonuçları ---");
            foreach (var kitap in bulunanlar)
            {
                Console.WriteLine($"- {kitap.Ad} (Yazar: {kitap.Yazar}, Tür: {kitap.Tur})");
            }
        }
    }

    static void KitapSil()
    {
        Console.Write("Silmek istediğiniz kitap adını girin: ");
        string ad = Console.ReadLine().ToLower();

        var kitap = kitaplar.FirstOrDefault(k => k.Ad.ToLower() == ad);

        if (kitap != null)
        {
            kitaplar.Remove(kitap);
            Console.WriteLine("Kitap başarıyla silindi!");
        }
        else
        {
            Console.WriteLine("Kitap bulunamadı.");
        }
    }

    static void KitaplariKaydet()
    {
        var satirlar = kitaplar.Select(k => $"{k.Ad}|{k.Yazar}|{k.Tur}");
        File.WriteAllLines(dosyaYolu, satirlar);
    }

    static void KitaplariYukle()
    {
        if (File.Exists(dosyaYolu))
        {
            var satirlar = File.ReadAllLines(dosyaYolu);
            foreach (var satir in satirlar)
            {
                var parcalar = satir.Split('|');
                if (parcalar.Length == 3)
                {
                    kitaplar.Add(new Kitap { Ad = parcalar[0], Yazar = parcalar[1], Tur = parcalar[2] });
                }
            }
        }
    }
}
