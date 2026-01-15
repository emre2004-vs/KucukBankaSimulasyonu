using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KucukBankaSimulasyonu
{
    public class KurumsalMusteri : Musteri
    {
        public string VergiNo { get; set; }

       
        public KurumsalMusteri()
        {
            
        }

        
        public KurumsalMusteri(int id, string adSoyad, string hesapNo, string vergiNo)
            : base(id, adSoyad, hesapNo)
        {
            VergiNo = vergiNo;
        }

        public override void BilgileriGoster()
        {
            Console.WriteLine($"\n--- Kurumsal Müşteri ---");
            Console.WriteLine($"ID: {MusteriId}");
            Console.WriteLine($"Ünvan: {AdSoyad}");
            Console.WriteLine($"Vergi No: {VergiNo}");
            Console.WriteLine($"Hesap No: {MusteriHesabi.HesapNo}");
            Console.WriteLine($"Bakiye: {MusteriHesabi.Bakiye} TL");
        }
    }
}
