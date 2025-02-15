using Dapper;
using Microsoft.Extensions.Logging;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quimica.Service.DataAccess
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly IConnectionBuilder _connectionBuilder;
        private readonly ILogger<ShipmentRepository> _logger;
        private readonly IGenericRepository<Shipment> _shipmentRepo;
        private readonly IGenericRepository<Address> _addressRepo;
        private readonly IGenericRepository<shipments_products> _productRepo;

        public ShipmentRepository(IConnectionBuilder connectionBuilder, 
                                  ILogger<ShipmentRepository> logger,
                                  IGenericRepository<Shipment> shipmentRepo,
                                  IGenericRepository<Address> addressRepo,
                                  IGenericRepository<shipments_products> productRepo
                                  )
        {
            _shipmentRepo = shipmentRepo;
            _addressRepo = addressRepo;
            _productRepo = productRepo;
            _connectionBuilder = connectionBuilder;
            _logger = logger;
        }


        

        public async Task InsertShipment(Shipment shipment)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {
                            // INSERT ADDRESS
                            shipment.Address.Location = null;
                            int InsertedAddresid = await _addressRepo.AddAsync(shipment.Address, transaction);

                            // INSERT SHIPMENTS
                            shipment.addres_id = InsertedAddresid;

                            int InserShipmentId = await _shipmentRepo.AddAsync( shipment,  transaction);

                            // INSERT PRODUCTS
                            if (shipment.Products?.Any() == true)
                            {
                                foreach (var product in shipment.Products)
                                {
                                    product.Id_shipment = InserShipmentId;
                                    await _productRepo.AddAsync(product, transaction);
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ShipmentRepository/InsertShipment: {ex.Message}");
                throw;
            }
        }


        public async Task UpdateShipment(Shipment shipment)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    db.Open();

                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {

                            Shipment shipmentExist = await _shipmentRepo.GetByIdAsync(shipment.Id);
                            if (shipmentExist!=null)
                            {
                                shipment.Address.Id = shipmentExist.addres_id;
                                await _addressRepo.UpdateAsync(shipment.Address);
                                await _shipmentRepo.UpdateAsync(shipment);
                            }
                            

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logger.LogError($"Error in ShipmentRepository/UpdateShipment: {ex.Message}");
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Shipment>> GetShipmentsByDate(DateTime Date)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {

                    string query = @"SELECT s.*,p.*, sp.amount,a.*,l.*
                                    FROM Shipments s
                                    LEFT JOIN address a ON a.id = s.addres_id  
                                    LEFT JOIN shipments_products sp ON s.id = sp.id_shipment
                                    LEFT JOIN products p ON sp.id_product = p.id
                                    JOIN Location l ON l.id = a.location_id 
                                    WHERE s.date =  @date;";

                    var shipmentsDictionary = new Dictionary<int, Shipment>();

                    await db.QueryAsync<Shipment, shipments_products, Address, Location, Shipment>(query,
                        (shipment, product, address, location) =>
                        {
                            if (!shipmentsDictionary.TryGetValue(shipment.Id, out var existingShipment))
                            {
                                // Si no existe en el diccionario, lo agregamos
                                existingShipment = shipment;
                                existingShipment.Products = new List<shipments_products>();
                                existingShipment.Address = address;
                                existingShipment.Address.Location = location;
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


        public async Task<IEnumerable<Shipment>> GetShipmentsByDateRange(DateTime dateFrom, DateTime dateTo)
        {

            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"SELECT *
                     FROM Shipments
                     WHERE [date] >= @dateFrom AND [date] <= @dateTo";

                    return await db.QueryAsync<Shipment>(query, new { dateFrom = dateFrom, dateTo = dateTo });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ShipmentRepository/GetShipmentsByDateRange: {ex.Message}");
                throw ex;
            }

        }


        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            try
            {

                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"SELECT s.*, p.*, sp.amount,sp.unit_of_measure, a.*, l.*
                                    FROM Shipments s
                                    LEFT JOIN address a ON a.id = s.addres_id  
                                    LEFT JOIN shipments_products sp ON s.id = sp.id_shipment
                                    LEFT JOIN products p ON sp.id_product = p.id
                                    LEFT JOIN Location l ON l.id = a.location_id 
                                    WHERE s.id = @idShipment;";

                    Shipment shipment = (await db.QueryAsync<Shipment, shipments_products, Address, Location, Shipment>(
                        query,
                        (shipment, product, address, location) =>
                        {
                            shipment.Address = address;
                            shipment.Address.Location = location;
                            shipment.Products.Add(product);
                            return shipment;
                        },
                        new { idShipment = id },
                        splitOn: "id,id,id,id"
                    )).FirstOrDefault();

                    return shipment;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ShipmentRepository/GetShipmentByIdAsync: {ex.Message}");
                throw ex;
            }
        }

        public async Task AddProductShipment(shipments_products shipments_Products)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"INSERT INTO shipments_products (id_shipment,id_product,amount)
                                   VALUES(@idShipment,@idProduct,@amount)";


                    var param = new
                    {
                        idShipment = shipments_Products.Id_shipment,
                        idProduct = shipments_Products.Id_product,
                        amount = shipments_Products.Amount
                    };
                    await db.ExecuteAsync(query, param);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ShipmentRepository/AddProductShipment: {ex.Message}");
                throw ex;
            }
        }

        public async Task DeleteProductShipment(int idShipment, int idProduct)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"DELETE FROM shipments_products 
                                    where id_shipment = @idShipment 
                                    AND id_product = @idProduct";

                    await db.ExecuteAsync(query, new { idShipment = idShipment, idProduct = idProduct, });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ShipmentRepository/DeleteProductShipment: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteShipment(int shipmentId)
        {
            Shipment shipment = await GetShipmentByIdAsync(shipmentId);

            if (shipment != null)
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    db.Open();
                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {
                            // Llamada al primer método para eliminar productos del envío
                            //await DeleteProducts(db, shipmentId, transaction);

                            await _productRepo.DeleteAsync("id_shipment = @shipmentId", new { shipmentId });
;

                            // Llamada al tercer método para eliminar el envío
                            await DeleteShipmentDetails(db, shipmentId, transaction); 

                            // Llamada al segundo método para eliminar la dirección del envío
                            await DeleteAddress(db, shipment.Address, transaction);



                            // Si todo ha sido exitoso, confirmar la transacción
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // En caso de error, revertir la transacción y manejar la excepción
                            transaction.Rollback();
                            _logger.LogError($"Error in DeleteShipment: {ex.Message}");
                            throw;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Shipment Inexistente");
            }

        }

        public async Task<List<ProductOfShipment>> GetProductsByShipment(int idShipment)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"
                SELECT sp.*, p.name AS Name  
                FROM shipments_products sp           
                LEFT JOIN products p ON p.id = sp.id_product   
                WHERE sp.id_shipment = @idShipment"; 
        
            var products = await db.QueryAsync<ProductOfShipment>(
                query,
                new { idShipment }  // Simplifica el parámetro
            );

                    return products.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ShipmentRepository/GetProductsByShipment: {ex.Message}"); // Corrige el nombre del método
                throw;
            }
        }

        private async Task DeleteAddress(IDbConnection db, Address address, IDbTransaction transaction)
        {

            if (address != null)
            {
                string query = @"DELETE FROM Address WHERE id = @addressID";
                await db.ExecuteAsync(query, new { addressID = address.Id }, transaction);
            }
        }

        private async Task DeleteShipmentDetails(IDbConnection db, int shipmentId, IDbTransaction transaction)
        {
            string query = @"DELETE FROM Shipments WHERE id = @shipmentId";
            await db.ExecuteAsync(query, new { shipmentId = shipmentId }, transaction);
        }

        private async Task DeleteProducts(IDbConnection db, int shipmentId, IDbTransaction transaction)
        {
            string query = @"DELETE FROM shipments_products WHERE id_shipment = @shipmentID";
            await db.ExecuteAsync(query, new { shipmentID = shipmentId }, transaction);
        }

       
    }
}
