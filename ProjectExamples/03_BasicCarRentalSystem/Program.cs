using System;
using System.Collections.Generic;
using System.Linq;

class Arac
{
    public string Plaka { get; set; }
    public string Marka { get; set; }
    public string Model { get; set; }
    public bool KiradaMi { get; set; } = false;
}

class Musteri
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Telefon { get; set; }
}

class Kiralama
{
    public Arac Arac { get; set; }
    public Musteri Musteri { get; set; }
    public DateTime KiralamaTarihi { get; set; }
}

class Program
{
    static List<Arac> araclar = new List<Arac>();
    static List<Musteri> musteriler = new List<Musteri>();
    static List<Kiralama> kiralamalar = new List<Kiralama>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Araç Kiralama Sistemi ---");
            Console.WriteLine("1 - Araç Ekle");
            Console.WriteLine("2 - Müşteri Ekle");
            Console.WriteLine("3 - Araç Kirala");
            Console.WriteLine("4 - Kiralanan Araçları Listele");
            Console.WriteLine("5 - Araç İade");
            Console.WriteLine("6 - Çıkış");
            Console.Write("Seçiminizi yapın: ");

            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    AracEkle();
                    break;
                case "2":
                    MusteriEkle();
                    break;
                case "3":
                    AracKirala();
                    break;
                case "4":
                    KiralananAraclariListele();
                    break;
                case "5":
                    AracIade();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                    break;
            }
        }
    }

    static void AracEkle()
    {
        Console.Write("Plaka: ");
        string plaka = Console.ReadLine();
        Console.Write("Marka: ");
        string marka = Console.ReadLine();
        Console.Write("Model: ");
        string model = Console.ReadLine();

        araclar.Add(new Arac { Plaka = plaka, Marka = marka, Model = model });
        Console.WriteLine("Araç başarıyla eklendi!");
    }

    static void MusteriEkle()
    {
        Console.Write("Ad: ");
        string ad = Console.ReadLine();
        Console.Write("Soyad: ");
        string soyad = Console.ReadLine();
        Console.Write("Telefon: ");
        string telefon = Console.ReadLine();

        musteriler.Add(new Musteri { Ad = ad, Soyad = soyad, Telefon = telefon });
        Console.WriteLine("Müşteri başarıyla eklendi!");
    }

    static void AracKirala()
    {
        Console.Write("Kiralanacak araç plakası: ");
        string plaka = Console.ReadLine();
        Arac arac = araclar.FirstOrDefault(a => a.Plaka == plaka && !a.KiradaMi);

        if (arac == null)
        {
            Console.WriteLine("Araç bulunamadı veya zaten kirada!");
            return;
        }

        Console.Write("Müşteri adı: ");
        string ad = Console.ReadLine();
        Musteri musteri = musteriler.FirstOrDefault(m => m.Ad == ad);

        if (musteri == null)
        {
            Console.WriteLine("Müşteri bulunamadı!");
            return;
        }

        kiralamalar.Add(new Kiralama { Arac = arac, Musteri = musteri, KiralamaTarihi = DateTime.Now });
        arac.KiradaMi = true;
        Console.WriteLine("Araç başarıyla kiralandı!");
    }

    static void KiralananAraclariListele()
    {
        foreach (var kiralama in kiralamalar)
        {
            Console.WriteLine($"Plaka: {kiralama.Arac.Plaka}, Müşteri: {kiralama.Musteri.Ad} {kiralama.Musteri.Soyad}, Tarih: {kiralama.KiralamaTarihi}");
        }
    }

    static void AracIade()
    {
        Console.Write("İade edilecek araç plakası: ");
        string plaka = Console.ReadLine();
        Arac arac = araclar.FirstOrDefault(a => a.Plaka == plaka && a.KiradaMi);

        if (arac != null)
        {
            arac.KiradaMi = false;
            var kiralama = kiralamalar.FirstOrDefault(k => k.Arac.Plaka == plaka);
            if (kiralama != null)
            {
                kiralamalar.Remove(kiralama);
            }
            Console.WriteLine("Araç başarıyla iade edildi!");
        }
        else
        {
            Console.WriteLine("Araç bulunamadı veya zaten iade edilmiş.");
        }
    }
}
