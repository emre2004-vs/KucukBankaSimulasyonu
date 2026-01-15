using System.Windows;
using System.Windows.Controls;

namespace KucukBankaSimulasyonu.UI.Views
{
    public partial class TransferView : Page
    {
        private int _musteriId;

        public TransferView(int musteriId)
        {
            InitializeComponent();
            _musteriId = musteriId;
        }

        private void TransferYap_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Transfer yapıldı. Müşteri ID: {_musteriId}");
        }
    }
}
