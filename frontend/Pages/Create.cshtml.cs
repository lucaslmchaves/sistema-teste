using GregPay.WebApp.Models;
using GregPay.WebApp.Services; // <-- USA O SERVIÇO
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GregPay.WebApp.Pages
{
    public class CreateModel : PageModel
    {
        // 1. INJETA O SERVIÇO
        private readonly IFuncionarioService _funcionarioService;

        public CreateModel(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [BindProperty]
        public FuncionarioViewModel Funcionario { get; set; } = new();

        public void OnGet()
        {
            // Define a data de nascimento padrão para o formulário
            Funcionario.DataNascimento = DateTime.Today;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // A validação agora checa o [Required] E o [CpfValidation]
            if (!ModelState.IsValid)
            {
                return Page(); // Retorna à página, mostrando os erros de validação
            }

            // 2. USA O SERVIÇO
            var resultado = await _funcionarioService.CreateFuncionarioAsync(Funcionario);

            if (resultado != null)
            {
                // Se funcionou, redireciona de volta para a lista (Index)
                return RedirectToPage("./Index");
            }
            else
            {
                // Adiciona um erro se a API falhar
                ModelState.AddModelError(string.Empty, "Erro ao tentar criar funcionário na API.");
                return Page();
            }
        }
    }
}

