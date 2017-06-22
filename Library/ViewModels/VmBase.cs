using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Library
{
    abstract class VmBase : INotifyPropertyChanged
    { 
        public delegate void BookAdded(int id);

        public static event BookAdded BookAddedEvent;
        
        public event PropertyChangedEventHandler PropertyChanged;
      
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
    }
}