using GregPay.WebApp.ValidationAttributes; // Importa o validador
using System.ComponentModel.DataAnnotations;

namespace GregPay.WebApp.Models
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [CpfValidation] 
        public string? Cpf { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "O campo Departamento é obrigatório.")]
        public string? Departamento { get; set; }

        [Display(Name = "Salário (R$)")]
        [Required(ErrorMessage = "O campo Salário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O salário deve ser maior que zero.")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Salario { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataNascimento { get; set; }
        
        public bool Ativo { get; set; }
        public List<FilhoViewModel> Filhos { get; set; } = new List<FilhoViewModel>();
    }
}

