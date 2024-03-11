using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Quimica.Core.Models.Filters;

namespace Quimica.Service.Business
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly ILogger<ShipmentService> _logger;

        public ShipmentService(IShipmentRepository shipmentRepository, ILogger<ShipmentService> logger)
        {
            _shipmentRepository = shipmentRepository;
            _logger = logger;
        }
       

        public async Task InsertShipment(Shipment shipment)
        {
            try
            {
                await _shipmentRepository.InsertShipment(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"ShipmentService/InsertShipment({JsonConvert.SerializeObject(shipment)})");
                throw ex;
            }
            
        }

        public async  Task UpdateShipment(Shipment shipment)
        {
            try
            {
                await _shipmentRepository.UpdateShipment(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ShipmentService/UpdateShipment({JsonConvert.SerializeObject(shipment)})");
                throw ex;
            }
            
        }

       public async Task DeleteShipment(int shipmentId)
        {

            try
            {
                await _shipmentRepository.DeleteShipment(shipmentId);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, $"ShipmentService/DeleteShipment(shipment id:{shipmentId})");
                throw ex;
            }
            
        }


        public async Task<IEnumerable<Shipment>> GetShipmentsByFilter(ShipmentFilter filter)
        {
            try
            {
                return await _shipmentRepository.GetShipmentsByFilter(filter);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ShipmentService/GetShipmentsByFilter({filter})");
                throw ex;
            }
        }


        public async Task<Shipment> GetshipmentById (int id)
        {
            try
            {
                return await _shipmentRepository.GetShipmentByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ShipmentService/GetshipmentById({id})");
                throw;
            }
        }

        public async Task AddProductShipment(shipments_products shipments_Products)
        {
            try
            {
                await _shipmentRepository.AddProductShipment(shipments_Products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ShipmentService/AddProductShipment({JsonConvert.SerializeObject(shipments_Products)})");
                throw ex;
            }
        }

        public async Task DeleteProductShipment(int idShipment, int idProduct)
        {
            try
            {
                await _shipmentRepository.DeleteProductShipment(idShipment, idProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ShipmentService/DeleteProductShipment(idShipment:{idShipment},idProduct:{idProduct})");
                throw ex;
            }
        }
    }
}
