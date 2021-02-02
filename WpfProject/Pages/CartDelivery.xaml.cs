using Microsoft.EntityFrameworkCore.Internal;
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
using WpfProject.DAL;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for CartDelivery.xaml
    /// </summary>
    public partial class CartDelivery : Page
    {
        public readonly DataContext context;
        public UserData UserData { get; set; }

        public CartDelivery()
        {
            InitializeComponent();
            context = DataContextAccesor.GetDataContext();
        }

        private void CartDelivery_Loaded(object sender, RoutedEventArgs e)
        {
            if(LoginService.user != null)
            {
                UserData = LoginService.user.UserData;
            }
            else
            {
                UserData = new UserData();
                UserData.Adres = new Adres();
            }

            CartDeliveryData.DataContext = UserData;
        }

        private void SumaryPage_Click(object sender, RoutedEventArgs e)
        {
            string delivery = "";
            decimal deliverycost=0;

            if(Deliver.IsChecked==true)
            {
                delivery = "Kurier";
                deliverycost = 13;
            }
            else if(Post.IsChecked ==true)
            {
                delivery = "Poczta";
                deliverycost = 10;
            }
            else if (Packer.IsChecked == true)
            {
                delivery = "Paczkomat";
                deliverycost = 11;
            }
            else if (Self.IsChecked == true)
            {
                delivery = "Odbior osobisty";
                deliverycost = 0;
            }

            var cartamount = CartHelper.getCartSum();
            var amount = cartamount + deliverycost;
            Order order = new Order { Ordered = CartHelper.getCart(),UserData = UserData, OrderAmount =cartamount,Amount = amount,OrderOption  = delivery};

            this.NavigationService.Navigate(new SumaryPage(order,fromcart:true));
        }
    }
}
