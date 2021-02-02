using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfProject.DAL;
using WpfProject.DialogWindow;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private List<Order> orderlist;
        public Orders()
        {
            InitializeComponent();

        }

        private void OrdersLoaded(object sender, RoutedEventArgs e)
        {
            DataContext context = DataContextAccesor.GetDataContext();

            var user = LoginService.user;

            orderlist = context.Order.Include(x => x.Ordered).Where(x => x.UserDataId == user.UserDataId).ToList();

            OrderList.ItemsSource = orderlist;
        }

        private void Order_Details_Click(object sender, RoutedEventArgs e)
        {
            var order = orderlist[OrderList.SelectedIndex];

            var dlg = new UserOrderDetails(order);

            dlg.ShowDialog();

            OrderList.SelectedIndex = -1;
        }
    }
}
