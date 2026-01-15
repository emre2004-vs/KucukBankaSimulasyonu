using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KucukBankaSimulasyonu
{
   
    public class BankaDbContext : DbContext
    {
        
        public BankaDbContext() : base("BankaDB_Baglantisi") { }
       
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Hesap> Hesaplar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
    }
}
