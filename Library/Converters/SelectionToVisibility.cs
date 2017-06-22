using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Library.Converters
{
    public class SelectionToVisibility: IValueConverter
    {
        
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if ((int)value >= 5)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType,
                object parameter, CultureInfo culture)
            {
                return null;
            }
       
    }
}
