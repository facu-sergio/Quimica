using Quimica.Core.attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    [Table("shipments")]
    public class Shipment
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public float Price { get; set; }
        public string Note { get; set; }

        public int? State { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [NotUpdatable]
        public int? addres_id { get; set; } 
        public Address? Address { get; set; }  // Propiedad de navegación

        public List<shipments_products>? Products { get; set; } = new List<shipments_products>();
        
    }
}
