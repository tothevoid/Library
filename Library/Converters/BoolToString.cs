using System;
using System.Globalization;
using System.Windows.Data;

namespace Library.Converters
{
    public class BoolToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;

            if (val == true)
                return "Да";
            else
                return "Нет";

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

