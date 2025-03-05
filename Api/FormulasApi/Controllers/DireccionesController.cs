using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quimica.Core.Bussiness;
using Quimica.Core.Models;

namespace FormulasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionesController : ControllerBase
    {

        private readonly IDireccionServices _direccionServices;
        private readonly IMapper _mapper;

        public DireccionesController(IDireccionServices direccionServices, IMapper mapper)
        {
            _direccionServices = direccionServices;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> InsertAddres(Address address)
        {
            try
            {
                int addressId = await _direccionServices.AddDireccionAsync(address);

                return Ok(new { Message = "Operación exitosa" });
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
