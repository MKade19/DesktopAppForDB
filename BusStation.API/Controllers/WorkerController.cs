using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/workers")]
    public class WorkerController : ControllerBase
    {
        private IWorkerService WorkerService { get; }

        public WorkerController(IWorkerService workerService)
        {
            WorkerService = workerService;
        }

        [HttpGet]
        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await WorkerService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Worker> GetByIdAsync(int id)
        {
            return await WorkerService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] Worker worker)
        {
            await WorkerService.CreateOneAsync(worker);
            return Created(new Uri(string.Empty), worker);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] Worker worker)
        {
            await WorkerService.UpdateByIdAsync(worker);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await WorkerService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
