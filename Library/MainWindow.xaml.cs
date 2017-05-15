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

namespace Library
{
    public partial class MainWindow : Window
    {
      

        public MainWindow()
        {
            var log = new Login();
            Singleton.wnd = log;
            Singleton.wnd.Closed += Wnd_Closed;
            log.ShowDialog();

            InitializeComponent();

            List<User> users = new List<User>();
            var doc = XDocument.Load("Users.xml");
            foreach (var x in doc.Element("Users").Elements())
            {
                users.Add(new User(x.Attribute("FirstName").Value, x.Attribute("LastName").Value, x.Attribute("Login").Value, x.Attribute("Password").Value, Convert.ToInt32(x.Attribute("Phone").Value), Convert.ToInt32(x.Attribute("PassportSeries").Value), Convert.ToInt32(x.Attribute("PassportId").Value)));
            }
            Table.ItemsSource = users;

            if (Singleton.CurrentUserType == Library.UserType.Admin)
                Color.Fill = new SolidColorBrush(Colors.Red);
            UserType.Text = Singleton.Name;

          

        }

        private void Wnd_Closed(object sender, EventArgs e)
        {
            if (Singleton.CurrentUserType == Library.UserType.NonLoged)
                App.Current.Shutdown();
        }

    }
}
