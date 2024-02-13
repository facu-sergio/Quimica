using Quimica.Core.Models;

namespace Quimica.Core.Bussiness
{
    public interface IShipmentService
    {
       Task InsertShipment(Shipment shipment);
       Task UpdateShipment(Shipment shipment);
       Task AddProductShipment(shipments_products shipments_Products);
       Task DeleteProductShipment(int idShipment, int idProdut);
       Task DeleteShipment(int shipmentId);
       Task<IEnumerable<Shipment>> GetShipmentsByDate(DateTime date);
       Task<Shipment> GetshipmentById(int id);
    }
}
