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
        private SqlConnection Connection;

        public void AddNewUser(string fn, string ln, string pr, string pass, int pid, int ps, long ph)
        {
            using (Connection)
            {
                string query = string.Format("INSERT INTO [Users] (FirstName, LastName, Patronymic, Password, PassportSeries, PassportId, Phone) VALUES ({0},{1},{2},{3},{4},{5},{6})", fn, ln, pr, pass, pid, ps, ph);
                SqlCommand comm = new SqlCommand(query, Connection);
                comm.ExecuteNonQuery();
            }
        }

        public void AddBook(string name, string link, string auth, int py, string genre, string publisher, string lang, int stock)
        {
            
        }

        public void Connect()
        {
            string sqlConnectionString = @"Data Source=DESKTOP-F129I9K\SQLEXPRESS; Database=LibraryProject; Integrated Security=True";
            Connection = new SqlConnection(sqlConnectionString); 
        }

        public void AddInJournal(int uid, int bid, string rdate)
        {
            using (Connection)
            {
                string query = string.Format("INSERT INTO [Journal] (UserID, BookID, ReturnDate) VALUES ({0},{1},{2})", uid,bid,rdate);
                SqlCommand comm = new SqlCommand(query, Connection);
                comm.ExecuteNonQuery();
            }
        }
        
        public bool Authentication(int id,string pass)
        {
            bool res = false;

            LibraryProjectEntities context = new LibraryProjectEntities();

            var search  = context.Users.Where(x => x.UserID == id && x.Password == pass).FirstOrDefault();

            if (search != null)
            {
                Singleton.GetInstance().Name = search.FirstName + " " + search.LastName;
                res = true;
                if (search.IsAdmin)
                    Singleton.GetInstance().CurrentUserType = Enums.UserType.Admin;
                else
                    Singleton.GetInstance().CurrentUserType = Enums.UserType.Reader;
            }
            return res;
           
        }

    }
}
