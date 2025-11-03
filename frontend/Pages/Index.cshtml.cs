using GregPay.WebApp.Models;
using GregPay.WebApp.Services; // <-- USA O SERVIÇO
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GregPay.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFuncionarioService _funcionarioService;

        public IndexModel(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        public List<FuncionarioViewModel> Funcionarios { get; set; } = new();

        // Propriedades para os filtros de busca (Regra de Negócio)
        // Os nomes SÃO 'Nome' e 'Departamento'
        [BindProperty(SupportsGet = true)]
        public string? Nome { get; set; } 

        [BindProperty(SupportsGet = true)]
        public string? Departamento { get; set; }

        public async Task OnGetAsync()
        {
            // A página agora passa as propriedades corretas para o serviço
            Funcionarios = await _funcionarioService.GetFuncionariosAsync(Nome, Departamento);
        }
    }
}

