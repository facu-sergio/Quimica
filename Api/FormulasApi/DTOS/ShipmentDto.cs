using FormulasApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.DTOS
{
    public class ShipmentDto : AuditableDto
    {
        public int Id { get; set; }
        public float Precio { get; set; }
        public string Nota { get; set; }    
        //public DateTime Fecha { get; set; }
        public AddresDto Direccion { get; set; }
        public List<ShipmentProductDto> Products { get; set; }

    }
}
