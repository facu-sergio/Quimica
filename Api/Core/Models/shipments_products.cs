using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class shipments_products
    {
        public int IdShipment { get; set; }
        public int IdProduct { get; set; }
        public decimal  Amount { get; set; }
        public string Unit { get; set; }
    }
}
