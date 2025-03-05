using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.DTOS
{
    public class AddresDto
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }    
        public string Localidad { get; set; }
        public ClienteDto Cliente { get; set; }
    }
}
