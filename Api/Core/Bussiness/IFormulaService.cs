using Quimica.Core.Models;

namespace Quimica.Core.Bussiness
{
    public interface IFormulaService
    {
        Task<List<Formula>> GetFormulas();
        Task<Formula> GetFormulaBase(int id);
        Task<Formula> InsertFormula(Formula formula);
        Task<Formula> GetFormula(int id,int cantidad);
    }
}
