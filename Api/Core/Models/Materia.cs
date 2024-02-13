namespace Quimica.Core.Models
{
    public class Materia
    {
        public int Materia_id { get; set; }
        public string Nombre { get; set; }
        public string Cantidades { get; set; }


        public static string InsertFormulaMateria
        {
            get
            {
                return @"INSERT INTO formulas_materias (materia_id, formula_id, cantidades) VALUES (@materia_id, @formula_id, @cantidades)";
            }
        }

        public static string InsertMateria
        {
            get
            {
                return @"INSERT INTO Materias (nombre) VALUES (@nombre)";
            }
        }
    }
}
