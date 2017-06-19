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
        public Action CloseWindow;

        public LoginVm(Action method)
        {
            CloseWindow = method;
        }

        private int? _login;

        public int? Login { get { return _login; } set { Set(ref _login, value); } }

        public ICommand OkPress { get { return new Command(Check); } }

        private void Check(object param)
        {
            var e = Singleton.GetInstance().CurrentUserType;
            var pass = param as PasswordBox;
            var conn = new DataBaseConnection();
            conn.Authentication((int)Login, pass.Password);

            if (Singleton.GetInstance().CurrentUserType != Enums.UserType.NonLogged)
                CloseWindow.Invoke();
            else
                MessageBox.Show("Try again");
        }

    }
}
