using System.Windows.Controls;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
#pragma warning disable CS0414 // The field 'AccountPage.OrdersPage' is assigned but its value is never used
        private Orders OrdersPage = null;
#pragma warning restore CS0414 // The field 'AccountPage.OrdersPage' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'AccountPage.AccountSettings' is assigned but its value is never used
        private AccountSettings AccountSettings = null;
#pragma warning restore CS0414 // The field 'AccountPage.AccountSettings' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'AccountPage.passwordChange' is assigned but its value is never used
        private PasswordChange passwordChange = null;
#pragma warning restore CS0414 // The field 'AccountPage.passwordChange' is assigned but its value is never used
        private User user
        {
            get
            {
                return LoginService.user;
            }
        }
        public AccountPage()
        {
            InitializeComponent();
        }


    }
}
