using System.ComponentModel.DataAnnotations;

namespace Livrasso___API.Validations
{
    public class MaxDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.Today;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"A data informada não pode ser maior que a data de hoje.";
        }
    }
}
