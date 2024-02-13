using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class Address
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string Street { get; set; }
        public string  Number { get; set; }
    }
}
