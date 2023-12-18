using Microsoft.AspNetCore.Mvc;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/bus-producers")]
    public class BusProducerController : ControllerBase
    {
        private IBusProducerService BusProducerService {  get; }   

        public BusProducerController(IBusProducerService busProducerService)
        {
            BusProducerService = busProducerService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<BusProducer>> GetAllAsync()
        {
            return await BusProducerService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<BusProducer> GetByIdAsync(int id)
        {
            return await BusProducerService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateOneAsync([FromBody] BusProducer busProducer)
        {
            TryValidateModel(busProducer);
            await BusProducerService.CreateOneAsync(busProducer);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] BusProducer busProducer)
        {
            TryValidateModel(busProducer);
            await BusProducerService.UpdateByIdAsync(busProducer);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await BusProducerService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
