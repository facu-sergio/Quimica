using FormulasApi.DTOS;
using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulasApi.DTOS
{
    public class ShipmentDto
    {
        public int? Id { get; set; }
        public string ClientName { get; set; }
        public string Location { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public float Price { get; set; }
        public string Note { get; set; }

        public int? State { get; set; }  
        public DateTime Date { get; set; }
        public List<ProductShipmentDto> Products { get; set; } = new List<ProductShipmentDto>();
    }
}
