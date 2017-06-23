using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public ICommand Recover { get { return new Command(CallRecover); } }

        private void CallRecover(object param)
        {
            var wnd = new AccountRecover();
            wnd.ShowDialog();
        }

        private void Check(object param)
        {
            var e = Singleton.GetInstance().CurrentUserType;
            var pass = param as PasswordBox;

            if (Login<=0 || Login==null || string.IsNullOrWhiteSpace(pass.Password))
            {
                MessageBox.Show("Неправильный ввод","Ошбика входа");
                return; 
            }

            if (!Authentication((int)Login, pass.Password))
            {
                MessageBox.Show("Неправильный ввод", "Ошбика входа");
                return;
            }

            if (Singleton.GetInstance().CurrentUserType != Enums.UserType.NonLogged)
                CloseWindow.Invoke();
            else
                MessageBox.Show("Try again");
        }

        public bool Authentication(int id, string pass)
        {
            bool res = false;


            var search = context.Users.Where(x => x.UserID == id && x.Password == pass).FirstOrDefault();

            if (search != null)
            {
                Singleton.GetInstance().UserId = id;
                Singleton.GetInstance().Name = search.FirstName + " " + search.LastName;
                res = true;
                if (search.IsLibrarian)
                    Singleton.GetInstance().CurrentUserType = Enums.UserType.Admin;
                else
                    Singleton.GetInstance().CurrentUserType = Enums.UserType.Reader;
            }
            return res;

        }

    }
}
