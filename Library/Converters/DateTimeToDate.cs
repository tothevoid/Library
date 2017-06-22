using System;
using System.Globalization;
using System.Windows.Data;

namespace Library.Converters
{
    public class DateTimeToDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return string.Format("{0}/{1}/{2}",date.Day,date.Month,date.Year);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
