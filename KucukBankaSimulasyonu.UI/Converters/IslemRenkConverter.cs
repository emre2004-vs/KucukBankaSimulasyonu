using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace KucukBankaSimulasyonu.UI.Converters
{
    public class IslemRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Black;

            string islemTipi = value.ToString();

            if (islemTipi.Contains("Yatır"))
                return Brushes.Green;

            if (islemTipi.Contains("Çek"))
                return Brushes.Red;

            if (islemTipi.Contains("Transfer"))
                return Brushes.SteelBlue;

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
