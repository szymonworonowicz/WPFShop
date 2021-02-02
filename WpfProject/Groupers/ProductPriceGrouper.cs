using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace WpfProject.Groupers
{
    class ProductPriceGrouper : IValueConverter
    {
        public int Interval { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = (decimal)value;
            if (price < Interval)
            {
                return String.Format("Mniej niż {0} zł", Interval);
            }
            else
            {
                int interval = (int)price / Interval;
                int lowerLimit = interval * Interval;
                int upperLimit = (interval + 1) * Interval;
                return String.Format("{0} zł – {1} zł", lowerLimit, upperLimit);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
