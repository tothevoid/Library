using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    class RegistrationVm:VmBase
    {
        private string _firstName;
        private string _lastName;
        private string _login;
        private string _password;
        private int? _phone;
        private int? _passportId;
        private int? _passportSeries;

        public delegate void Registraited();

        public event Registraited Logged;

        
        public string FirstName { get {return _firstName ;} set { Set(ref _firstName,value) ;} }
        public string LastName { get { return _lastName; } set { Set(ref _lastName, value); } }
        public string Login { get { return _login; } set {  Set(ref _login, value); } }
        public string Password { get { return _password; } set { Set(ref _password, value); } }
        public int? Phone { get { return _phone; } set {  Set(ref _phone, value); } }
        public int? PassportSeries { get { return _passportId; } set {  Set(ref _passportId, value); } }
        public int? PassportId { get { return _passportSeries; } set {  Set(ref _passportSeries, value); } }


        public ICommand OkPress { get {return new Command(OK);} }


        //TODO: empty fields

        private void OK(object param)
        {
            if (StringCheck(FirstName,LastName,Login,Password))
            {
                MessageBox.Show("Неправильный ввод", "Ошибка регистрации");
                return;
            }
            var xml = new DataConnections.XML();
            xml.AddNewUser(FirstName, LastName, Login, Password, Phone, PassportSeries, PassportId);
            Logged.Invoke();
        }

        private bool StringCheck(params string[] strs)
        {
           foreach (var str in strs)
           {
               if (string.IsNullOrEmpty(str))
               {
                    return false;
               }
           }

           return true;
        }
    }
}
