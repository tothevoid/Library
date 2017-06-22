using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.ViewModels
{
    class JournalVm : VmBase
    {
        public JournalVm()
        {
            Records = new ObservableCollection<Records>();
            Load(); 
        }

        private void Load()
        {
            if (Records.Count != 0)
                Records.Clear();
            var context = new LibraryProjectEntities();
            foreach (var record in context.Records.ToList())
            {
                Records.Add(record);
            }
        }

        public ObservableCollection<Records> Records { get; set; }

        public int[] Marks { get; } = new int[] { 1, 2, 3, 4, 5 };
        
        private int _selectedMark = 4;

        public int SelectedMark { get { return _selectedMark; } set { Set(ref _selectedMark, value); } } 

        public ICommand Taken { get { return new Command(SwitchTaken); } }

        public ICommand Returned { get { return new Command(SwitchReturned); } }

        void SwitchTaken(object param)
        {
            int id = (int)param;
            var context = new LibraryProjectEntities();
            var elm = context.Records.Where(x => x.RecordID == id).FirstOrDefault();
            //context.Books.Where(x => x.BookID == elm.BookID).First().InStock--;
            elm.IsAccepted = true;
            context.SaveChanges();
            Load();
        }

        void SwitchReturned(object param)
        {
            int id = (int)param;
            var context = new LibraryProjectEntities();
            var elm = context.Records.Where(x => x.RecordID == id).FirstOrDefault();

           // context.Books.Where(x => x.BookID == elm.BookID).First().InStock++;
            var book = context.Books.Where(x => x.BookID == elm.BookID).FirstOrDefault();
            var score = (book.Score * book.Marks + (SelectedMark+1)) / (book.Marks+1);
            book.Marks++;
            book.Score = Math.Round(score,2);
            elm.IsReturned = true;
            context.SaveChanges();
            Load();
        }

    }
}
