using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    class JournalVm : VmBase
    {
        public JournalVm()
        {
            var context = new LibraryProjectEntities();
            Records = new ObservableCollection<Library.Records>();
            foreach (var record in context.Records.ToList())
            {
                Records.Add(record);
            }
        }

        public ObservableCollection<Records> Records { get; set; }


    }
}
