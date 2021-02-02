using System.Windows;
using System.Windows.Controls;
using WpfProject.Account;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Page
    {
        private readonly User user;

        public PasswordChange()
        {
            InitializeComponent();
            this.user = LoginService.user;
        }


        private void Change_Password_Click(object sender, RoutedEventArgs e)
        {
            if (NewPassword.Password != VerifyNewPassword.Password)
            {
                Valid.Visibility = Visibility.Visible;
                Valid.Content = "Hasła nie są identyczne";
                return;
            }

            if (AccountManager.ChangePassword(OldPassword.Password, NewPassword.Password) == false)
            {
                Valid.Visibility = Visibility.Visible;
                Valid.Content = "Podano nieprawidłowe hasło";
                return;
            }
            else
            {
                MessageBox.Show("Zmieniono haslo", "Zmiana Hasla", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.Navigate(new SalesProducts());
            }
        }
    }
}
