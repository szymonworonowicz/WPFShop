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

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for SumaryPage.xaml
    /// </summary>
    public partial class SumaryPage : Page
    {
        private Order order;
        public SumaryPage()
        {
            InitializeComponent();
        }
        public SumaryPage(Order order)
        {
            InitializeComponent();
            this.order = order;
            DataContext = order;
            order.UserDataId = order.UserData.Id;
            Order_product_sumary.ItemsSource = order.Ordered;
        }

        private void Confirm_Order(object sender, RoutedEventArgs e)
        {
            order.Status = OrderStatus.Nowe;

            DataContext context = DataContextAccesor.GetDataContext();

            context.Order.Add(order);
            context.SaveChanges();

            MessageBox.Show("Dodano Zamowienie", "Zamowienie alert", MessageBoxButton.OK, MessageBoxImage.Information);

            CartHelper.resetCart();
            this.NavigationService.Navigate(new SalesProducts());
        }
    }
}
