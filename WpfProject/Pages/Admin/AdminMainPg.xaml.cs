using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using WpfProject.DialogWindow;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages.Admin
{
    /// <summary>
    /// Interaction logic for AdminMainPg.xaml
    /// </summary>
    public partial class AdminMainPg : Page
    {
        private readonly DataContext context;
        public ObservableCollection<Product> products { get; set; }
        public AdminMainPg()
        {
            InitializeComponent();
            context = DataContextAccesor.GetDataContext();
        }

        private void AdminPageLoaded(object sender, RoutedEventArgs e)
        {
            var list = context.Products.Include(x => x.Category).ThenInclude(x => x.SubCategory).ToList();
            products = new ObservableCollection<Product>(list);
            ListofItem.ItemsSource = products;
            StoreList.ItemsSource = products;
            var orders = context.Order.Include(x => x.Ordered).Include(x => x.UserData).ToList();

            ListofItemOrder.ItemsSource = orders;
        }

        private void ExpanderGroup_Expand(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;

            var child = Expanders.Children.OfType<Expander>();

            foreach (Expander expander in child)
            {
                if (expander != ex && ex.IsExpanded)
                {
                    expander.Visibility = Visibility.Collapsed;
                }
                else
                {
                    expander.Visibility = Visibility.Visible;
                }
            }
        }

        private void MainPage_CLick(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Content = new MainPage();
        }

        private void Products_Add_Click(object sender, RoutedEventArgs e)
        {
            ProductAdddlg dialog = new ProductAdddlg();
            dialog.ShowDialog();

            products.Add(dialog.newProduct);
        }

        private void Products_Edit_Click(object sender, RoutedEventArgs e)
        {
            var product = ListofItem.SelectedItem as Product;
            ProductAdddlg dialog = new ProductAdddlg(product);
            dialog.ShowDialog();
        }

        private void ListOfItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;

            if(list.SelectedItem == null)
            {
                Edit_Product.Visibility = Visibility.Hidden;
                Delete_Product.Visibility = Visibility.Hidden;
                Details_Product.Visibility = Visibility.Hidden;
            } 
            else
            {
                Edit_Product.Visibility = Visibility.Visible;
                Delete_Product.Visibility = Visibility.Visible;
                Details_Product.Visibility = Visibility.Visible;
            }
        }

        private void Item_Panel_Collapse(object sender, RoutedEventArgs e)
        {
            InfoColumn.Width=new GridLength(0);
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            InfoColumn.Width = new GridLength(250);
        }

        private void Store_OrderProduct_CLick(object sender, RoutedEventArgs e)
        {
            var product = StoreList.SelectedItem as Product;
            int.TryParse(OrderCount.Value, out int orderCount);

            product.StanMagazynowy += orderCount;
            try
            {
                context.Attach(product).State = EntityState.Modified;
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            Task t = new Task(async () =>
                await context.SaveChangesAsync()
            );

            t.Start();

        }

        private void Order_Edit_Click(object sender, RoutedEventArgs e)
        {
            OrderEdit edit = new OrderEdit();

            edit.ShowDialog();
        }

        private void Order_Details_Click(object sender, RoutedEventArgs e)
        {
            OrderDetails details = new OrderDetails();

            details.ShowDialog();
        }
    }
}
