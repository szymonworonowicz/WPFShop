using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
        private List<Product> products;
        public SalesProducts()
        {
            InitializeComponent();
        }

        private void LoadSaleProducts(object sender, RoutedEventArgs e)
        {
            var context = DataContextAccesor.GetDataContext();
            this.products = context.Products.Include(x => x.Category).ThenInclude(x => x.SubCategory).ToList();

            SalesProduct.ItemsSource = products;
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
        private ListCollectionView View
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(products);
            }
        }
        private void MenuItem_Click(object sender, MouseButtonEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item != null)
            {
                var header = item.Header.ToString();

                View.Filter = x =>
                {
                    Product current = x as Product;

                    if (current != null)
                    {
                        if (current.Category.Name == header == true)
                        {
                            return true;
                        }
                        else if (current.Category.SubCategory.Name == header)
                        {
                            return true;
                        }
                    }

                    return false;
                };
            }
        }

        public void Filter(string filter)
        {
            View.Filter = x =>
            {
                Product current = x as Product;

                if (current != null)
                {
                    if (current.Category.Name.ToLower().Contains(filter.ToLower()))
                    {
                        return true;
                    }
                    else if (current.Category.SubCategory.Name.ToLower().Contains(filter.ToLower()))
                    {
                        return true;
                    }
                    else if (current.Name.ToLower().Contains(filter.ToLower()))
                    {
                        return true;
                    }
                }

                return false;
            };
        }

        public void resetFilter()
        {
            View.Filter = null;
        }
    }
}
