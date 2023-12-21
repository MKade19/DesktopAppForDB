using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/repairments")]
    public class RepairmentController : ControllerBase
    {
        private IRepairmentService RepairmentService { get; }

        public RepairmentController(IRepairmentService repairmentService)
        {
            RepairmentService = repairmentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Repairment>> GetAllAsync()
        {
            return await RepairmentService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Repairment> GetByIdAsync(int id)
        {
            return await RepairmentService.GetByIdAsync(id);
        }

        [HttpGet("bus/{number}")]
        [Authorize]
        public async Task<IEnumerable<Repairment>> GetByBusNumberAsync(string number)
        {
            return await RepairmentService.GetByBusNumberAsync(number);
        }

        [HttpGet("year-count")]
        [Authorize]
        public async Task<IEnumerable<RepairmentYearWithCount>> GetYearsWithCountAsync()
        {
            return await RepairmentService.GetYearsWithCountAsync();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateOneAsync([FromBody] Repairment repairment)
        {
            await RepairmentService.CreateOneAsync(repairment);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] Repairment repairment)
        {
            await RepairmentService.UpdateByIdAsync(repairment);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await RepairmentService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
