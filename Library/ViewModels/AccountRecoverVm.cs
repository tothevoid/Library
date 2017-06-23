using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    class AccountRecoverVm : VmBase
    {
        private long _series;
        private long _number;

        public long Series { get { return _series; } set { Set(ref _series, value); } }

        public long Number { get { return _number; } set { Set(ref _number, value); } }

        public ICommand Recover {get { return new Command(GetLogPass); } }

        void GetLogPass (object param)
        {
            var res = context.Users.Where(x => x.PassportId == Number && x.PassportSeries == Series).FirstOrDefault();

            if (res==null)
            {
                MessageBox.Show("Данные введены неверно", "Ошибка поиска");
            }
            else
            {
                MessageBox.Show(string.Format("Номер: {0} \n Пароль: {1}",res.UserID, res.Password), "Данные уч. записи");
            }
        }
    }
}
