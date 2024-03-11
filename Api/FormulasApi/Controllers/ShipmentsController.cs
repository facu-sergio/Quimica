using AutoMapper;
using FormulasApi.DTOS;
using Microsoft.AspNetCore.Mvc;
using Quimica.Core.Bussiness;
using Quimica.Core.Models;
using Quimica.Core.Models.Filters;
using Quimica.Service.Business;

namespace FormulasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        private readonly IMapper _mapper;
        public ShipmentsController(IShipmentService shipmentService, IMapper mapper) 
        { 
            _shipmentService = shipmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getByFilters(ShipmentFilter filter)
        {
            try
            {
                if(filter.street == null && filter.Date == null)
                {
                    return BadRequest("required street or date");
                }
                var shipments = await _shipmentService.GetShipmentsByFilter(filter);
                var shipmentDtos = _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
                return Ok(shipmentDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("GetShipment")]
        public async Task<IActionResult> getShipmentById(int id)
        {
            try
            {
                return Ok(await _shipmentService.GetshipmentById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertShipment(ShipmentCreateDto shipment)
        {
            try
            {
                var shipmentFormt = _mapper.Map<Shipment>(shipment);
                await _shipmentService.InsertShipment(shipmentFormt);

                return Ok(new { Message = "Operación exitosa" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

       

        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProduct(shipments_products shipments_Products)
        {
            try
            {
                await _shipmentService.AddProductShipment(shipments_Products);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> updateShipment(Shipment shipment)
        {
            if (shipment.Id == 0)
            {
                return BadRequest(new { Message = "El campo 'id' es obligatorio." });
            }

          
            await _shipmentService.UpdateShipment(shipment);

            return Ok(new { Message = "Operación exitosa" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShipment(int idShipment)
        {
            await _shipmentService.DeleteShipment(idShipment);
            return NoContent();
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct (int idShipment,int idProduct)
        {
            await _shipmentService.DeleteProductShipment(idShipment, idProduct);
            return Ok(new { Message = "Operación exitosa" });
        }
    }
}
