using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WpfProject.DAL;
using WpfProject.Groupers;
using WpfProject.Helpers;
using WpfProject.Models;
using WpfProject.Profiler;

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
            //var context = DataContextAccesor.GetDataContext();
            //this.products = context.Products.Include(x => x.Category).ThenInclude(x => x.SubCategory).ToList();
            //products = DbAccessorService.getProducts();
            SalesProduct.ItemsSource = DbAccessorService.getProducts();
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
        private ListCollectionView ProductView
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(DbAccessorService.getProducts());
            }
        }

        public object ExpandersOrder { get; private set; }

        private void MenuItem_Click(object sender, MouseButtonEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item != null)
            {
                var header = item.Header.ToString();

                ProductView.Filter = x =>
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
            ProductView.Filter = x =>
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
            ProductView.Filter = null;
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
                        if (p.Price < cena && ((p.Sale != 0) == sale))
                        {
                            return true;
                        }
                    }
                    else if (subcategory == null)
                    {
                        if (p.Price < cena && ((p.Sale != 0) == sale) && p.Category.SubCategory.Name == category.Name)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (p.Price < cena && ((p.Sale != 0) == sale) && p.Category.SubCategory.Name == category.Name && p.Category.Name == subcategory.Name)
                        {
                            return true;
                        }
                    }
                }
                return false;
            };

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
        private void SortRemove_Click(object sender, RoutedEventArgs e)
        {
            ProductView.SortDescriptions.Clear();
            foreach (var element in Sort_Radio.Children)
            {
                RadioButton radio = element as RadioButton;
                if (radio.IsChecked == true)
                    radio.IsChecked = false;
            }
        }

        private void GroupRemove_Click(object sender, RoutedEventArgs e)
        {
            ProductView.GroupDescriptions.Clear();
            foreach (var element in Group_radio.Children)
            {
                RadioButton radio = element as RadioButton;
                if (radio.IsChecked == true)
                    radio.IsChecked = false;
            }
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

        private void Category_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Category_Filter.SelectedIndex != -1)
            {
                Subcategory_Filter.ItemsSource = DbAccessorService.getCategories()[Category_Filter.SelectedIndex].SubCategories;
            }
        }

        private void SalesPRoductsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if(InfoColumn.Width.Value != 250)
            {
                Storyboard storyboard = (Storyboard)FindResource("Begin");
                storyboard.Begin(InfoColumn, true);
            }

        }
    }
}
