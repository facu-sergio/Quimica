using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.Business
{
    public class SummaryShipmentsService : ISummaryShipmentsService
    {

        private readonly IShipmentRepository _shipmentReposity;
        
        public SummaryShipmentsService( IShipmentRepository shipmentReposity)
        {
            _shipmentReposity = shipmentReposity;
        }

        public  async Task<IEnumerable<Shipment>> GetShipmentsByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            return await _shipmentReposity.GetShipmentsByDateRange(dateFrom, dateTo);
        }


        public async  Task<IEnumerable<Shipment>> GetShipmentsByMont(int month, int year)
        {
            // Validar el mes
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "El mes debe estar entre 1 y 12.");
            }

            IEnumerable<Shipment> shipments;

            DateTime dateFrom = new DateTime(year, month, 1);

            DateTime dateTo = dateFrom.AddMonths(1).AddDays(-1);

            shipments = await _shipmentReposity.GetShipmentsByDateRange(dateFrom, dateTo);

            return shipments;
        }

        public async Task<MetricasPedidos> GetMetricsByMonth(int month, int year)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "El mes debe estar entre 1 y 12.");
            }

            IEnumerable<Shipment> shipments;

            DateTime dateFrom = new DateTime(year, month, 1);

            DateTime dateTo = dateFrom.AddMonths(1).AddDays(-1);

            shipments = await _shipmentReposity.GetShipmentsByDateRange(dateFrom, dateTo);

            MetricasPedidos metric  = new MetricasPedidos();

            metric.TotalPedidos = shipments.Count();

            var shipmentList = shipments.ToList();

            for (int i = 0; i < shipments.Count(); i++) 
            {

                List<ProductOfShipment> products = await _shipmentReposity.GetProductsByShipment(shipmentList[i].Id);
                for (int j = 0; j < products.Count(); j++)
                {
                    var existingProduct = metric.ProductosVendidos.FirstOrDefault(p => p.name == products[j].Name);

                    if(metric.ProductosVendidos.Any(p => p.name == products[j].Name))
                    {
                        existingProduct.Amount += products[j].Amount;
                    }
                    else
                    {
                        metric.ProductosVendidos.Add(new ProductosVendidos { name = products[j].Name, Amount = products[j].Amount, UnitOfMeasure = products[j].unit_of_measure });
                    }
                }
                
            }
            return metric;
        }


    }
}
