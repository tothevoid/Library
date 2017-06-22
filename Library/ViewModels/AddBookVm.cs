using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    class AddBookVm:VmBase
    {
        LibraryProjectEntities context = new LibraryProjectEntities();

        string _name;
        string _author;
        string _genre;
        string _publisher;
        int _publishYear;
        int _inStock;
        string _description;
        string _language;
        Uri _img;

        public string Name { get { return _name; } set { Set(ref _name, value); } }
        public string Author { get { return _author; } set { Set(ref _author, value); } }
        public string Genre { get { return _genre; } set { Set(ref _genre, value); } }
        public string Publisher { get { return _publisher; } set { Set(ref _publisher, value); } }
        public int PublishYear { get { return _publishYear; } set { Set(ref _publishYear, value); } }
        public int InStock { get { return _inStock; } set { Set(ref _inStock, value); } }
        public string Description { get { return _description; } set { Set(ref _description, value); } }
        public string Language { get { return _language; } set { Set(ref _language, value); } }
        public Uri Img { get { return _img; } set { Set(ref _img, value); } }

        public ICommand AddBook { get { return new Command(Add); } }
        public ICommand LoadImg { get { return new Command(Load); } }

        void Load(object param)
        { 
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Image";
            dlg.Filter = "Image (.png)|*.png";
            bool? result = dlg.ShowDialog();
            if (result == true)
                Img = new Uri(dlg.FileName);
        }

        bool CheckStrings(params string[] strings)
        {
            foreach (var str in strings)
            {
                if (string.IsNullOrWhiteSpace(str))
                return false;
            }
            return true;
        }

        bool CheckNums(params int[] nums)
        {
            foreach (var num in nums)
            {
                if (num <= 0)
                    return false;
            }
            return true;
        }

        

        void Add(object param)
        {
            if (!CheckStrings(Name, Author, Genre, Publisher, Description) || !CheckNums(PublishYear, InStock))
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка ввода");
                return;
            }

            var book = context.Books.Add(new Books { Author = Author, Description = Description, Genre = Genre, InStock = InStock, Language = Language, Name = Name, Publisher = Publisher, PublishYear = PublishYear});

            context.SaveChanges();
            
            int id = book.BookID;

            var link = Img.AbsolutePath.Split('/').Last().Split('.');

            var dir = Directory.GetCurrentDirectory() + "\\Covers\\" + id + "."+ link[1];

            if (Img != null)
            {
                context.Books.Where(x => x.BookID == id).First().ImgLink = id + "." + link[1];
                File.Copy(Img.AbsolutePath, dir);
                context.SaveChanges();
            }

            Singleton.GetInstance().LastId = id;
            OnAdding(id);
           // Singleton.GetInstance().ElmNum = 1;
            // close this wnd
        }

    }
}
