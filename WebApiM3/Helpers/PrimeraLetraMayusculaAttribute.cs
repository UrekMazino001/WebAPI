using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Importada.
using System.Linq;
using System.Threading.Tasks;

namespace WebApiM3.Helpers
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute // Finaliza en Attribute por regla de conveniencia.
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) //Value es el valor de la propiedad a validar, contexto de validacion.
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())){
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper()) {
                return new ValidationResult("Primera letra debe ir en mayuscula");
            }

            return ValidationResult.Success;
        }
    }
}
