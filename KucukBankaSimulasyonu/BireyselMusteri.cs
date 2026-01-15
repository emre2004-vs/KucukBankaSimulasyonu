using System;

namespace KucukBankaSimulasyonu
{
    public class BireyselMusteri : Musteri 
    {
        
        protected BireyselMusteri() : base()
        {
        }

       
        public BireyselMusteri(int id, string adSoyad, string hesapNo)
            : base(id, adSoyad, hesapNo)
        {
            
        }
        
    }
}
