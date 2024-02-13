using Dapper;
using Microsoft.Data.SqlClient;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;

namespace Quimica.Service.DataAccess
{
    public class FormulaRepository: IFormulaRepository
    {
        private readonly IConnectionBuilder _connectionBuilder;

        public FormulaRepository(IConnectionBuilder connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public async Task<Formula> GetFormulaBase(int id)
        {
            Formula formula = new Formula();
           
            try
            {
                using (SqlConnection db = _connectionBuilder.GetConnection())
                {
                    var lookup = new Dictionary<int, Formula>();

                    await db.QueryAsync<Formula, Materia, Formula>(
                        Formula.GetFormula,
                        (f, m) =>
                        {
                            formula.Nombre = f.Nombre;
                            formula.Materias.Add(m);
                            return f;
                        },
                        new { id },
                        splitOn: "materia_id"
                    );

                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
            }
            //formula.Materias = mat;
            return formula;
        }

        public async Task<List<Formula>> GetFormulas()
        {
            try
            {
                using (SqlConnection db = _connectionBuilder.GetConnection())
                {


                  var result =  await db.QueryAsync<Formula>(Formula.GetFormulas);
                    return result.ToList(); 

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //formula.Materias = mat;
            
        }

        public async Task<Formula> InsertFormulaBase(Formula formula)
        {
            try
            {
                Formula formulaCreated = new Formula();
                using (SqlConnection db = _connectionBuilder.GetConnection())
                {
                    int rowsaffected = db.Execute(Formula.InsertFormula, new { nombre = formula.Nombre});

                    if(rowsaffected > 0)
                    {
                        formulaCreated = await db.QuerySingleOrDefaultAsync<Formula>(Formula.GetFormulaByNombre, new {nombre = formula.Nombre});
                        return formulaCreated;
                    }
                    else
                    {
                        throw new Exception("fallo insertar nueva formula");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
