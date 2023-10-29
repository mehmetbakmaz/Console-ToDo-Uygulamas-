using System;
using System.Collections.Generic;
using System.Linq;

class Kart
{
    public int Id { get; set; }
    public string Baslik { get; set; }
    public string Icerik { get; set; }
    public int AtananKisiId { get; set; }
    public string Buyukluk { get; set; }
    public KartDurumu Durum { get; set; }
}

enum KartDurumu
{
    TODO,
    INPROGRESS,
    DONE
}

class TakimUyesi
{
    public int Id { get; set; }
    public string Isim { get; set; }
}

class Program
{
    static List<TakimUyesi> Takim = new List<TakimUyesi>
    {
        new TakimUyesi { Id = 1, Isim = "Ali" },
        new TakimUyesi { Id = 2, Isim = "Ayşe" },
        new TakimUyesi { Id = 3, Isim = "Mehmet" }
    };

    static List<Kart> kartlar = new List<Kart>
    {
        new Kart { Id = 1, Baslik = "Task 1", Icerik = "Task 1 content", AtananKisiId = 1, Buyukluk = "M", Durum = KartDurumu.TODO },
        new Kart { Id = 2, Baslik = "Task 2", Icerik = "Task 2 content", AtananKisiId = 2, Buyukluk = "S", Durum = KartDurumu.INPROGRESS },
        new Kart { Id = 3, Baslik = "Task 3", Icerik = "Task 3 content", AtananKisiId = 3, Buyukluk = "L", Durum = KartDurumu.DONE }
    };

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Kart Ekle");
            Console.WriteLine("2. Kart Güncelle");
            Console.WriteLine("3. Kart Sil");
            Console.WriteLine("4. Kart Taşı");
            Console.WriteLine("5. Kartları Listele");
            Console.WriteLine("6. Çıkış");

            var secim = Convert.ToInt32(Console.ReadLine());

            switch (secim)
            {
                case 1:
                    KartEkle();
                    break;
                case 2:
                    KartGuncelle();
                    break;
                case 3:
                    KartSil();
                    break;
                case 4:
                    KartTasi();
                    break;
                case 5:
                    KartlariListele();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Lütfen geçerli bir seçim yapın!");
                    break;
            }
        }
    }

    static void KartEkle()
    {
        Console.Write("Baslik: ");
        string baslik = Console.ReadLine();

        Console.Write("Icerik: ");
        string icerik = Console.ReadLine();

        Console.WriteLine("Takim Uyeleri:");
        foreach (var uye in Takim)
        {
            Console.WriteLine($"{uye.Id}. {uye.Isim}");
        }

        Console.Write("Atanan Kisi ID: ");
        int atananKisiId = Convert.ToInt32(Console.ReadLine());

        Console.Write("Buyukluk (XS, S, M, L, XL): ");
        string buyukluk = Console.ReadLine();

        var kart = new Kart
        {
            Id = kartlar.Max(k => k.Id) + 1, // En son ID'yi bulup bir artırarak yeni bir ID oluştur
            Baslik = baslik,
            Icerik = icerik,
            AtananKisiId = atananKisiId,
            Buyukluk = buyukluk,
            Durum = KartDurumu.TODO
        };

        kartlar.Add(kart);
        Console.WriteLine("Kart başarıyla eklendi!");
    }

    static void KartGuncelle()
    {
        Console.Write("Güncellenecek Kart ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var kart = kartlar.FirstOrDefault(k => k.Id == id);
        if (kart == null)
        {
            Console.WriteLine("Kart bulunamadı!");
            return;
        }

        Console.Write($"Yeni Baslik (Mevcut: {kart.Baslik}): ");
        kart.Baslik = Console.ReadLine();

        Console.Write($"Yeni Icerik (Mevcut: {kart.Icerik}): ");
        kart.Icerik = Console.ReadLine();

        Console.WriteLine("Takim Uyeleri:");
        foreach (var uye in Takim)
        {
            Console.WriteLine($"{uye.Id}. {uye.Isim}");
        }

        Console.Write($"Yeni Atanan Kisi ID (Mevcut: {kart.AtananKisiId}): ");
        kart.AtananKisiId = Convert.ToInt32(Console.ReadLine());

        Console.Write($"Yeni Buyukluk (Mevcut: {kart.Buyukluk}, XS, S, M, L, XL): ");
        kart.Buyukluk = Console.ReadLine();

        Console.WriteLine("Kart başarıyla güncellendi!");
    }

    static void KartSil()
    {
        Console.Write("Silinecek Kart ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var kart = kartlar.FirstOrDefault(k => k.Id == id);
        if (kart == null)
        {
            Console.WriteLine("Kart bulunamadı!");
            return;
        }

        kartlar.Remove(kart);
        Console.WriteLine("Kart başarıyla silindi!");
    }

    static void KartTasi()
    {
        Console.Write("Taşınacak Kart ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        var kart = kartlar.FirstOrDefault(k => k.Id == id);
        if (kart == null)
        {
            Console.WriteLine("Kart bulunamadı!");
            return;
        }

        Console.WriteLine("1. TODO");
        Console.WriteLine("2. IN PROGRESS");
        Console.WriteLine("3. DONE");
        Console.Write("Taşınacak durumu seçin: ");
        int secim = Convert.ToInt32(Console.ReadLine());

        switch (secim)
        {
            case 1:
                kart.Durum = KartDurumu.TODO;
                break;
            case 2:
                kart.Durum = KartDurumu.INPROGRESS;
                break;
            case 3:
                kart.Durum = KartDurumu.DONE;
                break;
            default:
                Console.WriteLine("Geçersiz seçim!");
                return;
        }

        Console.WriteLine("Kart başarıyla taşındı!");
    }

    static void KartlariListele()
    {
        Console.WriteLine("TODO");
        foreach (var kart in kartlar.Where(k => k.Durum == KartDurumu.TODO))
        {
            Console.WriteLine($"ID: {kart.Id}, Baslik: {kart.Baslik}, Atanan: {Takim.FirstOrDefault(u => u.Id == kart.AtananKisiId)?.Isim}");
        }

        Console.WriteLine("IN PROGRESS");
        foreach (var kart in kartlar.Where(k => k.Durum == KartDurumu.INPROGRESS))
        {
            Console.WriteLine($"ID: {kart.Id}, Baslik: {kart.Baslik}, Atanan: {Takim.FirstOrDefault(u => u.Id == kart.AtananKisiId)?.Isim}");
        }

        Console.WriteLine("DONE");
        foreach (var kart in kartlar.Where(k => k.Durum == KartDurumu.DONE))
        {
            Console.WriteLine($"ID: {kart.Id}, Baslik: {kart.Baslik}, Atanan: {Takim.FirstOrDefault(u => u.Id == kart.AtananKisiId)?.Isim}");
        }
    }
}
