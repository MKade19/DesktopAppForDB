using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
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
        public async Task<IEnumerable<Repairment>> GetAllAsync()
        {
            return await RepairmentService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Repairment> GetByIdAsync(int id)
        {
            return await RepairmentService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] Repairment repairment)
        {
            await RepairmentService.CreateOneAsync(repairment);
            return Created(new Uri(string.Empty), repairment);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] Repairment repairment)
        {
            await RepairmentService.UpdateByIdAsync(repairment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await RepairmentService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
