using System;
using System.Windows;

namespace Library
{
   
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            log.Focus();
            var act = new Action(Close);
            var instance = new ViewModels.LoginVm(act);
            DataContext = instance;
        }

      
    }
}
