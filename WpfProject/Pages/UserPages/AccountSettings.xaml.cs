using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using WpfProject.DAL;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for AccountSettings.xaml
    /// </summary>
    public partial class AccountSettings : Page
    {
        private User user;

        public AccountSettings()
        {
            InitializeComponent();
        }

        private void AccountSettingPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.user = LoginService.user;
            this.DataContext = user.UserData;
        }

        private void Edit_User_Data_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContextAccesor.GetDataContext();

            try
            {
                context.Update(user);
                context.SaveChanges();

                MessageBox.Show("Zmieniono dane", "Zmiana Danych", MessageBoxButton.OK, MessageBoxImage.Error);

                this.NavigationService.Navigate(new SalesProducts());
            }
            catch (DbUpdateConcurrencyException)
            {
                MessageBox.Show("Błąd zmiany danych", "Zmiana Danych", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
