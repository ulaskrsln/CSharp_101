using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_GPA_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Not Hesaplama Sistemine Hoşgeldiniz.");
            Console.WriteLine("(2 vize(%30+%30) + 1 final(%40) şeklinde hesaplama yapılacak.)");
            Console.Write("Lütfen ilk vize notunuzu giriniz:");
            double vize1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Lütfen ikinci vize notunuzu giriniz:");
            double vize2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Lütfen final notunuzu giriniz:");
            double final = Convert.ToDouble(Console.ReadLine());

            double GPA = (vize1 * 0.3) + (vize2 * 0.3) + (final * 0.4);

            Console.WriteLine("Not Ortalamanız: " + GPA);

            if (GPA >= 90)
            {
                Console.WriteLine("Harf Notunuz: AA");
            }
            else if (GPA >= 80)
            {
                Console.WriteLine("Harf Notunuz: BA");
            }
            else if (GPA >= 70)
            {
                Console.WriteLine("Harf Notunuz: BB");
            }
            else if (GPA >= 60)
            {
                Console.WriteLine("Harf Notunuz: CB");
            }
            else if (GPA >= 55)
            {
                Console.WriteLine("Harf Notunuz: CC");
            }
            else if (GPA >= 50)
            {
                Console.WriteLine("Harf Notunuz: DC");
            }
            else if (GPA >= 40)
            {
                Console.WriteLine("Harf Notunuz: DD");
            }
            else if (GPA >= 25)
            {
                Console.WriteLine("Harf Notunuz: FD");
            }
            else
            {
                Console.WriteLine("Harf Notunuz: FF");
            }
        }
    }
}
