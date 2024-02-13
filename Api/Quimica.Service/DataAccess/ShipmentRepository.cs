using Dapper;
using Microsoft.Extensions.Logging;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System.Data;

namespace Quimica.Service.DataAccess
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly IConnectionBuilder _connectionBuilder;
        private readonly ILogger<ShipmentRepository> _logger;

        public ShipmentRepository(IConnectionBuilder connectionBuilder, ILogger<ShipmentRepository> logger)
        {
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
                            int InsertedAddresid = await InsertAddress(db, shipment.Address, transaction);

                            // INSERT SHIPMENTS
                            int InserShipmentId = await InsertShipmentDetails(db, shipment, InsertedAddresid, transaction);

                            // INSERT PRODUCTS
                            await InsertProducts(db, shipment.Products, InserShipmentId, transaction);

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
                            Shipment shipmentExist = await GetShipmentByIdAsync(shipment.Id);

                            await UpdateAddress(db, shipment.Address, transaction);

                            await UpdateShipmentDetails(db, shipment, transaction);

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

                    await db.QueryAsync<Shipment, ProductOfShipment, Address, Location, Shipment>(query,
                        (shipment, product, address, location) =>
                        {
                            if (!shipmentsDictionary.TryGetValue(shipment.Id, out var existingShipment))
                            {
                                // Si no existe en el diccionario, lo agregamos
                                existingShipment = shipment;
                                existingShipment.Products = new List<ProductOfShipment>();
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

        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            try
            {

                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"SELECT s.*, p.*, sp.amount, a.*, l.*
                                    FROM Shipments s
                                    LEFT JOIN address a ON a.id = s.addres_id  
                                    LEFT JOIN shipments_products sp ON s.id = sp.id_shipment
                                    LEFT JOIN products p ON sp.id_product = p.id
                                    LEFT JOIN Location l ON l.id = a.location_id 
                                    WHERE s.id = @idShipment;";

                    Shipment shipment = (await db.QueryAsync<Shipment, ProductOfShipment, Address, Location, Shipment>(
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
                        idShipment = shipments_Products.idShipment,
                        idProduct = shipments_Products.idProduct,
                        amount = shipments_Products.amount
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
                            await DeleteProducts(db, shipmentId, transaction);

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


        private async Task<int> InsertAddress(IDbConnection db, Address address, IDbTransaction transaction)
        {
            string query = @"INSERT INTO Address(location_id,street,number)
                            OUTPUT INSERTED.Id  
                            VALUES (@locationID,@street,@number)";

            var param = new
            {
                street = address.Street,
                number = address.Number,
                locationID = address.Location.Id
            };

            return await db.QueryFirstOrDefaultAsync<int>(query, param, commandType: CommandType.Text, transaction: transaction);
        }

        private async Task<int> InsertShipmentDetails(IDbConnection db, Shipment shipment, int addressId, IDbTransaction transaction)
        {
            string query = @"INSERT INTO Shipments(clientName, price, note, date, addres_id, state)
                             OUTPUT INSERTED.Id   
                             VALUES (@clientName, @price, @note, @date, @addressId, @state)";

            var param = new
            {
                clientName = shipment.ClientName,
                price = shipment.Price,
                note = shipment.Note,
                date = shipment.Date,
                state = shipment.State,
                addressId = addressId
            };
            return await db.QueryFirstOrDefaultAsync<int>(query, param, commandType: CommandType.Text, transaction: transaction);
        }

        private async Task InsertProducts(IDbConnection db, List<ProductOfShipment> products, int idShipment, IDbTransaction transaction)
        {
            foreach (ProductOfShipment product in products)
            {
                string query = @"INSERT INTO shipments_products (id_shipment,id_product,amount)
                                 VALUES(@idShipment,@idProduct,@amount)";

                var param = new { idShipment = idShipment, idProduct = product.Id, amount = product.Amount };

                await db.ExecuteAsync(query, param, commandType: CommandType.Text, transaction: transaction);
            }

        }
        private async Task UpdateAddress(IDbConnection db, Address address, IDbTransaction transaction)
        {

            if(address.Id == 0 || address.Id == null )
            {
                throw new Exception("Id no puede ser 0 o null");
            }

            string query = @"UPDATE Address 
                             SET location_id = @locationId, street = @street, number = @number
                             WHERE id = @addressId";

            var param = new
            {
                locationId = address.Location.Id,
                street = address.Street,
                number = address.Number,
                addressId = address.Id
            };

            await db.ExecuteAsync(query, param, transaction);
        }

        private async Task UpdateShipmentDetails(IDbConnection db, Shipment shipment, IDbTransaction transaction)
        {
            string query = @" UPDATE Shipments 
                            SET clientName = @clientName, price = @price, note = @note, date = @date, state = @state
                            WHERE id = @id";

            var param = new
            {
                id = shipment.Id,
                clientName = shipment.ClientName,
                price = shipment.Price,
                note = shipment.Note,
                date = shipment.Date,
                state = shipment.State
            };

            await db.ExecuteAsync(query, param, transaction);
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
