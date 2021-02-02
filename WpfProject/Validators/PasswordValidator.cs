using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfProject.Validators
{
    public class PasswordValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = (string)value;
            if(password == null || password.Length==0)
                return new ValidationResult(true, null);
            if (password.Length < 5)
                return new ValidationResult(false, "Hasło musi zawierać conajmniej 5 znaków");
            return new ValidationResult(true, null);
        }
    }
}
