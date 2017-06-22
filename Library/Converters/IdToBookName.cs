using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Library.Converters
{
    public class IdToBookName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;
            var context = new LibraryProjectEntities();
            return context.Books.Where(x => x.BookID == id).First().Name;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}