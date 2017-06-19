using System;
using System.Windows;

namespace Library
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var log = new Login();
            log.Closed += Wnd_Closed;
            log.ShowDialog();
            InitializeComponent();
            MenuVm menu = new MenuVm();
            DataContext = menu;
            menu.LogOut += Menu_LogOut;
        }

        private void Menu_LogOut()
        {
            var wnd = new MainWindow();
            Close();
            wnd.Show();
        }

        private void Wnd_Closed(object sender, EventArgs e)
        {
            if (Singleton.GetInstance().CurrentUserType == Enums.UserType.NonLogged)
                App.Current.Shutdown();
        }

      
    }
    
}
