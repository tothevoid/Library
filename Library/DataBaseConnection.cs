using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.IO;
using System.Data.SqlClient;

namespace Library
{
    public class DataBaseConnection 
    { 
        public bool Authentication(int id,string pass)
        {
            bool res = false;

            LibraryProjectEntities context = new LibraryProjectEntities();

            var search  = context.Users.Where(x => x.UserID == id && x.Password == pass).FirstOrDefault();

            if (search != null)
            {
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
