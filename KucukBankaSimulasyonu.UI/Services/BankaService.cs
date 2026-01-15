using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KucukBankaSimulasyonu.UI.Services
{
    public class BankaService
    {
        public Musteri AktifMusteriyiGetir()
        {
            using (var db = new BankaDbContext())
            {
                return db.Musteriler
                    .Include(m => m.MusteriHesabi)
                    .OrderBy(m => m.MusteriId)
                    .First();
            }
        }

        public void ParaYatir(int musteriId, decimal tutar)
        {
            
            using (var db = new BankaDbContext())
            {
                
                var musteri = db.Musteriler
                    .Include(m => m.MusteriHesabi)
                    .FirstOrDefault(m => m.MusteriId == musteriId);

                if (musteri != null)
                {
                    
                    musteri.MusteriHesabi.Bakiye += tutar;

                    
                    db.Islemler.Add(new Islem
                    {
                        MusteriId = musteriId,
                        IslemTipi = "Para Yatırma",
                        Miktar = tutar,
                        IslemTarihi = DateTime.Now
                    });

                    
                    db.SaveChanges();
                }
            }
        }


        public void ParaCek(int musteriId, decimal miktar)
        {
            using (var db = new BankaDbContext())
            {
                var musteri = db.Musteriler
                    .Include(m => m.MusteriHesabi)
                    .First(m => m.MusteriId == musteriId);

                if (!musteri.HesaptanParaCek(miktar))
                    throw new System.Exception("Yetersiz bakiye");

                db.Islemler.Add(new Islem
                {
                    MusteriId = musteri.MusteriId,
                    Miktar = miktar,
                    IslemTarihi = DateTime.Now,
                    IslemTipi = "Para Çekme"
                });

                db.SaveChanges();
            }
        }
        public List<Islem> SonIslemleriGetir(int musteriId)
        {
            using (var db = new BankaDbContext())
            {
                return db.Islemler
                         .Where(i => i.MusteriId == musteriId)
                         .OrderByDescending(i => i.IslemTarihi)
                         .Take(5)
                         .ToList();
            }
        }
        public void TransferYap(int gonderenMusteriId, string hedefHesapNo, decimal tutar)

        {
            using (var db = new BankaDbContext())
            {
                var gonderen = db.Musteriler
                    .Include(m => m.MusteriHesabi)
                    .First(m => m.MusteriId == gonderenMusteriId);

                var aliciHesap = db.Hesaplar
                    .FirstOrDefault(h => h.HesapNo == hedefHesapNo);

                if (aliciHesap == null)
                    throw new Exception("Hedef hesap bulunamadı");

                if (!gonderen.HesaptanParaCek(tutar))
                    throw new Exception("Yetersiz bakiye");

                aliciHesap.Bakiye += tutar;

                db.Islemler.Add(new Islem
                {
                    MusteriId = gonderen.MusteriId,
                    Miktar = tutar,
                    IslemTipi = "Transfer (Gönderilen)",
                    IslemTarihi = DateTime.Now
                });

                db.Islemler.Add(new Islem
                {
                    MusteriId = aliciHesap.MusteriId,
                    Miktar = tutar,
                    IslemTipi = "Transfer (Alınan)",
                    IslemTarihi = DateTime.Now
                });

                db.SaveChanges();
            }
        }

        public List<Islem> MusteriIslemleriniGetir(int musteriId)
        {
            using (var db = new BankaDbContext())
            {
                return db.Islemler
                    .Where(i => i.MusteriId == musteriId)
                    .OrderByDescending(i => i.IslemTarihi)
                    .ToList();
            }
        }

        public List<Islem> IslemleriGetir(int musteriId)
        {
            using (var db = new BankaDbContext())
            {
                return db.Islemler
                    .Where(x => x.MusteriId == musteriId)
                    .OrderByDescending(x => x.IslemTarihi)
                    .ToList();
            }
        }


    }
}
