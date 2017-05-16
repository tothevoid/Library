using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public string FirstName { get {return _firstName ;} set { if (value!=_firstName) Set(ref _firstName,value) ;} }
        public string LastName { get { return _lastName; } set { if (value != _lastName) Set(ref _lastName, value); } }
        public string Login { get { return _login; } set { if (value != _login) Set(ref _login, value); } }
        public string Password { get { return _password; } set { if (value != _password) Set(ref _password, value); } }
        public int? Phone { get { return _phone; } set { if (value != _phone) Set(ref _phone, value); } }
        public int? PassportSeries { get { return _passportId; } set { if (value != _passportId) Set(ref _passportId, value); } }
        public int? PassportId { get { return _passportSeries; } set { if (value != _passportSeries) Set(ref _passportSeries, value); } }


        public ICommand OkPress { get {return new Command(OK);} }


        //TODO: empty fields

        private void OK(object param)
        {
            var xml = new DataConnections.XML();
            xml.AddNewUser(FirstName, LastName, Login, Password, Phone, PassportSeries, PassportId);
            Logged.Invoke();
        }
    }
}
