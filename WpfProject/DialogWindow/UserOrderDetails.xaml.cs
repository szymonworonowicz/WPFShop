using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfProject.Models;
using WpfProject.Pages;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for UserOrderDetails.xaml
    /// </summary>
    public partial class UserOrderDetails : Window
    {
        public UserOrderDetails(Order order)
        {
            InitializeComponent();
            Details_Context.Navigate(new SumaryPage(order, false));
        }
    }
}
