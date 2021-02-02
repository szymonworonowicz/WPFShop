using System.Windows;
using System.Windows.Controls;
using WpfProject.Account;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public User user { get; set; }
        public bool ValidatePassword = true;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterPage_Loaded(object sender, RoutedEventArgs e)
        {
            user = new User();
            user.UserData = new UserData();
            user.UserData.Adres = new Adres();
            DataContext = user;
            user.Role = AppRole.User;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            PasswordError.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(user.Name)==false && string.IsNullOrEmpty(user.Password) == false)
            {
                if (firstPassword.Password != repeatPassword.Password)
                {
                    PasswordError.Content = "Hasla musza byc identyczne";
                    PasswordError.Visibility = Visibility.Visible;
                }
                else
                {
                    
                    if (AccountManager.RegisteUser(user) == true)
                    {
                        this.NavigationService.Navigate(new SalesProducts());
                    }
                    else
                    {
                        PasswordError.Content = "Uzytkownik o danym loginie juz istnieje";
                        PasswordError.Visibility = Visibility.Visible;
                    }

                }
            }
            else
            {
                PasswordError.Content = "Prosze podac dane";
                PasswordError.Visibility = Visibility.Visible;
            } 


        }
    }
}
