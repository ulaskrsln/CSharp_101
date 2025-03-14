using System;
using System.Collections.Generic;
using System.Linq;

class Misafir
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Telefon { get; set; }
    public List<string> EkHizmetler { get; set; } = new List<string>();
    public decimal ToplamUcret { get; set; }
}

class Program
{
    static List<Misafir> misafirler = new List<Misafir>();
    static Dictionary<string, decimal> hizmetler = new Dictionary<string, decimal>
    {
        { "Kahvaltı", 50 },
        { "SPA", 150 },
        { "Havaalanı Transfer", 200 }
    };
    static decimal odaUcreti = 500;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Otel Resepsiyon Sistemi ---");
            Console.WriteLine("1 - Misafir Kaydı");
            Console.WriteLine("2 - Misafir Listeleme");
            Console.WriteLine("3 - Çıkış");
            Console.Write("Seçiminizi yapın: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    MisafirKaydi();
                    break;
                case "2":
                    MisafirListele();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                    break;
            }
        }
    }

    static void MisafirKaydi()
    {
        Console.Write("Ad: ");
        string ad = Console.ReadLine();
        Console.Write("Soyad: ");
        string soyad = Console.ReadLine();
        Console.Write("Telefon: ");
        string telefon = Console.ReadLine();

        List<string> secilenHizmetler = new List<string>();
        decimal toplamUcret = odaUcreti;

        Console.WriteLine("Ek hizmetler (seçmek için hizmet adını yazın, bitirmek için 'tamam' yazın):");
        foreach (var hizmet in hizmetler)
        {
            Console.WriteLine($"- {hizmet.Key} ({hizmet.Value} TL)");
        }

        while (true)
        {
            string secim = Console.ReadLine();
            if (secim.ToLower() == "tamam") break;
            if (hizmetler.ContainsKey(secim))
            {
                secilenHizmetler.Add(secim);
                toplamUcret += hizmetler[secim];
            }
            else
            {
                Console.WriteLine("Geçersiz hizmet, tekrar deneyin.");
            }
        }

        misafirler.Add(new Misafir { Ad = ad, Soyad = soyad, Telefon = telefon, EkHizmetler = secilenHizmetler, ToplamUcret = toplamUcret });
        Console.WriteLine($"Misafir başarıyla kaydedildi! Toplam Ücret: {toplamUcret} TL");
    }

    static void MisafirListele()
    {
        foreach (var misafir in misafirler)
        {
            Console.WriteLine($"Ad: {misafir.Ad}, Soyad: {misafir.Soyad}, Telefon: {misafir.Telefon}, Ücret: {misafir.ToplamUcret} TL, Ek Hizmetler: {string.Join(", ", misafir.EkHizmetler)}");
        }
    }
}