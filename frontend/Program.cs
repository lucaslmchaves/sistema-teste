using GregPay.WebApp.Models; 
using GregPay.WebApp.Services; // 1. ADICIONADO para o serviço
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Razor Pages ao contêiner de serviços
builder.Services.AddRazorPages();

// --- CONFIGURAÇÃO DO HTTPCLIENT ---
// O Serviço (FuncionarioService) ainda precisa disto:
builder.Services.AddHttpClient("ApiGregPay", client =>
{
    // A porta HTTP correta do seu backend (API)
    client.BaseAddress = new Uri("http://localhost:5270/"); 
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});
// --- FIM DA CONFIGURAÇÃO ---

// --- REGISTRO DO SERVIÇO (SOLID) ---
// 2. ADICIONADO - Registra nossa interface e classe de serviço
// Agora podemos injetar 'IFuncionarioService' nas nossas páginas
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
// --- FIM DO REGISTRO ---


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 3. CORRIGIDO - Desativado para rodar localmente via HTTP
// app.UseHttpsRedirection();

app.MapRazorPages();

app.Run();

