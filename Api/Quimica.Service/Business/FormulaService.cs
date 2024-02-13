using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.Business
{
    public class FormulaService : IFormulaService
    {
        private readonly IFormulaRepository _formulaRepository;

        public FormulaService(IFormulaRepository formulaRepository)
        {
           _formulaRepository = formulaRepository;
        }
        public async Task<Formula> GetFormula(int id, int cantidad)
        {
            try
            {
                Formula formulaBase = await _formulaRepository.GetFormulaBase(id);
               for (int i = 0;i<formulaBase.Materias.Count;i++)
                {
                    string cantidadOriginal = formulaBase.Materias[i].Cantidades;
                    string cantidadNumerica = new string(cantidadOriginal.Where(c=>char.IsDigit(c) || c == ',').ToArray());
                    
                    if (float.TryParse(cantidadNumerica, out float cantidadNumericaValor))
                    {
                        float resultado = cantidadNumericaValor * cantidad;
                        if (resultado > 1000)
                        {
                            resultado = resultado/1000;
                            cantidadOriginal = cantidadOriginal.Replace("Gr", "Kg");
                            cantidadOriginal = cantidadOriginal.Replace("Ml", "Lts");
                        }
                        formulaBase.Materias[i].Cantidades = resultado + cantidadOriginal.Substring(cantidadNumerica.Length);
                    }
                }
               return formulaBase;
            }catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Formula> GetFormulaBase(int id)
        {
            try
            {
                return await _formulaRepository.GetFormulaBase(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<List<Formula>> GetFormulas()
        {
            return _formulaRepository.GetFormulas();
        }

        public async Task<Formula> InsertFormula(Formula formula)  
        {
            try
            {
                return await _formulaRepository.InsertFormulaBase(formula);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
