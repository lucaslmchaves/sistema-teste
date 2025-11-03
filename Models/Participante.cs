namespace sistema_teste_dev_gregpay.Models
{
    public class Participante
    {
        public int Id { get; set; } // Chave primária
        public required string Nome { get; set; }
        public required string CpfCnpj { get; set; }
        public required string Email { get; set; }
        public bool Ativo { get; set; } // Indica se o participante está ativo
        public DateTime DataCriacao { get; set; } // Data de criação do registro
    }
}