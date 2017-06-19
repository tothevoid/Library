using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Library.Converters
{
    public class WidthToSign : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = (GridLength)value;
            if (res.Value == 300)
                return ">";
            else
                return "<";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
