using System.ComponentModel.DataAnnotations;

namespace GregPay.WebApp.Models
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; } 
        public string? Cpf { get; set; } 
        public string? Departamento { get; set; } 

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Salario { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataNascimento { get; set; }
        
        public bool Ativo { get; set; }
        public List<FilhoViewModel> Filhos { get; set; } = new List<FilhoViewModel>();
    }
}