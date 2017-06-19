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
        public delegate void Registraited();
        public event Registraited Logged;

        private string _firstName;
        private string _lastName;
        private string _login;
        private string _password;
        private string _patronymic;
        private long? _phone;
        private int? _passportId;
        private int? _passportSeries;

        public string FirstName { get {return _firstName ;} set { Set(ref _firstName,value) ;} }
        public string LastName { get { return _lastName; } set { Set(ref _lastName, value); } }
        public string Patronymic { get { return _patronymic; } set { Set(ref _patronymic, value); } }
        public string Login { get { return _login; } set {  Set(ref _login, value); } }
        public string Password { get { return _password; } set { Set(ref _password, value); } }
        public long? Phone { get { return _phone; } set {  Set(ref _phone, value); } }
        public int? PassportSeries { get { return _passportId; } set {  Set(ref _passportId, value); } }
        public int? PassportId { get { return _passportSeries; } set {  Set(ref _passportSeries, value); } }


        public ICommand OkPress { get {return new Command(OK);} }

        private void OK(object param)
        {
            if (StringCheck(FirstName,LastName,Login,Password,Patronymic))
            {
                MessageBox.Show("Неправильный ввод", "Ошибка регистрации");
                return;
            }
            var conn = new DataBaseConnection();
            conn.Connect();
            conn.AddNewUser(_firstName, _lastName, _patronymic, _password, (int)_passportId,(int) _passportSeries, (long)_phone);
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
