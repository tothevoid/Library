using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library.Models
{
     public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublishYear { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Available { get; set; }
        public Uri Link { get; set; }
        public Thickness Margin { get; set; } = new Thickness(0);
        public string Score { get; set; }
        public string Marks { get; set; }

        public Book(string name, string author, string gengre, string lang, string py, string publisher, string available, string link, string score, string marks, Thickness margin)
        {
            Margin = margin;
            Name = name;
            Author = author.ToUpper();
            Genre = Genre;
            PublishYear = Convert.ToInt32(py);
            Publisher = publisher;
            Language = lang;
            Available = Convert.ToInt32(available);
            Link = new Uri(string.Format(@"C:\Users\ivang\Desktop\Library\Library\bin\Debug\{0}", link));
            Score = "Рейтинг: " + score;
            Marks = "Оценок: " + marks;


        }
    }
}
