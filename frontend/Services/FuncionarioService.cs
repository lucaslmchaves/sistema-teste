using GregPay.WebApp.Models;
using System.Text;
using System.Text.Json;

namespace GregPay.WebApp.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public FuncionarioService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGregPay");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // --- FUNCIONÁRIOS (Sem alterações) ---
        public async Task<List<FuncionarioViewModel>> GetFuncionariosAsync(string? nome, string? departamento)
        {
            try
            {
                string url = $"/api/funcionarios?nome={nome}&departamento={departamento}";
                var httpResponseMessage = await _httpClient.GetAsync(url);
                if (!httpResponseMessage.IsSuccessStatusCode) return new List<FuncionarioViewModel>();
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<FuncionarioViewModel>>(contentStream, _jsonOptions) ?? new();
            }
            catch (Exception) { return new List<FuncionarioViewModel>(); }
        }
        public async Task<FuncionarioViewModel?> GetFuncionarioByIdAsync(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"/api/funcionarios/{id}");
            if (!httpResponseMessage.IsSuccessStatusCode) return null;
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<FuncionarioViewModel>(contentStream, _jsonOptions);
        }
        public async Task<FuncionarioViewModel?> CreateFuncionarioAsync(FuncionarioViewModel funcionario)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(funcionario), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PostAsync("/api/funcionarios", jsonContent);
            if (!httpResponseMessage.IsSuccessStatusCode) return null;
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<FuncionarioViewModel>(contentStream, _jsonOptions);
        }
        public async Task<bool> UpdateFuncionarioAsync(FuncionarioViewModel funcionario)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(funcionario), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PutAsync($"/api/funcionarios/{funcionario.Id}", jsonContent);
            return httpResponseMessage.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteFuncionarioAsync(int id)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"/api/funcionarios/{id}");
            return httpResponseMessage.IsSuccessStatusCode;
        }

        // --- FILHOS (Com novos métodos) ---

        // <-- NOVO MÉTODO ADICIONADO -->
        public async Task<FilhoViewModel?> GetFilhoByIdAsync(int filhoId)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"/api/filhos/{filhoId}");
            if (!httpResponseMessage.IsSuccessStatusCode) return null;

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<FilhoViewModel>(contentStream, _jsonOptions);
        }

        public async Task<FilhoViewModel?> CreateFilhoAsync(FilhoViewModel filho)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(filho), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PostAsync($"/api/funcionarios/{filho.FuncionarioId}/filhos", jsonContent);
            if (!httpResponseMessage.IsSuccessStatusCode) return null;
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<FilhoViewModel>(contentStream, _jsonOptions);
        }

        // <-- NOVO MÉTODO ADICIONADO -->
        public async Task<bool> UpdateFilhoAsync(FilhoViewModel filho)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(filho), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PutAsync($"/api/filhos/{filho.Id}", jsonContent);
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFilhoAsync(int filhoId)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"/api/filhos/{filhoId}");
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}

