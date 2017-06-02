using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Library.Models;
using System.Windows.Input;

namespace Library
{
    class MenuVm:VmBase
    {

        private Book _selectedBook;
        public Book SelectedBook { get { return _selectedBook; } set { Set(ref _selectedBook, value); } }
        int newMark;

        List<Book> books = new List<Book>();

        public MenuVm()
        {
            if (Singleton.CurrentUserType == Enums.UserType.Admin)
                Color = new SolidColorBrush(Colors.Red);
            UserName = Singleton.Name;

            var doc = new DataConnections.XML();
            Books = doc.GetBooks();

        }

        SolidColorBrush _color = new  SolidColorBrush(Colors.Blue);
        string _userName;
        private double _score;
        private double? _userScore;
        private string _marksCount;
        private int _width;
        private string _searchQuery = "...";

        public SolidColorBrush Color { get { return _color; }  set { Set(ref _color, value); } }

        public string UserName { get { return _userName; } set { Set(ref _userName, value); } }

        public List<Book> Books { get; set; } = new List<Book>();

        public double? UserScore { get { return _userScore; } set { Set(ref _userScore, value); } }

        public double Score { get { return _score; } set { Set(ref _score, value); } }

        public ICommand LoadData { get { return new Command(Connect); } }

        public ICommand SearchPressed { get { return new Command(Search); } }

        public ICommand Expand { get { return new Command(ToExpand); } }

        public string MarksCount { get { return _marksCount; } set { Set(ref _marksCount, value); } }

        public string SearchQuery { get { return _searchQuery; } set { Set(ref _searchQuery, value); } }

        public int Width { get { return _width; } set { Set(ref _width, value); } }

        private void ToExpand(object param)
        {
            if (Width == 0)
                Width = 300;
            else
                Width = 0;
        }

        private void Search(object param)
        {

            Books = Books.Where(x => x.Name.Contains(SearchQuery)).ToList();
        }

        private void Connect(object param)
        {
            IDataManager connection = new DataConnections.XML();
            books = connection.GetBooks();
            SelectedBook = books[0];
            UserScore = books[0].Score;
            MarksCount = string.Format("Based on: {0} reviews", books[0].Marks);
        }


    }
}
