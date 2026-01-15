using System;
using System.Windows;
using System.Windows.Controls;
using KucukBankaSimulasyonu.UI.Services;

namespace KucukBankaSimulasyonu.UI.Views
{
    public partial class DashboardView : UserControl
    {
        private Musteri _aktifMusteri;
        private readonly BankaService _bankaService = new BankaService();

        public DashboardView(Musteri musteri)
        {
            InitializeComponent();

            _aktifMusteri = musteri;
            Yukle();
        }


        // 🔄 BAKİYE + İŞLEMLERİ YENİLE
        private void Yukle()
        {
            // 🔥 EN KRİTİK SATIR
            _aktifMusteri = _bankaService.AktifMusteriyiGetir();

            TxtBakiye.Text = $"₺ {_aktifMusteri.MusteriHesabi.Bakiye:N2}";

            LvIslemler.ItemsSource =
                _bankaService.MusteriIslemleriniGetir(_aktifMusteri.MusteriId);
        }

        private void ParaYatir_Click(object sender, RoutedEventArgs e)
        {
            var popup = new ParaIslemWindow("Para Yatır")
            {
                Owner = Window.GetWindow(this)
            };

            if (popup.ShowDialog() == true)
            {
                _bankaService.ParaYatir(
                    _aktifMusteri.MusteriId,
                    popup.GirilenMiktar);

                Yukle();
            }
        }

        private void ParaCek_Click(object sender, RoutedEventArgs e)
        {
            var popup = new ParaIslemWindow("Para Çek")
            {
                Owner = Window.GetWindow(this)
            };

            try
            {
                if (popup.ShowDialog() == true)
                {
                    _bankaService.ParaCek(
                        _aktifMusteri.MusteriId,
                        popup.GirilenMiktar);

                    Yukle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void TransferToggle_Click(object sender, RoutedEventArgs e)
        {
            TransferPanel.Visibility =
                TransferPanel.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void TransferYap_Click(object sender, RoutedEventArgs e)
        {
            string hedefHesapNo = TxtHedefHesap.Text;

            if (string.IsNullOrWhiteSpace(hedefHesapNo))
            {
                MessageBox.Show("Hedef hesap numarası giriniz");
                return;
            }

            if (!decimal.TryParse(TxtTransferTutar.Text, out decimal tutar) || tutar <= 0)
            {
                MessageBox.Show("Geçerli bir tutar giriniz");
                return;
            }

            try
            {
                _bankaService.TransferYap(
                    _aktifMusteri.MusteriId, // int
                    hedefHesapNo,            // string
                    tutar
                );

                MessageBox.Show("Transfer başarılı ✅");

                TxtHedefHesap.Clear();
                TxtTransferTutar.Clear();
                TransferPanel.Visibility = Visibility.Collapsed;

                Yukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }
    }
}
