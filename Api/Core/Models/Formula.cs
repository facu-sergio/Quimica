namespace Quimica.Core.Models
{
    public class Formula
    {
        public int formula_id { get; set; }
        public string Nombre { get; set; }
        public string image { get; set; }
        //mapping
        public List<Materia> Materias { get; set; } = new List<Materia>();

        public static string GetFormulas
        {
            get
            {
                return @"SELECT * FROM Formulas";
            }
        }
        public static string GetFormula
        {
            get
            {
                return @"SELECT F.formula_id, F.nombre, M.materia_id, M.nombre, MF.cantidades
                    FROM Formulas F
                    INNER JOIN formulas_materias MF ON F.formula_id = MF.formula_id
                    INNER JOIN Materias M ON MF.materia_id = M.materia_id
                    WHERE F.formula_id = @id";
            }
        }

        public static string GetFormulaByNombre
        {
            get
            {
                return @"SELECT F.formula_id, F.nombre, M.materia_id, M.nombre, MF.cantidades
                    FROM Formulas F
                    INNER JOIN formulas_materias MF ON F.formula_id = MF.formula_id
                    INNER JOIN Materias M ON MF.materia_id = M.materia_id
                    WHERE F.nombre = @nombre";
            }
        }
        public static string InsertFormula
        {
            get
            {
                return @"INSERT INTO Formulas ([nombre]) VALUES(@nombre)";
            }
        }

        
    }
}
