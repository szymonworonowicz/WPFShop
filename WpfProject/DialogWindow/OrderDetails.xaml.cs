using System.Windows;
using WpfProject.Models;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        public OrderDetails(Order order)
        {
            InitializeComponent();
            this.DataContext = order;
        }
    }
}
