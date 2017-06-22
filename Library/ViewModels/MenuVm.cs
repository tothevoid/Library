using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Library
{
    class MenuVm:VmBase
    { 
        public MenuVm()
        { 
            CanvasChanged += MenuVm_CanvasChanged;
            BookAddedEvent += MenuVm_BookAddedEvent;
            if (Singleton.GetInstance().CurrentUserType == Enums.UserType.Admin)
            {
                Status = new Uri(Directory.GetCurrentDirectory() + @"\\Icons\\Admin.png");
                MenuVisibility = Visibility.Visible;
            }
               
            UserName = Singleton.GetInstance().Name;
            foreach (var book in context.Books.ToList())
            {
                Books.Add(book);
            }
            Singleton.GetInstance().ElmNum = 1;
            CanvasChanged.Invoke();
        }

        #region Fields

        Uri _status = new Uri(Directory.GetCurrentDirectory() + @"\\Icons\\User.png");
        private LibraryProjectEntities context = new LibraryProjectEntities();
        string _userName;
        private double _score;
        private double? _userScore;
        private string _marksCount;
        private int _width;
        private string _searchQuery;
        private int _selectedSign;
        private Uri _imgLink;
        private string _bookName;
        private string _bookGenre;
        private string _bookAuthor;
        private string _bookPublishYear;
        private string _bookPublisher;
        private string _bookLanguage;
        private string _bookDescription;
        private int _booksInStock;
        private int _viewBoxHeight = 820;
        private Visibility _menuVisibility = Visibility.Collapsed;
        private bool _isEnable;
        private int _currentBookId;
        
        #endregion

        #region Events 
        public event Action CanvasChanged;
            public delegate void WindowClosed();
            public event WindowClosed LogOut;
        #endregion

        #region Commands
            public ICommand BookSelected { get { return new Command(Select); } }
            public ICommand SearchPressed { get { return new Command(Search); } }
            public ICommand Expand { get { return new Command(ToExpand); } }
            public ICommand Log { get { return new Command(TriggerEvent); } }
            public ICommand Journal { get { return new Command(CallJournal); } }
            public ICommand Register { get { return new Command(CallRegistration); } }
            public ICommand AddBook { get { return new Command(CallAddBook); } }
            public ICommand TakeBook { get { return new Command(BookTaken); } }
            public ICommand ClearQuery { get { return new Command(Clear); } }
        #endregion

        #region Properties



        public Visibility MenuVisibility { get { return _menuVisibility; } set { Set(ref _menuVisibility, value); } }

        public bool IsEnable { get { return _isEnable; } set { Set(ref _isEnable, value); } }

        public string BookLanguage { get { return _bookLanguage; } set { Set(ref _bookLanguage, value); } }
        
        public int ViewBoxHeight { get { return _viewBoxHeight; } set { Set(ref _viewBoxHeight, value); } }

        public int BooksInStock { get { return _booksInStock; } set { Set(ref _booksInStock, value); } }
        
        public string[] SearchTypes { get; } = new string[] { "Название", "Автор", "Жанр", "Издатель", "Язык", "Год издания", "Наличие", "Оценка", "Кол-во оценок"};

        public string[] Signs { get; } = new string[] {"<", ">", "="};

        public int SelectedSign { get { return _selectedSign; } set { Set(ref _selectedSign, value); } }

        public Uri Status { get { return _status; } set { Set(ref _status, value); } }

        public string UserName { get { return _userName; } set { Set(ref _userName, value); } }

        public ObservableCollection<Books> Books { get; set; } = new ObservableCollection<Books>();

        public double? UserScore { get { return _userScore; } set { Set(ref _userScore, value); } }

        public double Score { get { return _score; } set { Set(ref _score, value); } }

        public string MarksCount { get { return _marksCount; } set { Set(ref _marksCount, value); } }

        public string SearchQuery { get { return _searchQuery; } set { Set(ref _searchQuery, value); } }

        public int Width { get { return _width; } set { Set(ref _width, value); } }

        public Uri ImgLink { get { return _imgLink; } set { Set(ref _imgLink, value); } }

        public string BookName { get { return _bookName; } set { Set(ref _bookName, value); } }

        public string BookPublishYear { get { return _bookPublishYear; } set { Set(ref _bookPublishYear, value); } }

        public string BookGenre { get { return _bookGenre; } set { Set(ref _bookGenre, value); } }

        public string BookAuthor { get { return _bookAuthor; } set { Set(ref _bookAuthor, value); } }

        public string BookPublisher { get { return _bookPublisher; } set { Set(ref _bookPublisher, value); } }

        public string BookDescription { get { return _bookDescription; } set { Set(ref _bookDescription, value); } }
        #endregion

        #region Methods

        private void BookTaken(object param)
        {
            DateTime date = DateTime.Today;
            context.Records.Add(new Records { BookID = _currentBookId, Date=date, ReturnDate = date.AddMonths(1), UserID  = Singleton.GetInstance().UserId });
            context.SaveChanges();
        }

        public void MenuVm_BookAddedEvent(int id)
        {
            Singleton.GetInstance().ElmNum = Books.Count + 1;
            Books.Add(context.Books.Where(x => x.BookID == id).First());
            if ((Books.Count - 1) % 5 == 0)
                CanvasChanged.Invoke();
        }

        private void MenuVm_CanvasChanged()
        {
            int row = Books.Count / 5;
            if (Books.Count % 5 != 0)
                row++;
            if (row > 2)
                ViewBoxHeight = row * 410;
        }

        private void CallAddBook(object param)
        {
            var wnd = new AddBook();
            wnd.Show();
        }
            
        private void CallJournal(object param)
        {
            var wnd = new Journal();
            wnd.Show();
        }

        private void Select(object param)
        {
            int id = (int)param;
            _currentBookId = id;

            
            Books obj = context.Books.Where(x => x.BookID==id).First();

            ImgLink = new Uri(Directory.GetCurrentDirectory() + @"\\Covers\\" + obj.ImgLink);
            BookName = obj.Name;
            BookPublisher = obj.Publisher;
            BookPublishYear = obj.PublishYear.ToString();
            BookGenre = obj.Genre;
            BookAuthor = obj.Author;
            BookDescription = obj.Description;
            BookLanguage = obj.Language;
            BooksInStock = obj.InStock;
            if (Width==0)
                ToExpand(null);
        }

        private void CallRegistration(object param)
        {
            var wnd = new Registration();
            wnd.Show();
        }

        private void Clear(object param)
        {
            SearchQuery = null;
            Books.Clear();
            Singleton.GetInstance().ElmNum = 1;
            foreach (var book in context.Books.ToList())
            {
                Books.Add(book);
            }
            CanvasChanged.Invoke();
            
        }

        private void TriggerEvent(object param)
        {
            if (LogOut!=null)
                LogOut.Invoke();
        }

        private void ToExpand(object param)
        {
            if (Width == 0)
                Width = 400;
            else
                Width = 0;
        }

        private void Search(object param)
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Books.Clear();
                Singleton.GetInstance().ElmNum = 1;
                foreach (var book in context.Books.ToList())
                {
                    Books.Add(book);
                }
               
                CanvasChanged.Invoke();
                return;
            }

            int type = (int)param;

            Books.Clear();
            List<Books> res = new List<Books>();

            switch (param)
            {
                case (0):
                    res = context.Books.Where(x => x.Name.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (1):
                    res = context.Books.Where(x => x.Author.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (2):
                    res = context.Books.Where(x => x.Genre.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (3):
                    res = context.Books.Where(x => x.Publisher.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (4):
                    res = context.Books.Where(x => x.Language.ToLower().Contains(SearchQuery.ToLower())).ToList();
                    break;

                case (5):
                    res = ((IEnumerable<Books>)context.Books).Where(x => Compare(x.PublishYear)).ToList();
                    break;

                case (6):
                    res = ((IEnumerable<Books>)context.Books).Where(x => Compare(x.InStock)).ToList();
                    break;

                case (7):
                    res = ((IEnumerable<Books>)context.Books).Where(x => CompareDouble(x.Score)).ToList();
                    break;

                case (8):
                    res = ((IEnumerable<Books>)context.Books).Where(x => Compare(x.Marks)).ToList();
                    break;

            }
            Singleton.GetInstance().ElmNum = 1;
            foreach (var book in res)
            {
                Books.Add(book);
            }
            CanvasChanged.Invoke();
           
        }

        private bool CompareDouble(double param)
        {  
            bool res = false;
            double num = 0;

            if (!double.TryParse(SearchQuery, out num))
                return res;

            switch (SelectedSign)
            {
                case (0):
                    if (param < num)
                        res = true;
                    break;
                case (1):
                    if (param > num)
                        res = true;
                    break;
                case (2):
                    if (param == num)
                        res = true;
                    break;

            }
            return res;
        }

        private bool Compare(int book)
        {
            bool res = false;
            int num = 0;

            if (!int.TryParse(SearchQuery, out num))
                return res;
            switch (SelectedSign)
            {
                case (0):
                    if (book < num)
                        res = true;
                    break;
                case (1):
                    if (book > num)
                        res = true;
                    break;
                case (2):
                    if (book == num)
                        res = true;
                    break;

            }
            return res;
        }
        #endregion
    }
}
