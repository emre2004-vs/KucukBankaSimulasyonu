using System;
using System.Windows;

namespace KucukBankaSimulasyonu.UI
{
    public partial class ParaIslemWindow : Window
    {
        public decimal GirilenMiktar { get; private set; }

        public ParaIslemWindow(string baslik)
        {
            InitializeComponent();
            TxtBaslik.Text = baslik;
        }

        private void Onay_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(TxtMiktar.Text, out decimal miktar) || miktar <= 0)
            {
                MessageBox.Show("Geçerli bir tutar giriniz",
                    "Hata",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            GirilenMiktar = miktar;
            DialogResult = true;
        }

        private void Iptal_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
