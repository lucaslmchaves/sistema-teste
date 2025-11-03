using GregPay.WebApp.Models;

namespace GregPay.WebApp.Services
{
    public interface IFuncionarioService
    {
        // Métodos para Funcionários
        Task<List<FuncionarioViewModel>> GetFuncionariosAsync(string? nome, string? departamento);
        Task<FuncionarioViewModel?> GetFuncionarioByIdAsync(int id);
        Task<FuncionarioViewModel?> CreateFuncionarioAsync(FuncionarioViewModel funcionario);
        Task<bool> UpdateFuncionarioAsync(FuncionarioViewModel funcionario);
        Task<bool> DeleteFuncionarioAsync(int id);

        // --- MÉTODOS DE FILHO ATUALIZADOS ---
        Task<FilhoViewModel?> GetFilhoByIdAsync(int filhoId); // <-- ADICIONADO
        Task<FilhoViewModel?> CreateFilhoAsync(FilhoViewModel filho);
        Task<bool> UpdateFilhoAsync(FilhoViewModel filho); // <-- ADICIONADO
        Task<bool> DeleteFilhoAsync(int filhoId);
    }
}

