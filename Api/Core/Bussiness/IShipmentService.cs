using Quimica.Core.Models;
using Quimica.Core.Models.Filters;

namespace Quimica.Core.Bussiness
{
    public interface IShipmentService
    {
       Task InsertShipment(Shipment shipment);
       Task UpdateShipment(Shipment shipment);
       Task AddProductShipment(shipments_products shipments_Products);
       Task DeleteProductShipment(int idShipment, int idProdut);
       Task DeleteShipment(int shipmentId);
       Task<IEnumerable<Shipment>> GetShipmentsByFilter(ShipmentFilter filter);
       Task<Shipment> GetshipmentById(int id);
    }
}
