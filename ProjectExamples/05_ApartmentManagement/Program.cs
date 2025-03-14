using System;
using System.Collections.Generic;

class Program
{
    static List<Resident> residents = new List<Resident>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("C# Apartmanına Hoşgeldiniz");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("1- Apartman sakinlerini Listele");
            Console.WriteLine("2- Apartmana yeni üye oluşturma");
            Console.WriteLine("3- Ödenen aidat");
            Console.WriteLine("4- Borç Sorgulama");
            Console.WriteLine("5- Çıkış");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ListResidents();
                    break;
                case 2:
                    AddResident();
                    break;
                case 3:
                    MarkPaid();
                    break;
                case 4:
                    CheckDebt();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }
    }

    // Apartman sakinlerini listeleme
    static void ListResidents()
    {
        if (residents.Count == 0)
        {
            Console.WriteLine("Hiç apartman sakini yok.");
        }
        else
        {
            Console.WriteLine("Apartman Sakinleri:");
            foreach (var resident in residents)
            {
                Console.WriteLine($"Adı: {resident.Name}, Taşınma Tarihi: {resident.MoveInDate.ToShortDateString()}, Aidat Durumu: {(resident.IsPaid ? "Ödendi" : "Ödenmedi")}");
            }
        }
    }

    // Apartmana yeni üye oluşturma
    static void AddResident()
    {
        Console.Write("Sakinin adı: ");
        string name = Console.ReadLine();

        Console.Write("Taşınma tarihi (yyyy-MM-dd): ");
        DateTime moveInDate = DateTime.Parse(Console.ReadLine());

        residents.Add(new Resident(name, moveInDate));
        Console.WriteLine("Yeni üye başarıyla eklendi.");
    }

    // Aidat ödeme durumu güncelleme
    static void MarkPaid()
    {
        Console.Write("Aidat ödeyen sakinin adını girin: ");
        string name = Console.ReadLine();

        var resident = residents.Find(r => r.Name == name);
        if (resident != null)
        {
            Console.Write("Kaç aylık aidat ödeyeceksiniz? ");
            int monthsToPay = int.Parse(Console.ReadLine());

            int paymentAmount = monthsToPay * 300; // Ödenecek toplam tutar
            int monthsSinceMoveIn = (DateTime.Now - resident.MoveInDate).Days / 30;
            int totalDebt = 300 * monthsSinceMoveIn; // Toplam borç hesaplanıyor

            // Eğer ödenen miktar borcu karşılıyorsa
            if (paymentAmount >= totalDebt)
            {
                resident.IsPaid = true; // Aidat ödendi
                Console.WriteLine($"{resident.Name} {monthsToPay} aylık aidat ödedi. Borcu tamamen ödendi.");
            }
            else
            {
                // Ödenen miktar borcu karşılamıyorsa
                Console.WriteLine($"{resident.Name} {monthsToPay} aylık aidat ödedi. Kalan borç: {totalDebt - paymentAmount} TL.");
            }
        }
        else
        {
            Console.WriteLine("Sakin bulunamadı.");
        }
    }

    // Borç sorgulama
    static void CheckDebt()
    {
        Console.Write("Borç sorgulamak için sakinin adını girin: ");
        string name = Console.ReadLine();

        var resident = residents.Find(r => r.Name == name);
        if (resident != null)
        {
            int daysSinceMoveIn = (DateTime.Now - resident.MoveInDate).Days;
            int monthsSinceMoveIn = daysSinceMoveIn / 30;
            int totalDebt = 300 * monthsSinceMoveIn; // Borç hesaplanıyor

            if (resident.IsPaid)
            {
                Console.WriteLine($"{resident.Name} aidatını ödedi. Borç yok.");
            }
            else
            {
                Console.WriteLine($"{resident.Name}'ın toplam borcu: {totalDebt} TL");
            }
        }
        else
        {
            Console.WriteLine("Sakin bulunamadı.");
        }
    }
}

class Resident
{
    public string Name { get; set; }
    public DateTime MoveInDate { get; set; }
    public bool IsPaid { get; set; }

    public Resident(string name, DateTime moveInDate)
    {
        Name = name;
        MoveInDate = moveInDate;
        IsPaid = false; // Başlangıçta aidat ödenmemiş sayılır.
    }
}
