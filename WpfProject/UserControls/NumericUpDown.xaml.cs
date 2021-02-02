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


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public NumericUpDown()
        {
            InitializeComponent();
        }
        static NumericUpDown()
        {
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(0),new ValidateValueCallback(Validate) );
        }

        private static bool Validate(object value)
        {
            int number = (int)value;

            return (!number.Equals(int.MaxValue) & !number.Equals(int.MinValue));
        }
        private void CounterTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;

            int.TryParse(text.Text, out int current);

            Value = current;
        }

        private void UpClick(object sender, RoutedEventArgs e)
        {
            Value++;
            if (Value > maxValue)
                Value = maxValue;
        }

        private void DownClick(object sender, RoutedEventArgs e)
        {
            Value--;
            if (Value < 0)
                Value = 0;
        }
    }
}

