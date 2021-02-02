using System.Windows;
using System.Windows.Controls;
using WpfProject.Helpers;
using WpfProject.Pages;
using WpfProject.Pages.Admin;
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
            if (LoginService.Role != AppRole.None)
            {
                Logout.Visibility = Visibility.Visible;
                Login.Visibility = Visibility.Collapsed;
            }
            else
            {
                Logout.Visibility = Visibility.Collapsed;
                Login.Visibility = Visibility.Visible;
            }

            if (LoginService.Role == AppRole.User)
            {
                Admin_Panel.Visibility = Visibility.Collapsed;
                User_Panel.Visibility = Visibility.Visible;
            }
            else if (LoginService.Role == AppRole.Admin)
            {
                Admin_Panel.Visibility = Visibility.Visible;
                User_Panel.Visibility = Visibility.Collapsed;
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

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainPage page = new MainPage();


            win.Content = page;
            page.WindowContent.Navigate(new Orders());
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainPage page = new MainPage();


            win.Content = page;
            page.WindowContent.Navigate(new AccountSettings());
        }

        private void PasswordChange_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            MainPage page = new MainPage();


            win.Content = page;
            page.WindowContent.Navigate(new PasswordChange());
        }

        private void AdminPanel_CLick(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;

            win.Content = new AdminMainPg();
        }
    }
}
