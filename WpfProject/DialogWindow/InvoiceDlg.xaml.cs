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

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for InvoiceDlg.xaml
    /// </summary>
    public partial class InvoiceDlg : Window
    {
        private Order order { get; set; }
        public InvoiceDlg()
        {
            InitializeComponent();
        }
        public InvoiceDlg(Order order)
        {
            InitializeComponent();
            this.order = order;
            DataContext = order;
        }
    }
}
