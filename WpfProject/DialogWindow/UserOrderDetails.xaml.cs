using System.Windows;
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
