using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfProject.Helpers;
using WpfProject.Pages.Admin;

namespace WpfProject.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private string to_search = "";
        public MainPage()
        {
            InitializeComponent();

            CommandBinding FindBinding = new CommandBinding();
            FindBinding.Command = ApplicationCommands.Find;
            FindBinding.Executed += FindCommandExecuted;
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.CommandBindings.Add(FindBinding);
        }

        private void FindCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SearchTextBox_Focus();
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            WindowContent.Navigate(new Cart());
        }

        private void Return_To_MainPage(object sender, RoutedEventArgs e)
        {
            if (WindowContent.Content is SalesProducts sale)
            {
                sale.resetFilter();
            }
            else
            {
                WindowContent.Navigate(new SalesProducts());
            }

        }

        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.CommandBindings.Clear();
            mw.Content = new AdminMainPg();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoginService.Role == AppRole.Admin)
            {
                Cart.Visibility = Visibility.Collapsed;
            }
        }

        private void Search_CLick(object sender, RoutedEventArgs e)
        {
            SalesProducts sale = null;
            if (Search_Text.Text == "Wyszukaj przedmiot..." && to_search == "")
            {
                return;
            }
            if (WindowContent.Content is SalesProducts)
            {
                sale = WindowContent.Content as SalesProducts;

            }
            else
            {
                sale = new SalesProducts();
                this.WindowContent.Navigate(sale);

            }
            sale.Filter(to_search);
        }

        private void SearchTextBox_Focus()
        {
            Search_Text.Text = "";
            Search_Text.Focus();

        }
        private void Search_TextBox_Focus(object sender, RoutedEventArgs e)
        {
            SearchTextBox_Focus();
        }

        private void Seach_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox search = sender as TextBox;
            to_search = search.Text;
            search.Text = "Wyszukaj przedmiot...";
        }
    }
}
