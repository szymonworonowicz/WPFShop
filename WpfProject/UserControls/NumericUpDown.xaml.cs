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
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public static DependencyProperty ValueProperty;
        private const int maxValue = 2000;

        //public string StringValue { get; set; }
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetCurrentValue(ValueProperty, value); }
        }
        public NumericUpDown()
        {
            InitializeComponent();
        }
        static NumericUpDown()
        {
            ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(NumericUpDown), new FrameworkPropertyMetadata("",FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        }

        private static bool Validate(object value)
        {
            if (value != null && (string)value != "")
            {
                int.TryParse((string)value, out int current);
                return current < maxValue;
            }
            return false;
        }
        private void CounterTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;

            int.TryParse(text.Text, out int current);
            if (current < 0)
                current = 0;
            else if (current > maxValue)
                current = maxValue;

            Value = current.ToString();
        }

        private void UpClick(object sender, RoutedEventArgs e)
        {
            int.TryParse(Value, out int current);
            current++;
            if (current > maxValue)
                current = maxValue;

            Value = current.ToString();
        }

        private void DownClick(object sender, RoutedEventArgs e)
        {
            int.TryParse(Value, out int current);
            current--;
            if (current < 0)
                current = 0;

            Value = current.ToString();
        }
    }
}

