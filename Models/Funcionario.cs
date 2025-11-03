using System.Text.Json.Serialization;

namespace sistema_teste_dev_gregpay.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cpf { get; set; }
        public required string Departamento { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }

        // Propriedade de navegação: Um funcionário pode ter muitos filhos
        public required ICollection<Filho> Filhos { get; set; }
    }
}