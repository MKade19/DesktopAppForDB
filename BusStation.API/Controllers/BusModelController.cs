using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{

    [ApiController]
    [Route("api/bus-station/bus-models")]
    public class BusModelController : ControllerBase
    {
        private IBusModelService BusModelService { get; }

        public BusModelController(IBusModelService busModelService)
        {
            BusModelService = busModelService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<BusModel>> GetAllAsync()
        {
            return await BusModelService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<BusModel> GetByIdAsync(int id)
        {
            return await BusModelService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateOneAsync([FromBody] BusModel model)
        {
            TryValidateModel(model);
            await BusModelService.CreateOneAsync(model);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] BusModel model)
        {
            TryValidateModel(model);
            await BusModelService.UpdateByIdAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await BusModelService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
