using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/buses")]
    public class BusController : ControllerBase
    {
        private IBusService BusService { get; }

        public BusController(IBusService busService)
        {
            BusService = busService;
        }

        [HttpGet]
        public async Task<IEnumerable<Bus>> GetAllAsync()
        {
            return await BusService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Bus> GetByIdAsync(int id)
        {
            return await BusService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] Bus bus)
        {
            await BusService.CreateOneAsync(bus);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] Bus bus)
        {
            await BusService.UpdateByIdAsync(bus);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await BusService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
