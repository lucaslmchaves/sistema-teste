using GregPay.WebApp.Models;
using GregPay.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GregPay.WebApp.Pages
{
    public class EditFilhoModel : PageModel
    {
        private readonly IFuncionarioService _funcionarioService;

        public EditFilhoModel(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [BindProperty]
        public FilhoViewModel Filho { get; set; } = new();

        // Precisamos disto para o botão "Cancelar"
        [BindProperty(SupportsGet = true)]
        public int FuncionarioId { get; set; } 

        public async Task<IActionResult> OnGetAsync(int filhoId)
        {
            var filho = await _funcionarioService.GetFilhoByIdAsync(filhoId);
            if (filho == null)
            {
                // Se não encontrar o filho, volta para a lista principal
                return RedirectToPage("./Index");
            }

            Filho = filho;
            FuncionarioId = filho.FuncionarioId; // Guarda o ID do pai
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Mostra erros de validação
            }

            var sucesso = await _funcionarioService.UpdateFilhoAsync(Filho);

            if (sucesso)
            {
                // Volta para a página de edição do PAI
                return RedirectToPage("./Edit", new { id = Filho.FuncionarioId });
            }

            ModelState.AddModelError(string.Empty, "Erro ao atualizar o filho na API.");
            return Page();
        }
    }
}

