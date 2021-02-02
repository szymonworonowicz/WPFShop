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
using WpfProject.Pages.Admin;

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

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowContent.Navigate(new Cart());
        }

        private void Return_To_MainPage(object sender, RoutedEventArgs e)
        {
            WindowContent.Navigate(new SalesProducts());
        }

        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw =(MainWindow)Application.Current.MainWindow;
            mw.Content = new AdminMainPg();
        }

        private void MainPAge_Loaded(object sender, RoutedEventArgs e)
        {
            if(LoginService.Role == AppRole.Admin)
            {
                Cart.Visibility = Visibility.Collapsed;
            }
        }
    }
}
