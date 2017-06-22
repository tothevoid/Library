using System;
using System.Globalization;
using System.Windows.Data;

namespace Library.Converters
{
    public class QuantityToEnable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string quantity = (string)value;
            int res = 0;
            int.TryParse(quantity, out res);
            if (res == 0)
            {
                return false;
            }
            return true;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

