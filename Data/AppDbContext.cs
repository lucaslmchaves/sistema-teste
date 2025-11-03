using Microsoft.EntityFrameworkCore;
using sistema_teste_dev_gregpay.Models; // Puxa a classe Participante que você criou

namespace sistema_teste_dev_gregpay.Data
{
    public class AppDbContext : DbContext
    {
        // Construtor que passa as opções para o DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define a tabela Participantes no banco de dados
        public DbSet<Participante> Participantes { get; set; }

        // Configurações adicionais do modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participante>(entity =>
            {
                // Define que 'Ativo' tem valor padrão true
                entity.Property(e => e.Ativo).HasDefaultValue(true); 
                
                // Define que 'DataCriacao' tem valor padrão a data atual
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("GETDATE()"); 
            });
        }
    }
}