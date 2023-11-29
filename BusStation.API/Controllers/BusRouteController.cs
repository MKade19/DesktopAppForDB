using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
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
        public async Task<IEnumerable<BusRoute>> GetAllAsync()
        {
            return await BusRouteService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<BusRoute> GetByIdAsync(int id)
        {
            return await BusRouteService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] BusRoute route)
        {
            await BusRouteService.CreateOneAsync(route);
            return Created(new Uri(string.Empty), route);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] BusRoute route)
        {
            await BusRouteService.UpdateByIdAsync(route);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await BusRouteService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
