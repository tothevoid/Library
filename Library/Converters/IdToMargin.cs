using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Library.Converters
{
    public class IdToMargin : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = Singleton.GetInstance().ElmNum;

            int col = 0;
            int row = 0;

            if (id <= 5)
            {
                row = 0;
                col = id - 1;
            }
            else
            {
                row = (id - 1 ) /  5;
                col = (id - 1) - row * 5;
           
            }
           
          
            Thickness margin = new Thickness((200 + 33) * col, (400 + 5) * row, 0, 0);

            id = Singleton.GetInstance().ElmNum++;

            return margin;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
