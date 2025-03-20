using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//DosyalarıYule()
//Kullanıcıadi()
//Sifre()
//DosyayyKaydet()


namespace _11_LoginApp
{
   
    class Program
    {
        static string dosyayolu = "kullanicilar.txt";   //ullanıcıları tutcağımız dosya
        static List<string> kullanicilar = new List<string>();  //Kullanıcıları tutan liste
        static void Main(string[] args)
        {
            DosyalarıYukle(); // Program başlarken dosyadan kullanıcıları yükle
            bool i = true;

            while (i == true)
            {
                Console.WriteLine("\n--- GİRİŞ EKRANI ---");
                Console.WriteLine("1 - Kullanıcı Ekle");
                Console.WriteLine("2 - Giriş Yap");
                Console.WriteLine("3 - Çıkış");
                Console.Write("Seçiminizi yapın: ");
                string secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":
                        KullaniciEkle();
                        break;
                    case "2":
                        GirisYap();
                        i=false;

                        break;
                    case "3":
                        DosyayiKaydet(); // Çıkarken kullanıcıları kaydet
                        Console.WriteLine("Kullanıcılar kaydedildi. Programdan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim! Tekrar deneyin.");
                        break;
                }
            }
        }


        static void KullaniciEkle()
        {
            Console.Write("Eklemek istediğiniz kullanıcı adını yazın: ");
            string kullaniciAdi = Console.ReadLine();
            Console.Write("Eklemek istediğiniz şifreyi yazın: ");
            string sifre = Console.ReadLine();
            kullanicilar.Add(kullaniciAdi + " " + sifre);
            Console.WriteLine("Kullanıcı başarıyla eklendi!");
        }

        static void GirisYap()
        {
            Console.Write("Kullanıcı adınızı girin: ");
            string kullaniciAdi = Console.ReadLine();
            Console.Write("Şifrenizi girin: ");
            string sifre = Console.ReadLine();
            if (kullanicilar.Contains(kullaniciAdi + " " + sifre))
            {
                Console.WriteLine("Giriş başarılı! Hoş geldiniz.");
            }
            else
            {
                Console.WriteLine("Kullanıcı adı veya şifre hatalı!");
            }
        }

        static void DosyalarıYukle()
        {
            if (File.Exists(dosyayolu))
            {
                kullanicilar = File.ReadAllLines(dosyayolu).ToList();
                Console.WriteLine("Kullanıcılar yüklendi.");
            }
            else
            {
                Console.WriteLine("Kullanıcılar dosyası bulunamadı. Yeni bir dosya oluşturulacak.");
                File.Create(dosyayolu).Close();
            }
        }

        static void DosyayiKaydet()
        {
            File.WriteAllLines(dosyayolu, kullanicilar);
            Console.WriteLine("Kullanıcılar dosyası kaydedildi.");
        }

    }
}
