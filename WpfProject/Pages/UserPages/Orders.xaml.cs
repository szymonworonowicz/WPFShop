using System;
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
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private readonly User user;
        public Orders(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void OrdersLoaded(object sender, RoutedEventArgs e)
        {
            OrdersList.DataContext = user;
        }
    }
}
