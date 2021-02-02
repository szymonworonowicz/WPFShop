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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PageOnLoaded(object sender, RoutedEventArgs e)
        {
            var context = DataContextAccesor.GetDataContext();
            var categoryList = context.Categories.ToList();

            CategoryList.ItemsSource = categoryList;
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
