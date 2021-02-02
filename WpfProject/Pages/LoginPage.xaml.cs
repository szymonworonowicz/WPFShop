using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfProject.Account;
using WpfProject.Helpers;
using WpfProject.Models;
using WpfProject.Pages.Admin;

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Onclick(object sender, RoutedEventArgs e)
        {
            if (Login.Text != String.Empty && Password.Password != String.Empty)
            {
                User user = new User
                {
                    Name = Login.Text,
                    Password = Password.Password
                };

                AppRole role = await AccountManager.Login(user);

                if (role == AppRole.Admin)
                {
                    MainWindow main = (MainWindow)Application.Current.MainWindow;
                    main.Content = new AdminMainPg();
                }
            }
        }
    }
}
