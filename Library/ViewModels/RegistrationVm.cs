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
        private string _password;
        private string _patronymic;
        private string _phone;
        private int? _passportId;
        private int? _passportSeries;

        public string FirstName { get {return _firstName ;} set { Set(ref _firstName,value) ;} }
        public string LastName { get { return _lastName; } set { Set(ref _lastName, value); } }
        public string Patronymic { get { return _patronymic; } set { Set(ref _patronymic, value); } }
        public string Password { get { return _password; } set { Set(ref _password, value); } }
        public string Phone { get { return _phone; } set {  Set(ref _phone, value); } }
        public int? PassportSeries { get { return _passportId; } set {  Set(ref _passportId, value); } }
        public int? PassportId { get { return _passportSeries; } set {  Set(ref _passportSeries, value); } }


        public ICommand OkPress { get {return new Command(OK);} }

        private void OK(object param)
        {
            if (!StringCheck(FirstName,LastName,Password,Patronymic,Phone))
            {
                MessageBox.Show("Неправильный ввод", "Ошибка регистрации");
                return;
            }

            var context = new LibraryProjectEntities();
            var res = context.Users.Add(new Users { FirstName = _firstName, LastName = _lastName, Patronymic = _patronymic, PassportId = (int)_passportId, PassportSeries = (int)_passportSeries, Password = _password, Phone =  _phone });
            context.SaveChanges();
            MessageBox.Show("Уникальный номер пользователя: "+ res.UserID, "Регистрация завершена" );
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
