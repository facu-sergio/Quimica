using Dapper;
using Microsoft.Extensions.Logging;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System.Data;

namespace Quimica.Service.DataAccess
{
    public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
    {
        private readonly IConnectionBuilder _connectionBuilder;
        private readonly ILogger<ShipmentRepository> _logger;

        public ShipmentRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }

        public async Task<IEnumerable<Shipment>> GetShipmentsByDate(DateTime Date)
        {
            try
            {
                 using (var connection = _dapperDataContext.Connection)
                {

                    string query = @"SELECT s.*,p.*, sp.amount,d.*
                                    FROM Pedidos s
                                    LEFT JOIN Direcciones d ON d.id = s.id_direccion 
                                    LEFT JOIN shipments_products sp ON s.id = sp.id_shipment
                                    LEFT JOIN Productos p ON sp.id_product = p.id
                                    WHERE s.fecha =  @date;";

                    var shipmentsDictionary = new Dictionary<int, Shipment>();

                    await connection.QueryAsync<Shipment, shipments_products, Address, Shipment>(query,
                        (shipment, product, direccion) =>
                        {
                            if (!shipmentsDictionary.TryGetValue(shipment.Id, out var existingShipment))
                            {
                                // Si no existe en el diccionario, lo agregamos
                                existingShipment = shipment;
                                existingShipment.Products = new List<shipments_products>();
                                existingShipment.Direccion = direccion;
                                shipmentsDictionary.Add(existingShipment.Id, existingShipment);
                            }

                            existingShipment.Products.Add(product);
                            return existingShipment;
                        },
                        new { date = Date },
                        splitOn: "id,id,id,id"
                    );

                    var uniqueShipments = shipmentsDictionary.Values.ToList();

                    return uniqueShipments;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
