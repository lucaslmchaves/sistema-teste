using Microsoft.EntityFrameworkCore;
using sistema_teste_dev_gregpay.Data;
using sistema_teste_dev_gregpay.Models; // Continua importando Models

var builder = WebApplication.CreateBuilder(args);

// --- CÓDIGO DO BANCO ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// --- FIM DO CÓDIGO DO BANCO ---

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- CORREÇÃO DE CORS ---
// Adiciona a política de CORS para permitir que o app Razor chame esta API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRazorApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:XXXX", "https://localhost:YYYY") // Colocaremos as portas do app Razor aqui depois
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// --- FIM DO CORS ---

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Mantenha comentado por enquanto

app.UseCors("AllowRazorApp"); // Ativa a política de CORS

// --- INÍCIO DA API DE FUNCIONÁRIOS (REFATORADO) ---

var group = app.MapGroup("/api/funcionarios").WithTags("Funcionarios");

// GET: /api/funcionarios (Lista todos os funcionários ATIVOS)
group.MapGet("/", async (AppDbContext context, string? nome, string? departamento) =>
{
    var query = context.Funcionarios.Include(f => f.Filhos).Where(f => f.Ativo);

    if (!string.IsNullOrEmpty(nome))
    {
        query = query.Where(f => f.Nome.Contains(nome)); // Regra: pesquisar por parte do nome
    }
    
    if (!string.IsNullOrEmpty(departamento))
    {
        query = query.Where(f => f.Departamento == departamento); // Regra: pesquisar por departamento
    }

    var funcionarios = await query.OrderBy(f => f.Nome).ToListAsync(); // Regra: Ordenar pelo nome
    return Results.Ok(funcionarios);
})
.WithName("GetFuncionarios");

// GET: /api/funcionarios/{id} (Busca um por ID, incluindo os filhos)
group.MapGet("/{id}", async (int id, AppDbContext context) =>
{
    var funcionario = await context.Funcionarios
                                   .Include(f => f.Filhos) // Inclui a lista de filhos
                                   .FirstOrDefaultAsync(f => f.Id == id);

    return funcionario != null ? Results.Ok(funcionario) : Results.NotFound("Funcionário não encontrado.");
})
.WithName("GetFuncionarioPorId");

// POST: /api/funcionarios (Cria um novo funcionário)
group.MapPost("/", async (Funcionario funcionario, AppDbContext context) =>
{
    funcionario.Ativo = true; // Garante que seja criado como ativo
    
    // EF Core vai inserir o funcionário E a lista de filhos dele
    context.Funcionarios.Add(funcionario); 
    await context.SaveChangesAsync();
    
    return Results.CreatedAtRoute("GetFuncionarioPorId", new { id = funcionario.Id }, funcionario);
})
.WithName("CriarFuncionario");

// PUT: /api/funcionarios/{id} (Atualiza um funcionário)
group.MapPut("/{id}", async (int id, Funcionario inputFuncionario, AppDbContext context) =>
{
    var funcionario = await context.Funcionarios.FindAsync(id);

    if (funcionario == null)
        return Results.NotFound("Funcionário não encontrado.");

    // Atualiza os campos principais
    funcionario.Nome = inputFuncionario.Nome;
    funcionario.Cpf = inputFuncionario.Cpf;
    funcionario.Departamento = inputFuncionario.Departamento;
    funcionario.Salario = inputFuncionario.Salario;
    funcionario.DataNascimento = inputFuncionario.DataNascimento;

    await context.SaveChangesAsync();
    return Results.Ok(funcionario);
})
.WithName("AtualizarFuncionario");

// DELETE: /api/funcionarios/{id} (Desativa um funcionário - Soft Delete)
group.MapDelete("/{id}", async (int id, AppDbContext context) =>
{
    var funcionario = await context.Funcionarios.FindAsync(id);

    if (funcionario == null)
        return Results.NotFound("Funcionário não encontrado.");

    funcionario.Ativo = false;
    await context.SaveChangesAsync();

    return Results.NoContent(); 
})
.WithName("DesativarFuncionario");
// --- FIM DA API DE FUNCIONÁRIOS ---

app.Run();