using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Library
{
    abstract class VmBase : INotifyPropertyChanged
    { 
        public delegate void BookAdded(int id);

        public delegate void ScoreUpdated(int id);

        public static event BookAdded BookAddedEvent;

        public static event ScoreUpdated ScoreChanged;

        public event PropertyChangedEventHandler PropertyChanged;


       
        protected static LibraryProjectEntities context = new LibraryProjectEntities();

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propName = null)
        {
            if (field != null && !field.Equals(value) || value != null && !value.Equals(field))
            {
                field = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        protected void OnAdding(int id)
        {
            if (BookAddedEvent!=null)
                BookAddedEvent.Invoke(id);
        }

        protected void OnScoreChanging(int id )
        {
            if (BookAddedEvent != null)
                ScoreChanged.Invoke(id);
        }
    }
}