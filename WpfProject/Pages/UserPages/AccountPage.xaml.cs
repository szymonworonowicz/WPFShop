using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private Orders OrdersPage = null;
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

        private void OrdersClick(object sender, RoutedEventArgs e)
        {
            if (OrdersPage == null)
                OrdersPage = new Orders(user);

            AccountContent.Navigate(OrdersPage);
        }
    }
}
