using Microsoft.AspNetCore.Mvc;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

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
        public async Task<IEnumerable<BusProducer>> GetAllAsync()
        {
            return await BusProducerService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<BusProducer> GetByIdAsync(int id)
        {
            return await BusProducerService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] BusProducer busProducer)
        {
            await BusProducerService.CreateOneAsync(busProducer);
            return Created(new Uri(string.Empty), busProducer);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] BusProducer busProducer)
        {
            await BusProducerService.UpdateByIdAsync(busProducer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await BusProducerService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
