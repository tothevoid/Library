using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace Library.Converters
{
    public class PathToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string file = (string)value;

            var path = Directory.GetCurrentDirectory();

            if (string.IsNullOrWhiteSpace(file))
            {
                return new Uri(path + @"\\Covers\\0.png");
            }

            return new Uri(path + @"\\Covers\\" + file);

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

