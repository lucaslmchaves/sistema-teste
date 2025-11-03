using GregPay.WebApp.ValidationAttributes; // Importa o validador
using System.ComponentModel.DataAnnotations;

namespace GregPay.WebApp.Models
{
    public class FilhoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome do Filho")]
        [Required(ErrorMessage = "O nome do filho é obrigatório.")]
        public string? Nome { get; set; }

        [Display(Name = "CPF do Filho")]
        [Required(ErrorMessage = "O CPF do filho é obrigatório.")]
        [CpfValidation] 
        public string? Cpf { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; } = DateTime.Today;
        
        public int FuncionarioId { get; set; }
    }
}

