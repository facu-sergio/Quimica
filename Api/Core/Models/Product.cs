using Quimica.Core.Attributes;

namespace Quimica.Core.Models
{
    [TableName("Productos")]
    public class Product
    {
        [PrimaryKey]
        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("nombre")]
        public string Nombre { get; set; }

    }
}
