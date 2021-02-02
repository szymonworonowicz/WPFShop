using System;
using System.Windows;
using System.Windows.Controls;
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

        private void Login_Onclick(object sender, RoutedEventArgs e)
        {
            if (Login.Text != String.Empty && Password.Password != String.Empty)
            {
                User user = new User
                {
                    Name = Login.Text,
                    Password = Password.Password
                };

                var data = AccountManager.Login(user);

                LoginService.Login(data);

                if (data.role == AppRole.Admin)
                {
                    MainWindow main = (MainWindow)Application.Current.MainWindow;
                    main.Content = new AdminMainPg();
                }
                else if (data.role == AppRole.User)
                {
                    MainWindow main = (MainWindow)Application.Current.MainWindow;
                    main.Content = new MainPage();
                }
            }
        }

        private void LoginPageLoaded(object sender, RoutedEventArgs e)
        {
            Login.Focus();
        }
    }
}
