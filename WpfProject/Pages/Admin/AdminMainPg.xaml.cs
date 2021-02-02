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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfProject.Helpers;

namespace WpfProject.Pages.Admin
{
    /// <summary>
    /// Interaction logic for AdminMainPg.xaml
    /// </summary>
    public partial class AdminMainPg : Page
    {
        public AdminMainPg()
        {
            InitializeComponent();
        }

        private void AdminPageLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void ExpanderGroup_Expand(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;

            var child = Expanders.Children.OfType<Expander>();

            foreach (Expander expander in child)
            {
                if (expander != ex && ex.IsExpanded)
                {
                    expander.Visibility = Visibility.Collapsed;
                }
                else
                {
                    expander.Visibility = Visibility.Visible;
                }
            }
        }

        private void MainPage_CLick(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Content = new MainPage();
        }
    }
}
