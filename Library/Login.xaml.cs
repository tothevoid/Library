﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

namespace Library
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
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
