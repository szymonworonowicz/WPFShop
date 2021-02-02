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
        public string Password { get; set; }
        public bool ValidatePassword = false;
        public LoginPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Login_Onclick(object sender, RoutedEventArgs e)
        {
            ErronMessage.Visibility = Visibility.Collapsed;
            if (Login.Text != String.Empty && Password != String.Empty)
            {
                User user = new User
                {
                    Name = Login.Text,
                    Password = this.Password
                };


                try
                {
                    var data = AccountManager.Login(user);
                    LoginService.Login(data);

                    if(data.role == AppRole.NotExist)
                    {
                        ErronMessage.Visibility = Visibility.Visible;
                        ErronMessage.Content = "uzytkownik nie istnieje w bazie";

                    }
                    else if(data.role == AppRole.BadPassword)
                    {
                        ErronMessage.Visibility = Visibility.Visible;
                        ErronMessage.Content = "zle haslo";
                    }
                    else if (data.role == AppRole.Admin)
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
                catch (Exception)
                {

                    ErronMessage.Visibility = Visibility.Visible;
                    ErronMessage.Content = "Niepoprawne dane logowania";
                }               
            } 
            else
            {
                ErronMessage.Visibility = Visibility.Visible;
                ErronMessage.Content = "Wprowadz wszystkie dane";

            }
        }

        private void LoginPageLoaded(object sender, RoutedEventArgs e)
        {
            Login.Focus();
        }
    }
}
