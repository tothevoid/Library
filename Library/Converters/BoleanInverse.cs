using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Library.Converters
{
    public class BooleanInverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vis = (Visibility)value;

            var res = Visibility.Collapsed;

            if (vis == Visibility.Collapsed)
                res = Visibility.Visible;

            return res;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
