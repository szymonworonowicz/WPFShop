using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public List<Category> CategoryList { get; set; }
        public MainPage()
        {
            InitializeComponent();

            var context = DataContextAccesor.GetDataContext();
            var list = context.Categories.ToList();
            CategoryList = context.Categories.Include(x => x.SubCategories).Where(x => x.SubCategoryId == null).ToList();
            CategoryMenu.ItemsSource = CategoryList;

        }

        private void PageOnLoaded(object sender, RoutedEventArgs e)
        {

            if (LoginService.Role == AppRole.Admin)
            {
                Cart.Visibility = Visibility.Collapsed;
                Admin.Visibility = Visibility.Visible;
            }
            else
            {
                Cart.Visibility = Visibility.Visible;
                Admin.Visibility = Visibility.Collapsed;
            }
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowContent.Navigate(new Cart());
        }
    }
}
