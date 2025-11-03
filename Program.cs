using Microsoft.EntityFrameworkCore;
using sistema_teste_dev_gregpay.Data;
using sistema_teste_dev_gregpay.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// --- CÓDIGO DO BANCO ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// --- FIM DO CÓDIGO DO BANCO ---

// --- INÍCIO DA CORREÇÃO DO SWAGGER ---
// Registra os serviços para explorar os endpoints da API
builder.Services.AddEndpointsApiExplorer(); 
// Registra o serviço que gera a documentação Swagger
builder.Services.AddSwaggerGen(); 
// --- FIM DA CORREÇÃO DO SWAGGER ---

var app = builder.Build();

// --- INÍCIO DA CORREÇÃO DO SWAGGER ---
// Configuração do Swagger/OpenAPI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Gera o arquivo .json do Swagger
    app.UseSwaggerUI(); // Gera a PÁGINA INTERATIVA (/swagger)
}
// --- FIM DA CORREÇÃO DO SWAGGER ---

// app.UseHttpsRedirection();

// --- INÍCIO DA API DE PARTICIPANTES ---
var group = app.MapGroup("/api/participantes").WithTags("Participantes");

// GET: /api/participantes (Lista todos os participantes ATIVOS)
group.MapGet("/", async (AppDbContext context) =>
{
    var participantes = await context.Participantes
                                     .Where(p => p.Ativo) 
                                     .ToListAsync();
    return Results.Ok(participantes);
})
.WithName("GetParticipantes");

// GET: /api/participantes/{id} (Busca um por ID)
group.MapGet("/{id}", async (int id, AppDbContext context) =>
{
    var participante = await context.Participantes.FindAsync(id);
    return participante != null ? Results.Ok(participante) : Results.NotFound("Participante não encontrado.");
})
.WithName("GetParticipantePorId");

group.MapPost("/", async (Participante participante, AppDbContext context) =>
{
    participante.Ativo = true; // Define o participante como ativo ao criar

    context.Participantes.Add(participante);
    await context.SaveChangesAsync();

    return Results.CreatedAtRoute("GetParticipantePorId", new { id = participante.Id }, participante);
})
.WithName("CriarParticipante");

// PUT: /api/participantes/{id} (Atualiza um participante)
group.MapPut("/{id}", async (int id, Participante inputParticipante, AppDbContext context) =>
{
    var participante = await context.Participantes.FindAsync(id);
    if (participante == null)
        return Results.NotFound("Participante não encontrado.");

    participante.Nome = inputParticipante.Nome;
    participante.CpfCnpj = inputParticipante.CpfCnpj;
    participante.Email = inputParticipante.Email;

    await context.SaveChangesAsync();
    return Results.Ok(participante);
})
.WithName("AtualizarParticipante");

// DELETE: /api/participantes/{id} (Desativa um participante - Soft Delete)
group.MapDelete("/{id}", async (int id, AppDbContext context) =>
{
    var participante = await context.Participantes.FindAsync(id);
    if (participante == null)
        return Results.NotFound("Participante não encontrado.");

    participante.Ativo = false;
    await context.SaveChangesAsync();

    return Results.NoContent(); 
})
.WithName("DesativarParticipante");
// --- FIM DA API DE PARTICIPANTES ---

app.Run();