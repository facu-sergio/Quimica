using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.DataAccess
{
    public interface IShipmentRepository
    {
        Task<Shipment> GetShipmentByIdAsync(int id);
        Task InsertShipment(Shipment shipment);
        Task UpdateShipment(Shipment shipment);
        Task  AddProductShipment(shipments_products shipments_Products);
        Task DeleteProductShipment(int idShipment, int idProdut);
        Task DeleteShipment(int shipmentId);
        Task<IEnumerable<Shipment>> GetShipmentsByDate(DateTime date);

    }
}
