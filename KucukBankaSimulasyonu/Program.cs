using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KucukBankaSimulasyonu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var baglam = new BankaDbContext())
            {
                Console.WriteLine("Veritabanı kontrol ediliyor...");
                baglam.Database.CreateIfNotExists();

                if (!baglam.Musteriler.Any())
                {
                    Console.WriteLine("Sistemde müşteri bulunamadı, örnek veriler oluşturuluyor...");

                    var m1 = new BireyselMusteri(101, "Ayşe Kara", "B-101-HSP");
                    var m2 = new KurumsalMusteri(201, "ABC Yazılım A.Ş.", "K-201-HSP", "1234567890");

                    baglam.Musteriler.Add(m1);
                    baglam.Musteriler.Add(m2);
                    baglam.SaveChanges();

                    m1.HesabaParaYatir(500m);
                    m2.HesabaParaYatir(1000m);
                    baglam.SaveChanges();

                    Console.WriteLine("Örnek müşteriler oluşturuldu.");
                }
            }

            Console.WriteLine("### Banka Simülasyonu Başlatıldı ###");

            using (var baglam = new BankaDbContext())
            {
                var aktifMusteri = baglam.Musteriler
                    .Include(m => m.MusteriHesabi)
                    .OrderBy(m => m.MusteriId)
                    .First();

                SimulasyonuBaslat(aktifMusteri);
            }
        }

        private static void SimulasyonuBaslat(Musteri mevcutMusteri)
        {
            bool devam = true;
            Console.WriteLine($"\nHoş geldiniz, {mevcutMusteri.AdSoyad}");

            while (devam)
            {
                Console.WriteLine("\n-------------------------------------");
                Console.WriteLine("1 - Bakiye Sorgula");
                Console.WriteLine("2 - Para Yatır");
                Console.WriteLine("3 - Para Çek");
                Console.WriteLine("4 - Bilgileri Göster");
                Console.WriteLine("5 - Para Transferi Yap");
                Console.WriteLine("6 - Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        Console.WriteLine($"Bakiyeniz: {mevcutMusteri.MusteriHesabi.Bakiye} TL");
                        break;

                    case "2":
                        ParaIslemiYap(mevcutMusteri, "YATIR");
                        break;

                    case "3":
                        ParaIslemiYap(mevcutMusteri, "CEK");
                        break;

                    case "4":
                        mevcutMusteri.BilgileriGoster();
                        break;

                    case "5":
                        TransferMenusu(mevcutMusteri);
                        break;

                    case "6":
                        devam = false;
                        break;

                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }
        }

        private static void ParaIslemiYap(Musteri musteri, string tip)
        {
            Console.Write($"Lütfen {tip}MAK istediğiniz miktarı girin: ");

            try
            {
                decimal miktar = Convert.ToDecimal(Console.ReadLine());

                using (var baglam = new BankaDbContext())
                {
                    var dbMusteri = baglam.Musteriler
                        .Include(m => m.MusteriHesabi)
                        .First(m => m.MusteriId == musteri.MusteriId);

                    bool basarili;

                    if (tip == "YATIR")
                    {
                        dbMusteri.HesabaParaYatir(miktar);
                        basarili = true;
                    }
                    else
                    {
                        basarili = dbMusteri.HesaptanParaCek(miktar);
                    }

                    if (!basarili)
                    {
                        Console.WriteLine("Yetersiz bakiye.");
                        return;
                    }

                    baglam.Islemler.Add(new Islem
                    {
                        MusteriId = dbMusteri.MusteriId,
                        Miktar = miktar,
                        IslemTarihi = DateTime.Now,
                        IslemTipi = tip == "YATIR" ? "Para Yatırma" : "Para Çekme"
                    });

                    baglam.SaveChanges();

                    musteri.MusteriHesabi.Bakiye = dbMusteri.MusteriHesabi.Bakiye;

                    Console.WriteLine("İşlem başarıyla tamamlandı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        private static void TransferMenusu(Musteri gonderen)
        {
            using (var baglam = new BankaDbContext())
            {
                var dbGonderen = baglam.Musteriler
                    .Include(m => m.MusteriHesabi)
                    .First(m => m.MusteriId == gonderen.MusteriId);

                var alici = baglam.Musteriler
                    .Include(m => m.MusteriHesabi)
                    .FirstOrDefault(m => m.MusteriId != gonderen.MusteriId);

                if (alici == null)
                {
                    Console.WriteLine("Transfer yapılacak başka müşteri yok.");
                    return;
                }

                Console.WriteLine($"Alıcı: {alici.AdSoyad}");
                Console.Write("Miktar: ");
                decimal miktar = Convert.ToDecimal(Console.ReadLine());

                if (!dbGonderen.HesaptanParaCek(miktar))
                {
                    Console.WriteLine("Yetersiz bakiye.");
                    return;
                }

                alici.HesabaParaYatir(miktar);

                // 🔴 GÖNDEREN LOG
                baglam.Islemler.Add(new Islem
                {
                    MusteriId = dbGonderen.MusteriId,
                    Miktar = miktar,
                    IslemTarihi = DateTime.Now,
                    IslemTipi = "Para Transferi (Çıkış)"
                });

                // 🔴 ALICI LOG
                baglam.Islemler.Add(new Islem
                {
                    MusteriId = alici.MusteriId,
                    Miktar = miktar,
                    IslemTarihi = DateTime.Now,
                    IslemTipi = "Para Transferi (Giriş)"
                });

                baglam.SaveChanges();

                // 🔴 RAM senkronizasyonu
                gonderen.MusteriHesabi.Bakiye = dbGonderen.MusteriHesabi.Bakiye;

                Console.WriteLine("Transfer başarıyla tamamlandı.");
            }
        }
    }
}
