using Microsoft.EntityFrameworkCore;
using sistema_teste_dev_gregpay.Models; 

namespace sistema_teste_dev_gregpay.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Novos DbSets
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Filho> Filhos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define o valor padrão para 'Ativo' do Funcionario
            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValue(true);
                
                // Configura a precisão do Salário
                entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
            });

            // Configura o relacionamento entre Funcionario e Filho
            modelBuilder.Entity<Filho>(entity =>
            {
                entity.HasOne(d => d.Funcionario)
                      .WithMany(p => p.Filhos)
                      .HasForeignKey(d => d.FuncionarioId)
                      .OnDelete(DeleteBehavior.Cascade); // Se deletar o pai, deleta os filhos
            });
        }
    }
}