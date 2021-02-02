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
using System.Windows.Shapes;
using WpfProject.Helpers;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Window
    {
        public OrderEdit()
        {
            InitializeComponent();
            
        }

        private void OrderEdit_Loaded(object sender, RoutedEventArgs e)
        {
            OrderStatus.ItemsSource = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>();
        }
    }
}
