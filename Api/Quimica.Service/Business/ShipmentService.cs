using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using Microsoft.Extensions.Logging;

namespace Quimica.Service.Business
{
    public class ShipmentService : IShipmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ShipmentService> _logger;

        public ShipmentService(IUnitOfWork unitOfWork, ILogger<ShipmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }



        public async Task<int> InsertShipment(Shipment shipment)
        {
            try
            {
                // Iniciar transacción
                _unitOfWork.BeginTransaction();
                // Insertar el envío
                int shipmentId = await _unitOfWork.Shipments.AddAsync(shipment);

                //Insertar productos
                if (shipment.Products != null)
                {
                    foreach (var produc in shipment.Products)
                    {
                        produc.id_shipment = shipmentId;
                        await _unitOfWork.ShipmentsProducts.AddAsync(produc);
                    }
                }

                // Confirmar la transacción si todo está bien
                _unitOfWork.Commit();

                return shipmentId;
            }
            catch (Exception ex)
            {
                // Revertir en caso de error
                _unitOfWork.Rollback();
                throw; 
            }
        }

        public async  Task UpdateShipment(Shipment shipment)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.Shipments.UpdateAsync(shipment);
                _unitOfWork.Commit();
            }
            catch (Exception ex) 
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

       public async Task DeleteShipment(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.Shipments.SoftDeleteAsync(id);
                _unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError(ex, $"ShipmentService/DeleteShipment({id})");
                throw;
            }
        }


        public async Task<IEnumerable<Shipment>> GetShipmentsByDate(DateTime date)
        {
            try
            {
              return await _unitOfWork.Shipments.GetShipmentsByDate(date);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error en GetShipmentsByDate - Fecha: {date}, Detalles: {ex.Message}");
                throw;
            }
        }


        public async Task<Shipment> GetshipmentById(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var shipment = await _unitOfWork.Shipments.GetByIdAsync(id);
                if (shipment == null) return null;
                var addressRepository = _unitOfWork.GetRepository<Address>();
                shipment.Direccion = await addressRepository.GetByIdAsync(shipment.Id_direccion);
                var clientRepository = _unitOfWork.GetRepository<Cliente>();
                shipment.Direccion.cliente = await clientRepository.GetByIdAsync(shipment.Direccion.id_cliente);

                // Cargar los registros de la tabla pivote con sus productos
                shipment.Products = (await _unitOfWork.ShipmentsProducts
                    .GetBySpecificColumnAsync("id_shipment", shipment.Id.ToString()))
                    .ToList();

                // Si tu ORM no carga automáticamente los productos, hazlo manualmente
                foreach (var sp in shipment.Products)
                {
                    sp.Producto = await _unitOfWork.Products.GetByIdAsync(sp.id_product);
                }

                return shipment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ShipmentService/GetshipmentById({id})");
                throw;
            }
        }

        public async Task AddProductShipment(shipments_products shipments_Products)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductShipment(int idShipment, int idProduct)
        {
            throw new NotImplementedException();
        }
    }
}
