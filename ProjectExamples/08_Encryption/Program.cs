using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static string filePath = "sifreler.txt";
    static Dictionary<string, string> sifreler = new Dictionary<string, string>();

    static void Main(string[] args)
    {
        DosyadanVerileriYukle();

        while (true)
        {
            Console.WriteLine("\n--- Şifre Saklama Uygulaması ---");
            Console.WriteLine("1 - Şifre Ekle");
            Console.WriteLine("2 - Şifreleri Listele");
            Console.WriteLine("3 - Şifre Sil");
            Console.WriteLine("4 - Rastgele Şifre Üret");
            Console.WriteLine("5 - Çıkış");
            Console.Write("Seçiminizi yapın: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    SifreEkle();
                    break;
                case "2":
                    SifreleriListele();
                    break;
                case "3":
                    SifreSil();
                    break;
                case "4":
                    RastgeleSifreUret();
                    break;
                case "5":
                    DosyayaVerileriKaydet();
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                    break;
            }
        }
    }

    static void SifreEkle()
    {
        Console.Write("Kullanıcı Adı Girin: ");
        string kullaniciAdi = Console.ReadLine();

        if (sifreler.ContainsKey(kullaniciAdi))
        {
            Console.WriteLine("Bu kullanıcı zaten mevcut!");
            return;
        }

        Console.Write("Şifre Girin: ");
        string sifre = Console.ReadLine();
        sifreler[kullaniciAdi] = sifre;
        Console.WriteLine("Şifre başarıyla eklendi.");
    }

    static void SifreleriListele()
    {
        if (!sifreler.Any())
        {
            Console.WriteLine("Kayıtlı şifre bulunmamaktadır.");
            return;
        }

        Console.WriteLine("\n--- Kayıtlı Şifreler ---");
        foreach (var item in sifreler)
        {
            Console.WriteLine($"Kullanıcı: {item.Key}, Şifre: {item.Value}");
        }
    }

    static void SifreSil()
    {
        Console.Write("Silmek istediğiniz kullanıcı adını girin: ");
        string kullaniciAdi = Console.ReadLine();

        if (sifreler.Remove(kullaniciAdi))
        {
            Console.WriteLine("Şifre başarıyla silindi.");
        }
        else
        {
            Console.WriteLine("Belirtilen kullanıcı bulunamadı.");
        }
    }

    static void RastgeleSifreUret()
    {
        Console.Write("Şifre uzunluğunu girin: ");
        if (int.TryParse(Console.ReadLine(), out int uzunluk))
        {
            string sifre = GenerateRandomPassword(uzunluk);
            Console.WriteLine($"Oluşturulan Şifre: {sifre}");
        }
        else
        {
            Console.WriteLine("Geçersiz uzunluk değeri!");
        }
    }

    static string GenerateRandomPassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        StringBuilder res = new StringBuilder();
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] uintBuffer = new byte[sizeof(uint)];

            while (length-- > 0)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                res.Append(validChars[(int)(num % (uint)validChars.Length)]);
            }
        }
        return res.ToString();
    }

    static void DosyayaVerileriKaydet()
    {
        List<string> lines = sifreler.Select(s => $"{s.Key}:{s.Value}").ToList();
        File.WriteAllLines(filePath, lines);
    }

    static void DosyadanVerileriYukle()
    {
        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(':');
            if (parts.Length == 2)
            {
                sifreler[parts[0]] = parts[1];
            }
        }
    }
}
