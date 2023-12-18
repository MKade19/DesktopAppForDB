using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/voyages")]
    public class VoyageController : ControllerBase
    {
        private IVoyageService VoyageService { get; }

        public VoyageController(IVoyageService voyageService)
        {
            VoyageService = voyageService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Voyage>> GetAllAsync()
        {
            return await VoyageService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Voyage> GetByIdAsync(int id)
        {
            return await VoyageService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateOneAsync([FromBody] Voyage voyage)
        {
            await VoyageService.CreateOneAsync(voyage);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] Voyage voyage)
        {
            await VoyageService.UpdateByIdAsync(voyage);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await VoyageService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
