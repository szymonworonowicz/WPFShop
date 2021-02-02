using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WpfProject.Validators
{
    public class PriceValidator : ValidationRule
    {
        private decimal min = 0;
        private decimal max = Decimal.MaxValue;
        public decimal Min
        {
            get { return min; }
            set { min = value; }
        }
        public decimal Max
        {
            get { return max; }
            set { max = value; }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal price = 0;
            try
            {
                if (((string)value).Length > 0)
                    price = Decimal.Parse((string)value,
                    NumberStyles.Any, cultureInfo);
            }
            catch
            {
                return new ValidationResult(false,
                "Niewlasciwy format");
            }

            if ((price < Min) || (price > Max))
            {
                return new ValidationResult(false,
                "Cena powinna być w zakresie " + Min + " do " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }

        }
    }
}
