﻿using System;
using System.Collections.Generic;
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
using WpfProject.Models;

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for SumaryPage.xaml
    /// </summary>
    public partial class SumaryPage : Page
    {
        public SumaryPage()
        {
            InitializeComponent();
        }
        public SumaryPage(Order order)
        {
            InitializeComponent();
            DataContext = order;
            Order_product_sumary.ItemsSource = order.Ordered;
        }
    }
}
