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
    /// Interaction logic for SalesProducts.xaml
    /// </summary>
    public partial class SalesProducts : Page
    {
        public SalesProducts()
        {
            InitializeComponent();
        }

        private void LoadSaleProducts(object sender, RoutedEventArgs e)
        {
            var context = DataContextAccesor.GetDataContext();
            var productList = context.Products.ToList();

            SalesProduct.ItemsSource = productList;
        }

        private void Item_Panel_Collapse(object sender, RoutedEventArgs e)
        {
            SalesProduct.SelectedIndex = -1;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Product p = SalesProduct.SelectedItem as Product;
            CartHelper.AddToCart(p, 1);

            MessageBox.Show("Dodano Do koszyka", "Zamowienie", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
