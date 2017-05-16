using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    //Singleton
    class Singleton
    {

        //TODO: fix it 

        public static Window wnd;

        private static Singleton instance;

        private Singleton()
        { }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }

        public static string Name { get; set; }

        public static UserType CurrentUserType = UserType.NonLoged;
    }

    public enum UserType
    {
        NonLoged,
        Reader,
        Admin
    }
}
