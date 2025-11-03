// Cole este código completo no novo arquivo.
// Ele é usado APENAS pela ferramenta 'dotnet ef'

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace sistema_teste_dev_gregpay.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 1. Configura para ler o appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 2. Pega a string de conexão
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 3. Cria as opções do DbContext manualmente
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // 4. Retorna o DbContext
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}