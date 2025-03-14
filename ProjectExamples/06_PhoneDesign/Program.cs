using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TelefonRehberi
{
    class Program
    {
        static string dosyaYolu = "rehber.txt";
        static List<Kisi> rehber = new List<Kisi>();

        static void Main(string[] args)
        {
            KisileriYukle();

            while (true)
            {
                Console.WriteLine("\nTelefon Rehberi Uygulaması");
                Console.WriteLine("1. Yeni Kişi Ekle");
                Console.WriteLine("2. Kişileri Listele");
                Console.WriteLine("3. Kişi Sil");
                Console.WriteLine("4. Kişi Güncelle");
                Console.WriteLine("5. Kişi Ara");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçiminizi yapın: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1": YeniKisiEkle(); break;
                    case "2": KisileriListele(); break;
                    case "3": KisiSil(); break;
                    case "4": KisiGuncelle(); break;
                    case "5": KisiAra(); break;
                    case "6": Environment.Exit(0); break;
                    default: Console.WriteLine("Geçersiz seçim!"); break;
                }
            }
        }

        static void KisileriYukle()
        {
            if (File.Exists(dosyaYolu))
            {
                var satirlar = File.ReadAllLines(dosyaYolu);
                foreach (var satir in satirlar)
                {
                    var bilgiler = satir.Split(',');
                    if (bilgiler.Length == 3)
                    {
                        rehber.Add(new Kisi { Ad = bilgiler[0], Soyad = bilgiler[1], Telefon = bilgiler[2] });
                    }
                }
            }
        }

        static void KisileriKaydet()
        {
            var satirlar = rehber.Select(k => $"{k.Ad},{k.Soyad},{k.Telefon}");
            File.WriteAllLines(dosyaYolu, satirlar);
        }

        static void YeniKisiEkle()
        {
            Console.Write("Ad: ");
            string ad = Console.ReadLine();

            Console.Write("Soyad: ");
            string soyad = Console.ReadLine();

            Console.Write("Telefon Numarası: ");
            string telefon = Console.ReadLine();

            rehber.Add(new Kisi { Ad = ad, Soyad = soyad, Telefon = telefon });
            KisileriKaydet();
            Console.WriteLine("Kişi başarıyla eklendi!");
        }

        static void KisileriListele()
        {
            if (!rehber.Any())
            {
                Console.WriteLine("Rehber boş!");
                return;
            }

            Console.WriteLine("\nRehberdeki Kişiler:");
            foreach (var kisi in rehber)
            {
                Console.WriteLine($"Ad: {kisi.Ad}, Soyad: {kisi.Soyad}, Telefon: {kisi.Telefon}");
            }
        }

        static void KisiSil()
        {
            Console.Write("Silinecek kişinin adını girin: ");
            string ad = Console.ReadLine();

            var kisi = rehber.FirstOrDefault(k => k.Ad.Equals(ad, StringComparison.OrdinalIgnoreCase));

            if (kisi != null)
            {
                rehber.Remove(kisi);
                KisileriKaydet();
                Console.WriteLine("Kişi başarıyla silindi!");
            }
            else
            {
                Console.WriteLine("Kişi bulunamadı!");
            }
        }

        static void KisiGuncelle()
        {
            Console.Write("Güncellenecek kişinin adını girin: ");
            string ad = Console.ReadLine();

            var kisi = rehber.FirstOrDefault(k => k.Ad.Equals(ad, StringComparison.OrdinalIgnoreCase));

            if (kisi != null)
            {
                Console.Write("Yeni Telefon Numarası: ");
                kisi.Telefon = Console.ReadLine();
                KisileriKaydet();
                Console.WriteLine("Kişi başarıyla güncellendi!");
            }
            else
            {
                Console.WriteLine("Kişi bulunamadı!");
            }
        }

        static void KisiAra()
        {
            Console.Write("Aranacak kişinin adını girin: ");
            string ad = Console.ReadLine();

            var bulunanKisiler = rehber.Where(k => k.Ad.IndexOf(ad, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (bulunanKisiler.Any())
            {
                Console.WriteLine("\nBulunan Kişiler:");
                foreach (var kisi in bulunanKisiler)
                {
                    Console.WriteLine($"Ad: {kisi.Ad}, Soyad: {kisi.Soyad}, Telefon: {kisi.Telefon}");
                }
            }
            else
            {
                Console.WriteLine("Kişi bulunamadı!");
            }
        }
    }

    class Kisi
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
    }
}
