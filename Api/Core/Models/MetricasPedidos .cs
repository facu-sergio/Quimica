using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Models
{
    public class MetricasPedidos
    {
        public int TotalPedidos { get; set; }
        public List<ProductosVendidos> ProductosVendidos { get; set; } = new List<ProductosVendidos>();
        //public string ProductoMasVendido { get; set; }
        //public decimal TicketPromedio { get; set; }
    }
}
