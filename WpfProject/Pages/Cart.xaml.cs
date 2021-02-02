using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfProject.DAL;
using WpfProject.Helpers;
using WpfProject.Models;
using WpfProject.UserControls;

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

        private void NumericUpDown_Change_Value(object sender, EventArgs e)
        {
            var numeric = sender as NumericUpDown;
            var product = CartGrid.SelectedItem as OrderItem;

            if (product != null)
            {
                int.TryParse(numeric.Value, out int amount);

                product.Count = amount;
            }

            CartSum.Content = $"Suma: {CartHelper.getCartSum()} zł";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var product = CartGrid.SelectedItem as OrderItem;
            Items.Remove(product);

            CartHelper.deleteFromCart(product);
            CartSum.Content = $"Suma: {CartHelper.getCartSum()} zł";
        }
    }
}
