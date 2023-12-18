using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/positions")]
    public class PositionController : ControllerBase
    {
        private IPositionService PositionService { get; }

        public PositionController(IPositionService positionService)
        {
            PositionService = positionService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return await PositionService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Position> GetByIdAsync(int id)
        {
            return await PositionService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateOneAsync([FromBody] Position position)
        {
            await PositionService.CreateOneAsync(position);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] Position position)
        {
            await PositionService.UpdateByIdAsync(position);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await PositionService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
