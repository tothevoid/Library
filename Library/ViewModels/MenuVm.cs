using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
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
           foreach (var book in context.Books.ToList())
           {
                Books.Add(book);
           }
            Singleton.GetInstance().ElmNum = 1;
            CanvasChanged += MenuVm_CanvasChanged;
        }

        private void MenuVm_CanvasChanged()
        {
            // TODO
        }

        Uri _status = new Uri(Directory.GetCurrentDirectory() + @"\\Icons\\User.png");

        private LibraryProjectEntities context = new LibraryProjectEntities();
        private Books _selectedBook;
        public Books SelectedBook { get { return _selectedBook; } set { Set(ref _selectedBook, value); } }

        private List<Books> backup = new List<Books>();
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
        private string _bookDescription;

        //TODO: siwtch user

        public event Action CanvasChanged;

        public delegate void WindowClosed();

        public event WindowClosed LogOut;

        public string[] SearchTypes { get; } = new string[] { "Название", "Автор", "Жанр", "Издатель", "Язык", "Год издания", "Наличие", "Оценка", "Кол-во оценок"};

        public string[] Signs { get; } = new string[] {"<", ">", "="};

        public int SelectedSign { get { return _selectedSign; } set { Set(ref _selectedSign, value); } }

        public Uri Status { get { return _status; } set { Set(ref _status, value); } }

        public string UserName { get { return _userName; } set { Set(ref _userName, value); } }

        public ObservableCollection<Books> Books { get; set; } = new ObservableCollection<Books>();

        public double? UserScore { get { return _userScore; } set { Set(ref _userScore, value); } }

        public double Score { get { return _score; } set { Set(ref _score, value); } }

        public ICommand BookSelected { get { return new Command(Select); } }

        public ICommand SearchPressed { get { return new Command(Search); } }

        public ICommand Expand { get { return new Command(ToExpand); } }

        public ICommand Log { get { return new Command(TriggerEvent); } }

        public ICommand ClearQuery { get { return new Command(Clear); } }

        public ICommand Journal { get { return new Command(CallJournal); } }

        public ICommand Register { get { return new Command(CallRegistration); } }

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


        private void CallJournal(object param)
        {
            var wnd = new Journal();
            wnd.Show();
        }

        private void Select(object param)
        {
            int id = (int)param;

            Books obj = context.Books.Where(x => x.BookID==id).First();

            ImgLink = new Uri(Directory.GetCurrentDirectory() + @"\\Covers\\" + obj.ImgLink);
            BookName = obj.Name;
            BookPublisher = obj.Publisher;
            BookPublishYear = obj.PublishYear.ToString();
            BookGenre = obj.Genre;
            BookAuthor = obj.Author;
            BookDescription = obj.Description;
        }

        private void CallRegistration(object param)
        {
            var wnd = new Registration();

            wnd.Show();

        }

        private void Clear(object pararm)
        {
            Books.Clear();
            foreach (var book in context.Books.ToList())
            {
                Books.Add(book);
            }
            Singleton.GetInstance().ElmNum = 1;
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

            foreach (var book in res)
            {
                Books.Add(book);
            }
            Singleton.GetInstance().ElmNum = 1;

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

       


    }
}
