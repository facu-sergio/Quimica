using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models.Filters
{
    public class ShipmentFilter
    {
        public string? street { get; set; }
        public string? number { get; set; }
        public DateTime Date { get; set; }
    }
}
