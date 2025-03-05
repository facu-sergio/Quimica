using Quimica.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    [TableName("Direcciones")]
    public class Address 
    {
        [PrimaryKey]
        [ColumnName("id")]
        public int? Id { get; set; }

        [ColumnName("id_cliente")]
        public int id_cliente { get; set; }

        [ColumnName("calle")]
        public string calle {  get; set; }

        [ColumnName("numero")]
        public int numero { get; set; }

        [ColumnName("localidad")]
        public string localidad { get; set; }

        [Navigation(typeof(Cliente), "id_cliente")]
        public Cliente cliente { get; set; }
    }
}
