using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KucukBankaSimulasyonu
{
    public class BankaTransferYoneticisi : ITransferServisi
    {
        public bool TransferYap(Hesap gonderen, Hesap alici, decimal miktar)
        {
            Console.WriteLine("\n--- Transfer İşlemi Kontrol Ediliyor ---");

            if (gonderen.ParaCek(miktar))
            {
                alici.ParaYatir(miktar);
                Console.WriteLine("İşlem Başarılı: Transfer tamamlandı.");
                return true;
            }

            Console.WriteLine("İşlem Başarısız: Yetersiz bakiye.");
            return false;
        }
    }
}
