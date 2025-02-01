using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.Bussiness
{
    public interface ISummaryShipmentsService
    {
        Task<IEnumerable<Shipment>> GetShipmentsByDateRange(DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<Shipment>> GetShipmentsByMont(int month, int year);
        Task<MetricasPedidos> GetMetricsByMonth(int month, int year);
    }
}
