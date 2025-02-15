using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class ProductOfShipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
       public string unit_of_measure { get; set; }
    }
}
