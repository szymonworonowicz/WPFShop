﻿using System.Windows;
using System.Windows.Controls;
using WpfProject.Helpers;
using WpfProject.Pages.Admin;

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private string to_search = "";
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
            if (WindowContent.Content is SalesProducts sale)
            {
                sale.resetFilter();
            }
            else
            {
                WindowContent.Navigate(new SalesProducts());
            }

        }

        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.Content = new AdminMainPg();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoginService.Role == AppRole.Admin)
            {
                Cart.Visibility = Visibility.Collapsed;
            }
        }

        private void Search_CLick(object sender, RoutedEventArgs e)
        {
            SalesProducts sale = null;
            if (Search_Text.Text == "Wyszukaj przedmiot..." && to_search == "")
            {
                return;
            }
            if (WindowContent.Content is SalesProducts)
            {
                sale = WindowContent.Content as SalesProducts;

            }
            else
            {
                sale = new SalesProducts();
                this.WindowContent.Navigate(sale);

            }
            sale.Filter(to_search);
        }

        private void Search_TextBox_Focus(object sender, RoutedEventArgs e)
        {
            TextBox search = sender as TextBox;
            search.Text = "";
        }

        private void Seach_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox search = sender as TextBox;
            to_search = search.Text;
            search.Text = "Wyszukaj przedmiot...";
        }
    }
}
