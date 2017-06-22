using System;
using System.Globalization;
using System.Windows.Data;
using System.Linq;

namespace Library.Converters
{
    public class BoolToVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;

            if (id == 0)
                return false;

            var context = new LibraryProjectEntities();

            var res = context.Records.Where(x => x.RecordID == id).FirstOrDefault();
            if (!res.IsAccepted)
                return true;
            return false;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

