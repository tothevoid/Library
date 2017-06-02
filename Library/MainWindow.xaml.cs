using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Library.Models;
using System.Globalization;

namespace Library
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var log = new Login();
            log.Closed += Wnd_Closed;
            log.ShowDialog();
            InitializeComponent();
            var menu = new MenuVm();
            DataContext = menu;
          

        }

        private void Wnd_Closed(object sender, EventArgs e)
        {
            if (Singleton.CurrentUserType == Enums.UserType.NonLogged)
                App.Current.Shutdown();
        }
      
    }

    public class ColorToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = Visibility.Hidden;

            if (value == null)
                return visibility;
           
            var clr = value as SolidColorBrush ;
           
            if (clr.Color == Colors.Red)
               visibility = Visibility.Visible;

            return visibility;


        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
