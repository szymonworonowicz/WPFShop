using System;
using System.Windows;
using System.Windows.Controls;
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
        public SumaryPage(Order order, bool fromcart)
        {
            InitializeComponent();
            this.order = order;
            DataContext = order;
            order.UserDataId = order.UserData.Id;
            Order_product_sumary.ItemsSource = order.Ordered;
            if (fromcart == false)
            {
                Confirm.Visibility = Visibility.Collapsed;
            }
        }

        private void Confirm_Order(object sender, RoutedEventArgs e)
        {
            order.Status = OrderStatus.Nowe;
            order.Date = DateTime.Now;

            DataContext context = DataContextAccesor.GetDataContext();

            foreach(var orderItem in order.Ordered)
            {
                orderItem.Product = null;
            }

            context.Order.Add(order);
            context.SaveChanges();

            MessageBox.Show("Dodano Zamowienie", "Zamowienie alert", MessageBoxButton.OK, MessageBoxImage.Information);

            CartHelper.resetCart();
            this.NavigationService.Navigate(new SalesProducts());
        }
    }
}
