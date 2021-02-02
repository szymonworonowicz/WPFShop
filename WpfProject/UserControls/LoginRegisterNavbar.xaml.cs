﻿using System;
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
using WpfProject.Helpers;
using WpfProject.Pages;
using WpfProject.Pages.UserPages;

namespace WpfProject.UserControls
{
    /// <summary>
    /// Interaction logic for LoginRegisterNavbar.xaml
    /// </summary>
    public partial class LoginRegisterNavbar : UserControl
    {
        public LoginRegisterNavbar()
        {
            InitializeComponent();
        }

        private void LoginRegisterNvbarLoaded(object sender, RoutedEventArgs e)
        {
            if(LoginService.Role !=AppRole.None)
            {
                Logout.Visibility = Visibility.Visible;
                Login.Visibility = Visibility.Collapsed;
            }
            else
            {
                Logout.Visibility = Visibility.Collapsed;
                Login.Visibility = Visibility.Visible;
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginService.Logout();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Content = new MainPage();
        }

        private void Zaloguj_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainPage page = (MainPage)win.Content;
            page.WindowContent.Navigate(new LoginPage());
        }

        private void Zarejestruj_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainPage page = (MainPage)win.Content;
            page.WindowContent.Navigate(new RegisterPage());
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainPage page = new MainPage();

            win.Content = page;
            page.WindowContent.Navigate(new AccountPage());
        }
    }
}
