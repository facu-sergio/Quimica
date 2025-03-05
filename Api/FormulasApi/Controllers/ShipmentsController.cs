using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quimica.Core.Bussiness;
using Quimica.Core.Models;
using Quimica.Service.DTOS;

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

        [HttpGet("GetAllByDate")]
        public async Task<IActionResult> getAllByDate(DateTime date)
        {
            try
            {
               /* var shipments = await _shipmentService.GetShipmentsByDate(date);
                var shipmentDtos = _mapper.Map<IEnumerable<ShipmentDto>>(shipments)*/
                var shipments = await _shipmentService.GetShipmentsByDate(date);
                return Ok(shipments);
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
                var shipment = (await _shipmentService.GetshipmentById(id));
                var shipmentFormat = _mapper.Map<ShipmentDto>(shipment);
                return Ok(shipmentFormat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertShipment(Shipment shipment)
        {
            try
            {
                //var shipmentFormt = _mapper.Map<Shipment>(shipment);
                await _shipmentService.InsertShipment(shipment);

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

        [HttpPut("DeleteShipment")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            await _shipmentService.DeleteShipment(id);
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
