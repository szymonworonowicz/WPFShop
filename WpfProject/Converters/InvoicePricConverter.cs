using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using WpfProject.Models;

namespace WpfProject.Converters
{
    [ValueConversion(typeof(Product), typeof(decimal))]
    public class InvoicePriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Product item = value as Product;
            if (item.Sale == 0)
                return item.Price;
            return item.SalePrice;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
