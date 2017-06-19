using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Library.Converters
{
    public class IdToLastName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;
            var context = new LibraryProjectEntities();
            var user = context.Users.Where(x=>x.UserID==id).First();
            return string.Format("{0} {1}. {2}.", user.LastName, user.FirstName.ToCharArray().First(), user.Patronymic.ToCharArray().First());
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

