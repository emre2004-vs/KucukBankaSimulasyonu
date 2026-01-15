using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;


namespace KucukBankaSimulasyonu
{
    public class Hesap
    {
    
        [Key, ForeignKey(nameof(Musteri))]
        public int MusteriId { get; set; }
        

        [Required]
        [StringLength(20)]
        public string HesapNo { get; set; }

        public decimal Bakiye { get; set; }

        public DateTime AcilisTarihi { get; set; }

        public virtual Musteri Musteri { get; set; }

        public Hesap()
        {
            AcilisTarihi = DateTime.Now;
            Bakiye = 0m;
        }
        

        public Hesap(string hesapNo)
        {
            HesapNo = hesapNo;
            Bakiye = 0m;
            AcilisTarihi = DateTime.Now;
        }
        

        public decimal BakiyeGoster()
        {
            return Bakiye;
        }

        public void ParaYatir(decimal miktar)
        {
            if (miktar <= 0)
                throw new ArgumentException("Yatırılan miktar pozitif olmalıdır.");

            Bakiye += miktar;
        }
        

        public bool ParaCek(decimal miktar)
        {
            if (miktar <= 0)
                throw new ArgumentException("Çekilen miktar pozitif olmalıdır.");

            if (miktar > Bakiye)
                return false;

            Bakiye -= miktar;
            return true;
        }
    }
}