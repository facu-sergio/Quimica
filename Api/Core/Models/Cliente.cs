using Quimica.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    [TableName("Clientes")]
    public class Cliente
    {
        [PrimaryKey]
        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("nombre")]
        public string  Nombre { get; set; }

        [ColumnName("telefono")]
        public string Telefono { get; set; }

        [ColumnName("fecha_alta")]
        public DateTime Fecha_alta { get; set; }
    }
}
