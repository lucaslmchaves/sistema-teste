using GregPay.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace GregPay.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Lista de funcionários para mostrar na tabela
        public List<FuncionarioViewModel> Funcionarios { get; set; } = new List<FuncionarioViewModel>();

        // Propriedades para os campos de filtro
        [BindProperty(SupportsGet = true)]
        public string? FiltroNome { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroDepartamento { get; set; }

        public async Task OnGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiGregPay");

            // Constrói a URL da API com os filtros
            string url = $"/api/funcionarios?nome={FiltroNome}&departamento={FiltroDepartamento}";

            try
            {
                var httpResponseMessage = await httpClient.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    
                    // Configura o JsonSerializer para não ser case-sensitive
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    
                    Funcionarios = await JsonSerializer.DeserializeAsync<List<FuncionarioViewModel>>(contentStream, options) ?? new List<FuncionarioViewModel>();
                }
            }
            catch (Exception)
            {
                // Lidar com o erro (ex: API está offline)
                Funcionarios = new List<FuncionarioViewModel>();
            }
        }
    }
}
