using KucukBankaSimulasyonu.UI.Services;
using System.Windows.Controls;

namespace KucukBankaSimulasyonu.UI.Views
{
    public partial class CustomerInfoView : Page
    {
        private readonly Musteri _musteri;

        // 🔹 AKTİF MÜŞTERİ DIŞARIDAN ALINIYOR
        public CustomerInfoView(Musteri musteri)
        {
            InitializeComponent();
            _musteri = musteri;
            Yukle();
        }

        private void Yukle()
        {
            TxtAdSoyad.Text = $"Ad Soyad: {_musteri.AdSoyad}";
            TxtMusteriId.Text = $"Müşteri ID: {_musteri.MusteriId}";
            TxtMusteriTipi.Text = $"Müşteri Tipi: {_musteri.GetType().Name}";
            TxtHesapNo.Text = $"Hesap No: {_musteri.MusteriHesabi.HesapNo}";
            TxtBakiye.Text = $"Bakiye: ₺ {_musteri.MusteriHesabi.Bakiye:N2}";

            if (_musteri is KurumsalMusteri kurumsal)
                TxtVergiNo.Text = $"Vergi No: {kurumsal.VergiNo}";
            else
                TxtVergiNo.Text = "Vergi No: -";
        }
    }
}



