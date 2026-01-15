using System.Collections.Generic;
using System.Windows;

namespace KucukBankaSimulasyonu.UI.Views
{
    public partial class IslemlerView : Window
    {
        // 🔹 XAML Binding buraya bakar
        public List<Islem> Islemler { get; }

        public IslemlerView(List<Islem> islemler)
        {
            InitializeComponent();

            Islemler = islemler;

            // 🔹 Binding çalışsın diye
            DataContext = this;
        }
    }
}
