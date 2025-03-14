using System;

class Program
{
    static string[] birler = { "", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz" };
    static string[] onlar = { "", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
    static string[] binler = { "", "bin", "milyon", "milyar", "trilyon" };

    static void Main(string[] args)
    {
        Console.Write("Bir sayı girin: ");
        if (long.TryParse(Console.ReadLine(), out long sayi))
        {
            if (sayi == 0)
                Console.WriteLine("Sıfır");
            else
                Console.WriteLine($"Yazıyla: {SayiyiYaziyaCevir(sayi)}");
        }
        else
        {
            Console.WriteLine("Geçersiz sayı girdiniz!");
        }
    }

    static string SayiyiYaziyaCevir(long sayi)
    {
        if (sayi == 0) return "sıfır";

        string sonuc = "";
        int grupSayaci = 0;

        while (sayi > 0)
        {
            int grup = (int)(sayi % 1000);
            if (grup != 0)
            {
                string grupYazi = GrupYaziyaCevir(grup);
                if (grup == 1 && grupSayaci == 1)
                {
                    sonuc = $"{binler[grupSayaci]} {sonuc}";
                }
                else
                {
                    sonuc = $"{grupYazi} {binler[grupSayaci]} {sonuc}";
                }
            }
            sayi /= 1000;
            grupSayaci++;
        }

        return sonuc.Trim();
    }

    static string GrupYaziyaCevir(int grup)
    {
        string yazi = "";

        int yuzler = grup / 100;
        int onlarBasamak = (grup % 100) / 10;
        int birlerBasamak = grup % 10;

        if (yuzler != 0)
        {
            yazi += (yuzler == 1 ? "yüz" : $"{birler[yuzler]} yüz");
        }

        if (onlarBasamak != 0)
        {
            yazi += $" {onlar[onlarBasamak]}";
        }

        if (birlerBasamak != 0)
        {
            yazi += $" {birler[birlerBasamak]}";
        }

        return yazi.Trim();
    }
}
