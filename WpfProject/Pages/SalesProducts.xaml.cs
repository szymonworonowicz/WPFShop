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
            var productList = context.Products.Where(x => x.Sale == true).Take(6).ToList();

            SalesProduct.ItemsSource = productList;
        }
    }
}
