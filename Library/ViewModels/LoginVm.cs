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
    class LoginVm : VmBase
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
                if (elm.Attribute("Login").Value == _login && elm.Attribute("Password").Value == pass.Password)
                {
                    if (Convert.ToInt32(elm.Attribute("IsAdmin").Value) == 0)
                        Singleton.CurrentUserType = UserType.Reader;
                    else
                        Singleton.CurrentUserType = UserType.Admin;
                    Singleton.Name = elm.Attribute("FirstName").Value + " " + elm.Attribute("LastName").Value;
                    break;
                }
            }

            if (Singleton.CurrentUserType != UserType.NonLoged)
                Singleton.wnd.Close();
            else
                MessageBox.Show("Try again");
        }

        private void CallRegistration(object param)
        {
            var wnd = new Registration();
       
            wnd.ShowDialog();

        }


    }
}
