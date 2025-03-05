using Quimica.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    [TableName("shipments_products")]
    public class shipments_products
    {
       
        [ColumnName("id_shipment")]
        public int? id_shipment { get; set; }

        [ColumnName("id_product")]
        public int id_product { get; set; }

        [ColumnName("amount")]
        public int amount { get; set; }

        [ColumnName("unit_of_measure")]
        public string unit_of_measure { get; set; }


        [Navigation(typeof(Product), "id_product")]
        public Product? Producto { get; set; }
    }
}
