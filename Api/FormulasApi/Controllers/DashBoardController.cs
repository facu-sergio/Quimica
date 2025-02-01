using Microsoft.AspNetCore.Mvc;
using Quimica.Core.Bussiness;
using Quimica.Core.Models;


namespace FormulasApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DashBoardController : Controller
    {
        private readonly ISummaryShipmentsService _summaryShipmentsService;

        public DashBoardController(ISummaryShipmentsService summaryShipmentsService)
        {
            _summaryShipmentsService = summaryShipmentsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetShipmentsByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            return Ok( await _summaryShipmentsService.GetShipmentsByDateRange(dateFrom,dateTo));
        }

        [HttpGet("GetShipmentMont")]
        public async Task<IActionResult> GetShipmentsByMonth(int month, int year)
        {
            return Ok(await _summaryShipmentsService.GetShipmentsByMont(month, year));
        }

        [HttpGet("GetMetricsMont")]
        public async Task<IActionResult> GetMetricsByMonth(int month, int year)
        {
            return Ok(await _summaryShipmentsService.GetMetricsByMonth(month, year));
        }

    }
}
