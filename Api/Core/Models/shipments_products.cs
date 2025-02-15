using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class shipments_products
    {
        public int? Id_shipment { get; set; }
        public int? Id_product { get; set; }
        public decimal?  Amount { get; set; }
        public string? unit_of_measure { get; set; }
    }
}
