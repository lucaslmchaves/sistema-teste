using GregPay.WebApp.Models;
using GregPay.WebApp.Services; // <-- USA O SERVIÇO
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GregPay.WebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly IFuncionarioService _funcionarioService;

        public EditModel(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        // ViewModel principal (para o formulário de Edição do Funcionário)
        [BindProperty]
        public FuncionarioViewModel Funcionario { get; set; } = new();

        // ViewModel secundário (para o formulário de "Novo Filho")
        [BindProperty]
        public FilhoViewModel NovoFilho { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // 1. USA O SERVIÇO para buscar o funcionário e seus filhos
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(id);

            if (funcionario == null)
            {
                return RedirectToPage("./Index");
            }

            Funcionario = funcionario;

            // Prepara o formulário de "Novo Filho"
            NovoFilho.FuncionarioId = id; // Define o ID do pai
            NovoFilho.DataNascimento = DateTime.Today;
            
            return Page();
        }

        // Handler para o formulário principal (Salvar dados do Funcionário)
        public async Task<IActionResult> OnPostAsync()
        {
            // Valida apenas os dados do funcionário (não do NovoFilho)
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 2. USA O SERVIÇO para atualizar o funcionário
            var sucesso = await _funcionarioService.UpdateFuncionarioAsync(Funcionario);

            if (sucesso)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "Erro ao tentar atualizar o funcionário na API.");
            return Page();
        }

        // Handler para o formulário "Novo Filho"
        public async Task<IActionResult> OnPostAddFilhoAsync()
        {
            // 3. USA O SERVIÇO para criar um novo filho
            var resultado = await _funcionarioService.CreateFilhoAsync(NovoFilho);

            if (resultado == null)
            {
                ModelState.AddModelError(string.Empty, "Erro ao adicionar filho.");
            }
            
            // Recarrega a página de Edição (OnGet) para mostrar o novo filho na lista
            return RedirectToPage(new { id = NovoFilho.FuncionarioId });
        }

        // Handler para o botão "Excluir" do filho
        public async Task<IActionResult> OnPostDeleteFilhoAsync(int filhoId, int funcionarioId)
        {
            // 4. USA O SERVIÇO para excluir o filho
            var sucesso = await _funcionarioService.DeleteFilhoAsync(filhoId);

            if (!sucesso)
            {
                ModelState.AddModelError(string.Empty, "Erro ao excluir filho.");
            }

            // Recarrega a página de Edição (OnGet) para remover o filho da lista
            return RedirectToPage(new { id = funcionarioId });
        }
    }
}

