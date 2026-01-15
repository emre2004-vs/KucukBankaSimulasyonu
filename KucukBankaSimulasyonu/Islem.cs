using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KucukBankaSimulasyonu
{
    public class Islem
    {
        [Key]
        public int IslemId { get; set; } 
        public decimal Miktar { get; set; }
        public DateTime IslemTarihi { get; set; }
        public string IslemTipi { get; set; } 

        
        public int MusteriId { get; set; }

        [ForeignKey(nameof(MusteriId))]
        public virtual Musteri Musteri { get; set; }

    }
}
