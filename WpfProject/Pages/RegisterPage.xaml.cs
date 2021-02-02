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
            bool canregister = AccountManager.RegisteUser(user);
            this.NavigationService.Navigate(new SalesProducts());

        }
    }
}
