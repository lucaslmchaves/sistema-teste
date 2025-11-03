using System.Text.Json.Serialization;

namespace sistema_teste_dev_gregpay.Models
{
    public class Filho
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        // Chave estrangeira
        public int FuncionarioId { get; set; }
        
        [JsonIgnore] 
        public Funcionario? Funcionario { get; set; } 
    }
}