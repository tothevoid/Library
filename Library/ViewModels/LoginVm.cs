using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Library.ViewModels
{
    class LoginVm:VmBase
    {
        private string _login;

        public string Login { get { return _login; } set { if (value != _login) Set(ref _login, value); } }
        
        public ICommand Register { get { return new Command(CallRegistration); } }

        public ICommand OkPress { get { return new Command(Check); } }

        private void Check(object param)
        {
            
            var e = Singleton.CurrentUserType;

            var pass = param as PasswordBox;

            var doc = XDocument.Load("Users.xml");
            
            foreach (XElement elm in doc.Element("Users").Elements())
            {
                if (elm.Attribute("Login").Value==_login && elm.Attribute("Password").Value == pass.Password)
                {               
                    if (Convert.ToInt32(elm.Attribute("IsAdmin").Value) == 0)
                        Singleton.CurrentUserType = UserType.Reader;
                    else
                        Singleton.CurrentUserType = UserType.Admin;
                    Singleton.Name = elm.Attribute("FirstName").Value +" "+ elm.Attribute("LastName").Value;
                    break;
                }
            }

            if (Singleton.CurrentUserType != UserType.NonLoged)
                Singleton.wnd.Close();
            else
                MessageBox.Show("Try again");
        }

        void Connect()
        {

            // строка подключения к БД
            string connStr = "server=localhost;user=librarygovan;database=id1564197;password=rael1122;";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = "SELECT * FROM test";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // выполняем запрос и получаем ответ
            string name = command.ExecuteScalar().ToString();
            // выводим ответ в консоль
            // закрываем соединение с БД
            conn.Close();

        }
       

        private void CallRegistration(object param)
        {
            var loader = new XMLManager();
            var wnd = new Registration();
            wnd.ShowDialog();
            
        }
    }
}
