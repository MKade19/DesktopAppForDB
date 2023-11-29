using Microsoft.AspNetCore.Mvc;
using BusStation.Common.Models;
using BusStation.API.Services.Abstract;
using BusStation.API.Services;

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
        public async Task<IEnumerable<BusModel>> GetAllAsync()
        {
            return await BusModelService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<BusModel> GetByIdAsync(int id)
        {
            return await BusModelService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] BusModel model)
        {
            await BusModelService.CreateOneAsync(model);
            return Created(new Uri(string.Empty), null);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] BusModel model)
        {
            await BusModelService.UpdateByIdAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await BusModelService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
