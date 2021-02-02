using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WpfProject.Validators
{
    public class PhotoValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isnull = (bool)value;
            if (isnull == true)
                return new ValidationResult(false, "zdjęcie nie może być puste");

            return new ValidationResult(true, null);
        }
    }
}
