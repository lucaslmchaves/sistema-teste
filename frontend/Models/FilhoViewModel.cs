namespace GregPay.WebApp.Models
{
    // Esta classe é usada para receber os dados dos filhos
    public class FilhoViewModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int FuncionarioId { get; set; }
    }
}
