using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class Address
    {
        public int? Id { get; set; }

        public int Location_id { get; set; }
        public string Street { get; set; }
        
        public string  Number { get; set; }

        [NotMapped]
        public Location? Location { get; set; }  // Propiedad de navegación
    }
}
