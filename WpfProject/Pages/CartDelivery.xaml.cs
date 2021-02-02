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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for CartDelivery.xaml
    /// </summary>
    public partial class CartDelivery : Page
    {
        public CartDelivery()
        {
            InitializeComponent();
        }

        private void SumaryPage_Click(object sender, RoutedEventArgs e)
        {
            //TODO przekazanie elementow
            this.NavigationService.Navigate(new SumaryPage());
        }
    }
}
