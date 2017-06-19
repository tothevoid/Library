using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    class Singleton
    {
        private static Singleton instance;

        private Singleton()
        { }

        public static Singleton GetInstance()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }

        public string Name { get; set; }

        public Enums.UserType CurrentUserType = Enums.UserType.NonLogged;

        public int ElmNum { get; set; } = 1;
    }

   
}
