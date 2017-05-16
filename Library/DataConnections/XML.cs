using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.IO;

namespace Library.DataConnections
{
    class XML
    {

        public void AddNewUser(string fn,string ln,string log, string pass,int? ph, int? pid, int? ps)
        {
            XDocument doc = XDocument.Load("Users.xml");
            var elm = doc.Element("Users");
            
            XElement user = new XElement("User");

            user.Add(new XAttribute("FirstName", fn));
            user.Add(new XAttribute("LastName", ln));
            user.Add(new XAttribute("Login", log));
            user.Add(new XAttribute("Password", pass));
            user.Add(new XAttribute("Phone", ph));
            user.Add(new XAttribute("PassportSeries", ps));
            user.Add(new XAttribute("PassportId", pid));
            user.Add(new XAttribute("IsAdmin", 0));

            elm.Add(user);
            doc.Save("Users.xml");
        }

        // add new book

        public void CreateNew()
        {
            if (File.Exists("Users.xml"))
            {
                File.Delete("Users.xml");
            }
            XDocument doc = new XDocument();
            doc.Add(new XElement("Users"));
            doc.Save("Users.xml");
        }

       

    }
}
