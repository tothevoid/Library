using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Library.Models;
using System.Windows.Input;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;

namespace Library
{
    class MenuVm:VmBase
    { 
        public MenuVm()
        { 
           if (Singleton.GetInstance().CurrentUserType == Enums.UserType.Admin)
                Status = new Uri(Directory.GetCurrentDirectory() + @"\\Icons\\Admin.png");
           UserName = Singleton.GetInstance().Name;
           foreach (var book in context.Book.ToList())
           {
                Books.Add(book);
           }
            Singleton.GetInstance().ElmNum = 1;
        }

        Uri _status = new Uri(Directory.GetCurrentDirectory() + @"\\Icons\\User.png");

        private LibraryProjectEntities context = new LibraryProjectEntities();
        private Book _selectedBook;
        public Book SelectedBook { get { return _selectedBook; } set { Set(ref _selectedBook, value); } }
        int newMark;

        private List<Book> backup = new List<Book>();
        string _userName;
        private double _score;
        private double? _userScore;
        private string _marksCount;
        private int _width;
        private string _searchQuery;
        private int _searchQueryNum;
        private int _selectedSign;


        public delegate void WindowClosed();

        public event WindowClosed LogOut;

        public string[] SearchTypes { get; } = new string[] { "Название", "Автор", "Жанр", "Издатель", "Язык", "Год издания", "Наличие", "Оценка", "Кол-во оценок"};

        public string[] Signs { get; } = new string[] {"<", ">", "="};

        public int SelectedSign { get { return _selectedSign; } set { Set(ref _selectedSign, value); } }

        public Uri Status { get { return _status; } set { Set(ref _status, value); } }

        public string UserName { get { return _userName; } set { Set(ref _userName, value); } }

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        public double? UserScore { get { return _userScore; } set { Set(ref _userScore, value); } }

        public double Score { get { return _score; } set { Set(ref _score, value); } }

        public ICommand LoadData { get { return new Command(Connect); } }

        public ICommand SearchPressed { get { return new Command(Search); } }

        public ICommand Expand { get { return new Command(ToExpand); } }

        public ICommand SelectBook { get { return new Command(LoadInfo); } }

        public ICommand Log { get { return new Command(TriggerEvent); } }

        public ICommand ClearQuery { get { return new Command(Clear); } }

        public string MarksCount { get { return _marksCount; } set { Set(ref _marksCount, value); } }

        public string SearchQuery { get { return _searchQuery; } set { Set(ref _searchQuery, value); } }

        public int SearchQueryNum { get { return _searchQueryNum; } set { Set(ref _searchQueryNum, value); } }
        

        public int Width { get { return _width; } set { Set(ref _width, value); } }

        private void Clear(object pararm)
        {
            Books.Clear();
            foreach (var book in context.Book.ToList())
            {
                Books.Add(book);
            }
            Singleton.GetInstance().ElmNum = 1;

        }

        private void LoadInfo(object param)
        {
           
        }

        private void TriggerEvent(object param)
        {
            if (LogOut!=null)
                LogOut.Invoke();
        }

        private void ToExpand(object param)
        {
            if (Width == 0)
                Width = 300;
            else
                Width = 0;
        }

        private void Search(object param)
        {
            int type = (int)param;

            Books.Clear();
            List<Book> res = new List<Book>();

            switch (param)
            {
                case (0):
                    res = context.Book.Where(x => x.Name.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (1):
                    res = context.Book.Where(x => x.Author.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (2):
                    res = context.Book.Where(x => x.Genre.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (3):
                    res = context.Book.Where(x => x.Publisher.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (4):
                    res = context.Book.Where(x => x.Language.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (5):
                    res = ((IEnumerable<Book>)context.Book).Where(x => Compare(x.PublishYear)).ToList();
                    break;

                case (6):
                    res = ((IEnumerable<Book>)context.Book).Where(x => Compare(x.InStock)).ToList();
                    break;

                case (7):
                    res = ((IEnumerable<Book>)context.Book).Where(x => Compare(x.Marks)).ToList();
                    break;

                case (8):
                    res = ((IEnumerable<Book>)context.Book).Where(x => CompareDouble(x.Score)).ToList();
                    break;

            }

            foreach (var book in res)
            {
                Books.Add(book);
            }
            Singleton.GetInstance().ElmNum = 1;

        }

        private bool CompareDouble(double param)
        {
            // fix double
            bool res = false;
            switch (SelectedSign)
            {
                case (0):
                    if (param < SearchQueryNum)
                        res = true;
                    break;
                case (1):
                    if (param > SearchQueryNum)
                        res = true;
                    break;
                case (2):
                    if (param == SearchQueryNum)
                        res = true;
                    break;

            }
            return res;
        }

        private bool Compare(int book)
        {
            bool res = false;
            switch (SelectedSign)
            {
                case (0):
                    if (book < SearchQueryNum)
                        res = true;
                    break;
                case (1):
                    if (book > SearchQueryNum)
                        res = true;
                    break;
                case (2):
                    if (book == SearchQueryNum)
                        res = true;
                    break;

            }
            return res;
        }

        private void Connect(object param)
        {
           
        }


    }
}
