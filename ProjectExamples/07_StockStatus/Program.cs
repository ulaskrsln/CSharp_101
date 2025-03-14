using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static string filePath = "stok_verileri.txt";
    static List<Depo> depolar = new List<Depo>();

    static void Main(string[] args)
    {
        DosyadanVerileriYukle();

        while (true)
        {
            Console.WriteLine("\n--- Depo & Stok Takip Sistemi ---");
            Console.WriteLine("1 - Depo Ekle");
            Console.WriteLine("2 - Depoları Listele");
            Console.WriteLine("3 - Stok Ekle");
            Console.WriteLine("4 - Stokları Listele");
            Console.WriteLine("5 - Depolar Arası Stok Aktar");
            Console.WriteLine("6 - Depo Stok Sayım Güncelle");
            Console.WriteLine("7 - Çıkış");
            Console.Write("Seçiminizi yapın: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    DepoEkle();
                    break;
                case "2":
                    DepolariListele();
                    break;
                case "3":
                    StokEkle();
                    break;
                case "4":
                    StoklariListele();
                    break;
                case "5":
                    StokAktar();
                    break;
                case "6":
                    StokSayimGuncelle();
                    break;
                case "7":
                    DosyayaVerileriKaydet();
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                    break;
            }
        }
    }

    class Depo
    {
        public string DepoAdi { get; set; }
        public Dictionary<string, int> Stoklar { get; set; } = new Dictionary<string, int>();
    }

    static void DepoEkle()
    {
        Console.Write("Depo Adını Girin: ");
        string depoAdi = Console.ReadLine();

        if (depolar.Any(d => d.DepoAdi == depoAdi))
        {
            Console.WriteLine("Bu isimde bir depo zaten mevcut!");
            return;
        }

        depolar.Add(new Depo { DepoAdi = depoAdi });
        Console.WriteLine("Depo başarıyla eklendi.");
    }

    static void DepolariListele()
    {
        if (!depolar.Any())
        {
            Console.WriteLine("Hiç depo bulunmamaktadır.");
            return;
        }

        Console.WriteLine("\nMevcut Depolar:");
        foreach (var depo in depolar)
        {
            Console.WriteLine($"- {depo.DepoAdi}");
        }
    }

    static void StokEkle()
    {
        Console.Write("Depo Adını Girin: ");
        string depoAdi = Console.ReadLine();
        var depo = depolar.FirstOrDefault(d => d.DepoAdi == depoAdi);

        if (depo == null)
        {
            Console.WriteLine("Depo bulunamadı.");
            return;
        }

        Console.Write("Ürün Adını Girin: ");
        string urunAdi = Console.ReadLine();
        Console.Write("Adet Girin: ");
        if (int.TryParse(Console.ReadLine(), out int adet))
        {
            if (depo.Stoklar.ContainsKey(urunAdi))
                depo.Stoklar[urunAdi] += adet;
            else
                depo.Stoklar[urunAdi] = adet;

            Console.WriteLine("Stok başarıyla eklendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz adet girişi!");
        }
    }

    static void StoklariListele()
    {
        foreach (var depo in depolar)
        {
            Console.WriteLine($"\nDepo: {depo.DepoAdi}");
            if (!depo.Stoklar.Any())
            {
                Console.WriteLine("  - Stok bulunmamaktadır.");
                continue;
            }

            foreach (var stok in depo.Stoklar)
            {
                Console.WriteLine($"  - Ürün: {stok.Key}, Adet: {stok.Value}");
            }
        }
    }

    static void StokAktar()
    {
        Console.Write("Kaynak Depo Adını Girin: ");
        string kaynakDepoAdi = Console.ReadLine();
        var kaynakDepo = depolar.FirstOrDefault(d => d.DepoAdi == kaynakDepoAdi);
        if (kaynakDepo == null)
        {
            Console.WriteLine("Kaynak depo bulunamadı.");
            return;
        }

        Console.Write("Hedef Depo Adını Girin: ");
        string hedefDepoAdi = Console.ReadLine();
        var hedefDepo = depolar.FirstOrDefault(d => d.DepoAdi == hedefDepoAdi);
        if (hedefDepo == null)
        {
            Console.WriteLine("Hedef depo bulunamadı.");
            return;
        }

        Console.Write("Ürün Adını Girin: ");
        string urunAdi = Console.ReadLine();
        Console.Write("Adet Girin: ");
        if (int.TryParse(Console.ReadLine(), out int adet))
        {
            if (kaynakDepo.Stoklar.ContainsKey(urunAdi) && kaynakDepo.Stoklar[urunAdi] >= adet)
            {
                kaynakDepo.Stoklar[urunAdi] -= adet;
                if (hedefDepo.Stoklar.ContainsKey(urunAdi))
                    hedefDepo.Stoklar[urunAdi] += adet;
                else
                    hedefDepo.Stoklar[urunAdi] = adet;

                Console.WriteLine("Stok başarıyla aktarıldı.");
            }
            else
            {
                Console.WriteLine("Yeterli stok bulunmamaktadır.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz adet girişi!");
        }
    }

    static void StokSayimGuncelle()
    {
        Console.Write("Depo Adını Girin: ");
        string depoAdi = Console.ReadLine();
        var depo = depolar.FirstOrDefault(d => d.DepoAdi == depoAdi);
        if (depo == null)
        {
            Console.WriteLine("Depo bulunamadı.");
            return;
        }

        Console.Write("Ürün Adını Girin: ");
        string urunAdi = Console.ReadLine();
        Console.Write("Yeni Adet Girin: ");
        if (int.TryParse(Console.ReadLine(), out int yeniAdet))
        {
            depo.Stoklar[urunAdi] = yeniAdet;
            Console.WriteLine("Stok başarıyla güncellendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz adet girişi!");
        }
    }

    static void DosyayaVerileriKaydet()
    {
        List<string> lines = new List<string>();
        foreach (var depo in depolar)
        {
            lines.Add($"Depo:{depo.DepoAdi}");
            foreach (var stok in depo.Stoklar)
            {
                lines.Add($"{stok.Key}:{stok.Value}");
            }
        }
        File.WriteAllLines(filePath, lines);
    }

    static void DosyadanVerileriYukle()
    {
        if (!File.Exists(filePath))
            return;

        Depo currentDepo = null;
        foreach (var line in File.ReadAllLines(filePath))
        {
            if (line.StartsWith("Depo:"))
            {
                currentDepo = new Depo { DepoAdi = line.Substring(5) };
                depolar.Add(currentDepo);
            }
            else
            {
                var parts = line.Split(':');
                if (currentDepo != null && parts.Length == 2 && int.TryParse(parts[1], out int adet))
                {
                    currentDepo.Stoklar[parts[0]] = adet;
                }
            }
        }
    }
}
