using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/bus-routes")]
    public class BusRouteController : ControllerBase
    {
        private IBusRouteService BusRouteService { get; }

        public BusRouteController(IBusRouteService busRouteService)
        {
            BusRouteService = busRouteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<BusRoute>> GetAllAsync()
        {
            return await BusRouteService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<BusRoute> GetByIdAsync(int id)
        {
            return await BusRouteService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateOneAsync([FromBody] BusRoute route)
        {
            await BusRouteService.CreateOneAsync(route);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] BusRoute route)
        {
            await BusRouteService.UpdateByIdAsync(route);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await BusRouteService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
