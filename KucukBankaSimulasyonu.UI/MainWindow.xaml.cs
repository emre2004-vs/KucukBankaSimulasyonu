using System.Windows;
using KucukBankaSimulasyonu.UI.Services;
using KucukBankaSimulasyonu.UI.Views;

namespace KucukBankaSimulasyonu.UI
{
    public partial class MainWindow : Window
    {
        private readonly BankaService _bankaService;
        private Musteri _aktifMusteri;

        public MainWindow()
        {
            InitializeComponent();

            _bankaService = new BankaService();
            Yukle();

            // 🔹 Açılışta Dashboard göster
            MainContent.Content = new DashboardView(_aktifMusteri);
        }

        private void Yukle()
        {
            _aktifMusteri = _bankaService.AktifMusteriyiGetir();
            TxtAdSoyad.Text = $"Hoş geldin, {_aktifMusteri.AdSoyad}";
        }

        private void AnaSayfa_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DashboardView(_aktifMusteri);
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            var view = new TransferView(_aktifMusteri.MusteriId);
            MainContent.Content = view;
        }



        private void Islemler_Click(object sender, RoutedEventArgs e)
        {
            var islemler =
                _bankaService.MusteriIslemleriniGetir(_aktifMusteri.MusteriId);

            var pencere = new IslemlerView(islemler)
            {
                Owner = this
            };

            pencere.ShowDialog();
        }


        private void MusteriBilgileri_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CustomerInfoView(_aktifMusteri);
        }
    }
}
