using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfProject.DAL;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();

        }

        private void OrdersLoaded(object sender, RoutedEventArgs e)
        {
            DataContext context = DataContextAccesor.GetDataContext();

            var user = LoginService.user;

            var orderList = context.Order.Include(x => x.Ordered).Where(x => x.UserDataId == user.UserDataId).ToList();

            OrderList.ItemsSource = orderList;
        }
    }
}
