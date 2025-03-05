using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.DTOS
{
    public class ShipmentProductDto
    {
        public int Amount { get; set; }
        public string Unit_of_measure { get; set; }
        public ProductDto Producto { get; set; }
    }
}
