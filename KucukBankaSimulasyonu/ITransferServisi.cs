using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KucukBankaSimulasyonu
{
    // ITransferServisi.cs
    public interface ITransferServisi
    {
        bool TransferYap(Hesap gonderen, Hesap alici, decimal miktar);
    }
}
