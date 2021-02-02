using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        public ObservableCollection<OrderItem> Items;
        public Cart()
        {
            InitializeComponent();
            var context = DataContextAccesor.GetDataContext();
            var products = context.Products.ToList();
            var list = CartHelper.getCart();
            if (list == null)
                Items = new ObservableCollection<OrderItem>();
            else
                Items = new ObservableCollection<OrderItem>(list);

            CartGrid.ItemsSource = Items;
        }

        private void DeliveryClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CartDelivery());
        }
    }
}
