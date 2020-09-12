using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiManutencaoFilmes.Validations {
    public class ApenasNumerosAttribute : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            bool IsAllDigits(string s) => s.All(char.IsDigit);

            if (!IsAllDigits(value.ToString())) {
                return new ValidationResult("O campo deve ser preenchido apenas com números!");
            }            

            return ValidationResult.Success;
        }

        
    }
}
