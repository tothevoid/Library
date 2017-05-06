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
            InitializeComponent();
            var log = new Login();
            log.ShowDialog();

            XDocument doc = new XDocument();
            var a = new XElement("Users");
            var b = new XElement("User");
            b.Add(new XAttribute("IsAdmin", true));
            a.Add(b);
            doc.Add(a);
            doc.Save("users.xml");
            
           
        }
    }
}
