using Microsoft.AspNetCore.Mvc;
using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using Quimica.Service.DTOS;

namespace FormulasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulasController : Controller
    {
        private readonly IFormulaService _formulaService;
        public FormulasController(IFormulaService formulaService)
        {
            _formulaService = formulaService; 
        }

        [HttpGet]
        public async Task<List<Formula>> GetFormulas()
        {
            return await _formulaService.GetFormulas();
        }
        [HttpGet("{id}")]
        public async Task<Formula> GetFormulaBase(int id)
        {
            return await _formulaService.GetFormulaBase(id);
        }

        [HttpPost]
        public async Task<Formula> InsertFormula(Formula formula)
        {
            return await _formulaService.InsertFormula(formula);
        }

        [HttpPost("calculate")]
        public async Task<Formula> CalculateFormula([FromBody] CalculateFormulaModel calculate)
        {
            return await _formulaService.GetFormula(calculate.formula_id,calculate.cantidad);
        }

    }
}
