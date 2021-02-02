using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfProject.DAL;
using WpfProject.DialogWindow;
using WpfProject.Groupers;
using WpfProject.Helpers;
using WpfProject.Models;
using WpfProject.Profiler;

namespace WpfProject.Pages.Admin
{
    /// <summary>
    /// Interaction logic for AdminMainPg.xaml
    /// </summary>
    public partial class AdminMainPg : Page
    {
        private readonly DataContext context;
        public ObservableCollection<Product> products { get; set; }
        public List<Category> categories { get; set; }
        public List<Order> orders { get; set; }
        public AdminMainPg()
        {
            InitializeComponent();
            context = DataContextAccesor.GetDataContext();
        }

        private void AdminPageLoaded(object sender, RoutedEventArgs e)
        {

            //var list = context.Products.Include(x => x.Category).ThenInclude(x => x.SubCategory).ToList();
            products = new ObservableCollection<Product>(DbAccessorService.getProducts());
            categories = DbAccessorService.getCategories();


            orders = DbAccessorService.getOrders();
            Category_Filter.ItemsSource = categories;

            Order_State_Filter_Combo.ItemsSource = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>();
            ListofItem.ItemsSource = products;
            StoreList.ItemsSource = products;
            UserList.ItemsSource = DbAccessorService.getUsers();
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

            if (dialog.newProduct.Photo != null)
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

            if (list.SelectedItem == null)
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
            InfoColumn.Width = new GridLength(0);
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


        }

        private void Order_Edit_Click(object sender, RoutedEventArgs e)
        {
            Order order = ListofItemOrder.SelectedItem as Order;
            OrderEdit edit = new OrderEdit(order);

            edit.ShowDialog();
        }

        private void Order_Details_Click(object sender, RoutedEventArgs e)
        {
            Order order = ListofItemOrder.SelectedItem as Order;
            OrderDetails details = new OrderDetails(order);

            details.ShowDialog();
        }

        private void Product_Delete_Click(object sender, RoutedEventArgs e)
        {
            Product p = ListofItem.SelectedItem as Product;
            products.Remove(p);

            Task t = new Task(async () =>
            {
                context.Products.Remove(p);
                await context.SaveChangesAsync();
            });

            t.Start();
            //context.Products.Remove(p);
            //context.SaveChanges();
        }
        private ListCollectionView ProductView
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(products);
            }
        }
        private void Product_Filter_None(object sender, RoutedEventArgs e)
        {
            ProductView.Filter = null;
            Cena_Filter.Clear();
            Sale_Filter.IsChecked = false;
            Subcategory_Filter.SelectedIndex = -1;
            Category_Filter.SelectedIndex = -1;

        }

        private void Product_Filter(object sender, RoutedEventArgs e)
        {
            ProductView.GroupDescriptions.Clear();
            decimal.TryParse(Cena_Filter.Text, out decimal cena);
            bool sale = Sale_Filter.IsChecked.Value;
            Category category = Category_Filter.SelectedItem as Category;
            Category subcategory = Subcategory_Filter.SelectedItem as Category;

            ProductView.Filter = x =>
             {
                 Product p = x as Product;

                 if (p != null)
                 {
                     if (cena == 0)
                     {
                         if (category == null)
                         {
                             if (((p.Sale != 0) == sale))
                             {
                                 return true;
                             }
                         }
                         else if (subcategory == null)
                         {
                             if (((p.Sale != 0) == sale) && p.Category.SubCategory.Name == category.Name)
                             {
                                 return true;
                             }
                         }
                         else
                         {
                             if (((p.Sale != 0) == sale) && p.Category.SubCategory.Name == category.Name && p.Category.Name == subcategory.Name)
                             {
                                 return true;
                             }
                         }
                     }
                     else if (category == null)
                     {
                         if (p.Price < cena  && ((p.Sale != 0) == sale))
                         {
                             return true;
                         }
                     }
                     else if (subcategory == null)
                     {
                         if (p.Price < cena  && ((p.Sale != 0) == sale) && p.Category.SubCategory.Name == category.Name)
                         {
                             return true;
                         }
                     }
                     else
                     {
                         if (p.Price < cena  && ((p.Sale != 0) == sale) && p.Category.SubCategory.Name == category.Name && p.Category.Name == subcategory.Name)
                         {
                             return true;
                         }
                     }
                 }
                 return false;
             };
            
        }

        private void Category_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Category_Filter.SelectedIndex != -1)
            {
                Subcategory_Filter.ItemsSource = categories[Category_Filter.SelectedIndex].SubCategories;
            }
        }

        private void Group_Product(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Name == "Group_Category")
            {
                ProductView.GroupDescriptions.Clear();
                ProductView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            }
            else if (radio.Name == "Group_Price")
            {
                ProductView.Filter = null;
                ProductView.GroupDescriptions.Clear();
                ProductView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));

                ProductPriceGrouper grouper = new ProductPriceGrouper { Interval = 200 };
                ProductView.GroupDescriptions.Add(new PropertyGroupDescription("Price", grouper));
            }
        }

        private void Sort_Price_Asc(object sender, RoutedEventArgs e)
        {
            ProductView.SortDescriptions.Clear();
            ProductView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
        }

        private void Sort_Price_Dsc(object sender, RoutedEventArgs e)
        {
            ProductView.SortDescriptions.Clear();
            ProductView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
        }

        private void Sort_Date(object sender, RoutedEventArgs e)
        {
            ProductView.SortDescriptions.Clear();
            ProductView.SortDescriptions.Add(new SortDescription("AddedDate", ListSortDirection.Ascending));
        }

        private void Sort_Name_Asc(object sender, RoutedEventArgs e)
        {
            ProductView.SortDescriptions.Clear();
            ProductView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void Sort_Name_Dsc(object sender, RoutedEventArgs e)
        {
            ProductView.SortDescriptions.Clear();
            ProductView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
        }
        private void OrderExpander_Expand(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;

            var child = ExpandersOrder.Children.OfType<Expander>();

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

        private ListCollectionView OrderView
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(orders);
            }
        }

        private void Order_Filter(object sender, RoutedEventArgs e)
        {
            if (Order_State_Filter_Combo.SelectedIndex != -1)
            {
                OrderStatus status = (OrderStatus)Order_State_Filter_Combo.SelectedItem;
                OrderView.Filter = x =>
                {
                    Order order = x as Order;
                    if (order.Status == status)
                    {
                        return true;
                    }
                    return false;
                };
            }
        }

        private void Filter_None(object sender, RoutedEventArgs e)
        {
            OrderView.Filter = null;
        }

        private void Order_Status_Group(object sender, RoutedEventArgs e)
        {
            OrderView.GroupDescriptions.Clear();
            OrderView.GroupDescriptions.Add(new PropertyGroupDescription("Status"));
        }

        private void Order_Price_Group(object sender, RoutedEventArgs e)
        {
            OrderView.Filter = null;
            OrderView.GroupDescriptions.Clear();
            OrderView.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));

            ProductPriceGrouper grouper = new ProductPriceGrouper { Interval = 2000 };
            OrderView.GroupDescriptions.Add(new PropertyGroupDescription("Amount", grouper));

        }

        private void Sort_Order_Price_Asc(object sender, RoutedEventArgs e)
        {
            OrderView.SortDescriptions.Clear();
            OrderView.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Ascending));
        }

        private void Sort_Order_Price_Desc(object sender, RoutedEventArgs e)
        {
            OrderView.SortDescriptions.Clear();
            OrderView.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
        }

        private void Sort_Order_Date_Asc(object sender, RoutedEventArgs e)
        {
            OrderView.SortDescriptions.Clear();
            OrderView.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
        }

        private void Sort_Order_Date_Desc(object sender, RoutedEventArgs e)
        {
            OrderView.SortDescriptions.Clear();
            OrderView.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
        }
    }
}
