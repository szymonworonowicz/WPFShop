using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject.UserControls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public static DependencyProperty ValueProperty;
        public static DependencyProperty MinProperty;
        public static DependencyProperty MaxProperty;


        public EventHandler ValueChanged { get; set; }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetCurrentValue(ValueProperty, value); }
        }

        public string minValue
        {
            get => (string)GetValue(MinProperty);
            set => SetCurrentValue(MinProperty, value);
        }

        public string maxValue
        {
            get => (string)GetValue(MaxProperty);
            set => SetCurrentValue(MaxProperty, value);
        }
        public NumericUpDown()
        {
            InitializeComponent();
        }
        static NumericUpDown()
        {
            ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(NumericUpDown), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
            MinProperty = DependencyProperty.Register("minValue", typeof(string), typeof(NumericUpDown), new FrameworkPropertyMetadata(""));
            MaxProperty = DependencyProperty.Register("maxValue", typeof(string), typeof(NumericUpDown), new FrameworkPropertyMetadata(""));
        }

        private void CounterTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;
            int.TryParse(maxValue, out int max);

            int.TryParse(text.Text, out int current);
            if (current < 0)
                current = 0;
            else if (current > max)
                current = max;

            Value = current.ToString();

            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        private void UpClick(object sender, RoutedEventArgs e)
        {
            int.TryParse(Value, out int current);
            int.TryParse(maxValue, out int max);
            current++;
            if (current > max)
                current = max;

            Value = current.ToString();
        }

        private void DownClick(object sender, RoutedEventArgs e)
        {
            int.TryParse(Value, out int current);
            int.TryParse(minValue, out int min);
            current--;
            if (current < min)
                current = min;

            Value = current.ToString();
        }
    }
}

