using GregPay.WebApp.Models;
using GregPay.WebApp.Services; // <-- USA O SERVIÇO
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GregPay.WebApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IFuncionarioService _funcionarioService;

        public DeleteModel(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [BindProperty]
        public FuncionarioViewModel Funcionario { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // 1. USA O SERVIÇO para buscar os dados e mostrar na confirmação
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(id);

            if (funcionario == null)
            {
                return RedirectToPage("./Index"); // Não encontrou, volta para a lista
            }

            Funcionario = funcionario;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 2. USA O SERVIÇO para deletar
            var sucesso = await _funcionarioService.DeleteFuncionarioAsync(Funcionario.Id);

            if (sucesso)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao tentar excluir o funcionário na API.");
                return Page();
            }
        }
    }
}

