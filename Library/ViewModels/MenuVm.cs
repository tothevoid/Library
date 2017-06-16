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

namespace Library
{
    class MenuVm:VmBase
    { 
        public MenuVm()
        {
            if (Singleton.CurrentUserType == Enums.UserType.Admin)
                 Status = new Uri(@"C:\Users\ivang\Desktop\Library\Library\admin.png");
            //  Color = new SolidColorBrush(Colors.Red);
            UserName = Singleton.Name;

            var doc = new DataConnections.XML();
            Books = new ObservableCollection<Book>(doc.GetBooks());
            backup = doc.GetBooks();
        }

        Uri _status = new Uri(@"C:\Users\ivang\Desktop\Library\Library\user.png");


        private Book _selectedBook;
        public Book SelectedBook { get { return _selectedBook; } set { Set(ref _selectedBook, value); } }
        int newMark;

        private List<Book> backup = new List<Book>();
        string _userName;
        private double _score;
        private double? _userScore;
        private string _marksCount;
        private int _width;
        private string _searchQuery = "...";

        public delegate void WindowClosed();

        public event WindowClosed LogOut;



        public Uri Status { get { return _status; } set { Set(ref _status, value); } }

        public string UserName { get { return _userName; } set { Set(ref _userName, value); } }

        public ObservableCollection<Book> Books { get; set; }

        public double? UserScore { get { return _userScore; } set { Set(ref _userScore, value); } }

        public double Score { get { return _score; } set { Set(ref _score, value); } }

        public ICommand LoadData { get { return new Command(Connect); } }

        public ICommand SearchPressed { get { return new Command(Search); } }

        public ICommand Expand { get { return new Command(ToExpand); } }

        public ICommand SelectBook { get { return new Command(LoadInfo); } }

        public ICommand Log { get { return new Command(TriggerEvent); } }

        public string MarksCount { get { return _marksCount; } set { Set(ref _marksCount, value); } }

        public string SearchQuery { get { return _searchQuery; } set { Set(ref _searchQuery, value); } }

        public int Width { get { return _width; } set { Set(ref _width, value); } }

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
            var sorted = backup.Where(x => x.Name.ToLower().Contains(SearchQuery.ToLower())).ToList();
            Books.Clear();

            int col = 0;
            int row = 0;

            foreach (var x in sorted)
            { 
                Thickness margin = new Thickness((200 + 33) * col, (400 + 5) * row, 0, 0);
                x.Margin = margin;
                if (col == 4)
                {
                        row++;
                        col = 0;
                }
                else
                    col++;
                
            }

            foreach (var x in sorted)
            {
                Books.Add(x);
            }


        }

        private void Connect(object param)
        {
            IDataManager connection = new DataConnections.XML();
           // books = connection.GetBooks();
          //  SelectedBook = books[0];
           // UserScore = books[0].Score;
           // MarksCount = string.Format("Based on: {0} reviews", books[0].Marks);
        }


    }
}
