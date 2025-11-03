using System.ComponentModel.DataAnnotations;

namespace GregPay.WebApp.ValidationAttributes
{
    // Validador para a regra de negócio do CPF
    public class CpfValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success; // [Required] trata de valores nulos
            }

            var cpf = value.ToString()!.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return new ValidationResult("CPF deve ter 11 dígitos.");

            if (new string(cpf[0], 11) == cpf)
                return new ValidationResult("CPF inválido.");

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCpf;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            if (cpf.EndsWith(digito))
                return ValidationResult.Success;
            else
                return new ValidationResult("CPF inválido.");
        }
    }
}