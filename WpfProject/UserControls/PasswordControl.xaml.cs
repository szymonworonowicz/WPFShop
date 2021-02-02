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

namespace WpfProject.UserControls
{
    /// <summary>
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class PasswordControl : UserControl
    {
        public static DependencyProperty PasswordProperty;
        private bool OldPasswordButtonstate = false;
        public string Password
        {
            get
            {
                return (string)GetValue(PasswordProperty);
            }
            set
            {
                SetValue(PasswordProperty, value);
            }
        }
        public PasswordControl()
        {
            InitializeComponent();
        }

        static PasswordControl()
        {
            PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(PasswordControl), new FrameworkPropertyMetadata(String.Empty,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
                , new ValidateValueCallback(Validate));
        }

        private static bool Validate(object value)
        {
            return true; // pozniej dac tu validacje po regexie
        }

        private void OldPasswordButtonClick(object sender, RoutedEventArgs e)
        {
            if (OldPasswordButtonstate == true)
            {
                OldPassword.Visibility = Visibility.Visible;
                OldPassword.Password = VisibleOldPassword.Text;
                VisibleOldPassword.Visibility = Visibility.Collapsed;
                OldPasswordButtonstate = false;
            }
            else
            {
                OldPassword.Visibility = Visibility.Collapsed;
                VisibleOldPassword.Visibility = Visibility.Visible;
                VisibleOldPassword.Text = OldPassword.Password.ToString();
                OldPasswordButtonstate = true;
            }
        }

        private void Password_change(object sender, RoutedEventArgs e)
        {
            Password = OldPassword.Password;
        }

        private void VisiblePassword_Text_Changed(object sender, TextChangedEventArgs e)
        {
            OldPassword.Password = VisibleOldPassword.Text;
        }
    }
}
