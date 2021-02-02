using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfProject.Validators
{
    public class PhoneNumberValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string phonenumber = (string)value;
            Regex regex = new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");

            if (phonenumber == null)
                return new ValidationResult(true, null);
            else if (regex.IsMatch(phonenumber))
                return new ValidationResult(true, null);
            return new ValidationResult(false, "podaj poprawny numer telefonu");
        }
    }
}
