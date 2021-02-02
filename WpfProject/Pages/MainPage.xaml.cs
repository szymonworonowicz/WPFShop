﻿using System;
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
        }

        private void Zaloguj_OnClick(object sender, RoutedEventArgs e)
        {
            WindowContent.Navigate(new LoginPage());
        }
    }
}
