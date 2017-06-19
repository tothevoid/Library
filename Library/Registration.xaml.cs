using System.Windows;

namespace Library
{
   
    public partial class Registration : Window
    {
        

        public Registration()
        {
            InitializeComponent();

            var registration = new ViewModels.RegistrationVm();
            registration.Logged += Close;
            DataContext = registration;
        }

       
    }
}
