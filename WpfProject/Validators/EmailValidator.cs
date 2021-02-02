using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfProject.Validators
{
    public class EmailValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = (string)value;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if(email == null)
                return new ValidationResult(true, null);
            else if (regex.IsMatch(email))
                return new ValidationResult(true, null);
            return new ValidationResult(false, "podaj poprawny email");
        }
    }
}
