using Microsoft.Win32;
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

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for ProductAdddlg.xaml
    /// </summary>
    public partial class ProductAdddlg : Window
    {
        private string PhotoUrl;
        public ProductAdddlg()
        {
            InitializeComponent();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Multiselect = false;

            if(dialog.ShowDialog() == true)
            {
                PhotoUrl = dialog.FileNames[0];
            }
        }
    }
}
