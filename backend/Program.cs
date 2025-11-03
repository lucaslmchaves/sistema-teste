using Microsoft.EntityFrameworkCore;
using sistema_teste_dev_gregpay.Data;
using sistema_teste_dev_gregpay.Models; 

var builder = WebApplication.CreateBuilder(args);

// --- CÓDIGO DO BANCO ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// --- FIM DO CÓDIGO DO BANCO ---

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- CORREÇÃO DE CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRazorApp",
        policy =>
        {
            // Substitua pelas portas HTTP e HTTPS do seu frontend (ex: 5027 e 7027)
            policy.WithOrigins("http://localhost:5027", "https://localhost:7027") 
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

// Descomentado para HTTP (mais fácil para testes locais)
// app.UseHttpsRedirection(); 

app.UseCors("AllowRazorApp"); // Ativa a política de CORS

// --- INÍCIO DA API DE FUNCIONÁRIOS ---
var group = app.MapGroup("/api/funcionarios").WithTags("Funcionarios");

// ... (Todos os endpoints de Funcionários - GET, POST, PUT, DELETE - permanecem iguais) ...
group.MapGet("/", async (AppDbContext context, string? nome, string? departamento) =>
{
    var query = context.Funcionarios.Include(f => f.Filhos).Where(f => f.Ativo);
    if (!string.IsNullOrEmpty(nome)) { query = query.Where(f => f.Nome.Contains(nome)); }
    if (!string.IsNullOrEmpty(departamento)) { query = query.Where(f => f.Departamento == departamento); }
    var funcionarios = await query.OrderBy(f => f.Nome).ToListAsync();
    return Results.Ok(funcionarios);
}).WithName("GetFuncionarios");

group.MapGet("/{id}", async (int id, AppDbContext context) =>
{
    var funcionario = await context.Funcionarios.Include(f => f.Filhos).FirstOrDefaultAsync(f => f.Id == id);
    return funcionario != null ? Results.Ok(funcionario) : Results.NotFound("Funcionário não encontrado.");
}).WithName("GetFuncionarioPorId");

group.MapPost("/", async (Funcionario funcionario, AppDbContext context) =>
{
    funcionario.Ativo = true;
    context.Funcionarios.Add(funcionario); 
    await context.SaveChangesAsync();
    return Results.CreatedAtRoute("GetFuncionarioPorId", new { id = funcionario.Id }, funcionario);
}).WithName("CriarFuncionario");

group.MapPut("/{id}", async (int id, Funcionario inputFuncionario, AppDbContext context) =>
{
    var funcionario = await context.Funcionarios.FindAsync(id);
    if (funcionario == null) return Results.NotFound("Funcionário não encontrado.");
    funcionario.Nome = inputFuncionario.Nome;
    funcionario.Cpf = inputFuncionario.Cpf;
    funcionario.Departamento = inputFuncionario.Departamento;
    funcionario.Salario = inputFuncionario.Salario;
    funcionario.DataNascimento = inputFuncionario.DataNascimento;
    await context.SaveChangesAsync();
    return Results.Ok(funcionario);
}).WithName("AtualizarFuncionario");

group.MapDelete("/{id}", async (int id, AppDbContext context) =>
{
    var funcionario = await context.Funcionarios.FindAsync(id);
    if (funcionario == null) return Results.NotFound("Funcionário não encontrado.");
    funcionario.Ativo = false;
    await context.SaveChangesAsync();
    return Results.NoContent(); 
}).WithName("DesativarFuncionario");

// POST: /api/funcionarios/{id}/filhos
group.MapPost("/{id}/filhos", async (int id, Filho filho, AppDbContext context) =>
{
    var funcionario = await context.Funcionarios.FindAsync(id);
    if (funcionario == null) return Results.NotFound("Funcionário não encontrado.");
    filho.FuncionarioId = id;
    context.Filhos.Add(filho);
    await context.SaveChangesAsync();
    return Results.Created($"/api/filhos/{filho.Id}", filho);
}).WithName("AdicionarFilho");
// --- FIM DA API DE FUNCIONÁRIOS ---


// --- INÍCIO DA API DE FILHOS ---
var filhosGroup = app.MapGroup("/api/filhos").WithTags("Filhos");

// --- NOVO ENDPOINT ADICIONADO ---
// GET: /api/filhos/{filhoId} (Busca um filho específico)
filhosGroup.MapGet("/{filhoId}", async (int filhoId, AppDbContext context) =>
{
    var filho = await context.Filhos.FindAsync(filhoId);
    return filho != null ? Results.Ok(filho) : Results.NotFound("Filho não encontrado.");
})
.WithName("GetFilhoPorId");
// --- FIM DO NOVO ENDPOINT ---

// PUT: /api/filhos/{filhoId} (Atualiza um filho existente)
filhosGroup.MapPut("/{filhoId}", async (int filhoId, Filho inputFilho, AppDbContext context) =>
{
    var filho = await context.Filhos.FindAsync(filhoId);
    if (filho == null) return Results.NotFound("Filho não encontrado.");
    filho.Nome = inputFilho.Nome;
    filho.Cpf = inputFilho.Cpf;
    filho.DataNascimento = inputFilho.DataNascimento;
    await context.SaveChangesAsync();
    return Results.Ok(filho);
}).WithName("AtualizarFilho");

// DELETE: /api/filhos/{filhoId} (Exclui um filho)
filhosGroup.MapDelete("/{filhoId}", async (int filhoId, AppDbContext context) =>
{
    var filho = await context.Filhos.FindAsync(filhoId);
    if (filho == null) return Results.NotFound("Filho não encontrado.");
    context.Filhos.Remove(filho);
    await context.SaveChangesAsync();
    return Results.NoContent();
}).WithName("ExcluirFilho");
// --- FIM DA API DE FILHOS ---

app.Run();

