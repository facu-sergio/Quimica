using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.DataAccess
{
    public interface IFormulaRepository
    {
        Task<List<Formula>> GetFormulas();
        Task<Formula> GetFormulaBase(int id);
        Task<Formula> InsertFormulaBase(Formula formula);
    }
}
