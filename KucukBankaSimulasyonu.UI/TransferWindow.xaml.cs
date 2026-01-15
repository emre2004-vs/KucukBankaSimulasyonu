using System;
using System.Windows;
using KucukBankaSimulasyonu.UI.Services;

namespace KucukBankaSimulasyonu.UI
{
    public partial class TransferWindow : Window
    {
        private readonly BankaService _bankaService;
        private readonly int _gonderenMusteriId;

        public TransferWindow(int musteriId)
        {
            InitializeComponent();
            _bankaService = new BankaService();
            _gonderenMusteriId = musteriId;
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string hedefHesapNo = TxtHedefHesap.Text;
                decimal tutar = decimal.Parse(TxtTutar.Text);

                _bankaService.TransferYap(_gonderenMusteriId, hedefHesapNo, tutar);


                MessageBox.Show("Transfer başarılı ✅");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }
    }
}
