using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    class User
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public int PassportSeries { get; set; }
        public int PassportId { get; set; }

        public User(string fn,string ln, string log, string pass,int ph,int ps, int pid)
        {
            FirstName = fn;
            LastName = ln;
            Login = log;
            Password = pass;
            Phone = ph;
            PassportSeries = ps;
            PassportId = pid;
            
        }

    }
}
