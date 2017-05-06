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
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;

namespace Library
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
      
        void Connect()
        {
            MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder(); ;
            mysqlCSB.Server = "ip адрес сервера";
            mysqlCSB.Database = "имя БД";
            mysqlCSB.UserID = "имя пользователя";
            mysqlCSB.Password = "пароль";
        }
       
        public Login()
        {
            InitializeComponent();
            DataContext = this;
               
        }

        public ICommand Register { get { return new Command(Smth); } }

        void Smth(object param)
        {
            throw new StackOverflowException();
        }
    }
}
