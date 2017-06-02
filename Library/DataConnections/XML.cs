using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.IO;
using Library.Models;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library.DataConnections
{
    class XML:IDataManager
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

        public List<User> LoadAllUsers()
        {
            List<User> users = new List<User>();
            var doc = XDocument.Load("Users.xml");
            foreach (var x in doc.Element("Users").Elements())
            {
                users.Add(new User(x.Attribute("FirstName").Value, x.Attribute("LastName").Value, x.Attribute("Login").Value, x.Attribute("Password").Value, Convert.ToInt32(x.Attribute("Phone").Value), Convert.ToInt32(x.Attribute("PassportSeries").Value), Convert.ToInt32(x.Attribute("PassportId").Value)));
            }
            return users;
        }
       
        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            XDocument doc = XDocument.Load("Books.xml");

            int col=0;
            int row=0;

            
            foreach (var user in doc.Element("Books").Elements())
            {
                //150
                //  Thickness margin = new Thickness((150 + 38)*col, (300+20)*row, 0, 0);

                Thickness margin = new Thickness((200+33) * col, (400 + 5) * row, 0, 0);

                var book = new Book(
                      user.Attribute("Name").Value,
                      user.Attribute("Author").Value,
                      user.Attribute("Genre").Value,
                      user.Attribute("Language").Value,
                      user.Attribute("PublishYear").Value,
                      user.Attribute("Publisher").Value,
                      user.Attribute("Available").Value,
                      user.Attribute("ImgLink").Value,
                      user.Attribute("Score").Value,
                      user.Attribute("CountOfMarks").Value,
                      margin
                      );
                books.Add(book);

                if (col == 4)
                {
                    row++;
                    col = 0;
                }
                else
                    col++;
            }

            return books;
        }

        public void CreateBook()
        {
            XDocument doc = new XDocument();
            XElement users = new XElement("Books");
            XElement user = new XElement("Book");
            user.Add(
                new XAttribute("Name", "First"),
                new XAttribute("Author", "Second"),
                new XAttribute("Genre", "Third"),
                new XAttribute("Language", "Fourth"),
                new XAttribute("PublishYear", "1999"),
                new XAttribute("Publisher", "Sixth"),
                new XAttribute("Available", "5"),
                new XAttribute("ImgLink", "img.jpg"),
                new XAttribute("Score", "2.44"),
                new XAttribute("CountOfMarks", "10")
                );
            users.Add(user);
            doc.Add(users);
            doc.Save("Books.xml");
        }

    }
}
