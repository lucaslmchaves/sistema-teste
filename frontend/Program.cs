using GregPay.WebApp.Models; // Importa os ViewModels
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços Razor
builder.Services.AddRazorPages();

// --- CONFIGURAÇÃO DO HTTPCLIENT ---
// Configura o serviço HttpClient para chamar a nossa API
builder.Services.AddHttpClient("ApiGregPay", client =>
{
    // ATENÇÃO: Use a URL HTTPS do seu Backend (API) aqui
    client.BaseAddress = new Uri("https://localhost:7214/"); // <-- CONFIRME ESTA PORTA
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});
// --- FIM DA CONFIGURAÇÃO ---

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Reativamos o HTTPS
app.UseHttpsRedirection();

app.MapRazorPages();

app.Run();
