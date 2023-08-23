using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class IfmaEmail : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string email = value.ToString();
            string pattern = @"^\w+@acad\.ifma\.edu\.br$";

            if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                return new ValidationResult("Email must have the format @acad.ifma.edu.br");
            }
        }

        return ValidationResult.Success;
    }
}
