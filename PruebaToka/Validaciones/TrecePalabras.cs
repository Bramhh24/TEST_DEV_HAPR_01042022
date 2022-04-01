using System.ComponentModel.DataAnnotations;

namespace PruebaToka.Validaciones
{
    public class TrecePalabras: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var palabras = value.ToString();

            if(palabras.Length < 13 || palabras.Length > 13) // Si la palabra es menor o mayor a 13
            {
                return new ValidationResult("Tienen que ser 13 caracteres."); // Manda un error
            }

            return ValidationResult.Success;
        }
    }
}
