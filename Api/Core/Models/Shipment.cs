using Quimica.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    [TableName("Pedidos")]
    public class Shipment : Auditable
    {
        [ColumnName("id_direccion")]
        public int Id_direccion { get; set; } // Hacer opcional con int?

        [Navigation(typeof(Address), "id_direccion")]
        public Address? Direccion { get; set; } // Hacer opcional con ?

        [ColumnName("fecha")]
        public DateTime? Fecha { get; set; } 

        [ColumnName("nota")]
        public string? Nota { get; set; } // Ya es opcional porque es string?

        [ColumnName("id_estado")]
        public int? id_estado { get; set; } // Hacer opcional con int?

        [ColumnName("precio")]
        public float? Precio { get; set; } // Hacer opcional con float?

        [Navigation(typeof(shipments_products), "id_shipment")]
        public List<shipments_products>? Products { get; set; } // Hacer opcional con ?
    }
}
