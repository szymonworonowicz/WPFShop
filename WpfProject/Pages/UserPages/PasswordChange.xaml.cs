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
using WpfProject.Models;

namespace WpfProject.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Page
    {
        private readonly User user;

        public PasswordChange()
        {
            InitializeComponent();
        }
        public PasswordChange(User user)
        {
            InitializeComponent();
            this.user = user;
        }
       
    }
}
