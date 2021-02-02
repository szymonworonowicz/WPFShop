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
using System.Windows.Shapes;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Window
    {
        private readonly Order order;
        public OrderEdit(Order Order)
        {
            InitializeComponent();
            this.order = Order;
        }

        private void OrderEdit_Loaded(object sender, RoutedEventArgs e)
        {
            OrderStatus.ItemsSource = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>();
            DataContext = order;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
