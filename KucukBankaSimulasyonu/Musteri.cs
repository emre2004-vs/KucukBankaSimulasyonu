using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KucukBankaSimulasyonu
{
    public class Musteri
    {
        [Key]
        public int MusteriId { get; set; }
        public string AdSoyad { get; set; }

        
        public virtual Hesap MusteriHesabi { get; set; }

       
        public Musteri() { }
        

        
        public Musteri(int id, string adSoyad, string hesapNo)
        {
            MusteriId = id;
            AdSoyad = adSoyad;

           
            MusteriHesabi = new Hesap(hesapNo);
        }

        
        public void HesabaParaYatir(decimal miktar)
        {
            MusteriHesabi.ParaYatir(miktar);
        }

       
        
        public bool HesaptanParaCek(decimal miktar)
        {
            
            return MusteriHesabi.ParaCek(miktar);
        }
        
        public virtual void BilgileriGoster()
        {
            Console.WriteLine($"\n--- Müşteri Bilgisi (GENEL) ---");
            Console.WriteLine($"ID: {MusteriId}, Ad: {AdSoyad}");

            Console.WriteLine($"Hesap No: {MusteriHesabi.HesapNo}, Bakiye: {MusteriHesabi.Bakiye} TL");
        }
    }
}