using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await WorkerService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Worker> GetByIdAsync(int id)
        {
            return await WorkerService.GetByIdAsync(id);
        }

        [HttpGet("position/{title}")]
        [Authorize]
        public async Task<IEnumerable<Worker>> GetByPositionAsync(string title)
        {
            return await WorkerService.GetByPosition(title);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateOneAsync([FromBody] Worker worker)
        {
            await WorkerService.CreateOneAsync(worker);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] Worker worker)
        {
            await WorkerService.UpdateByIdAsync(worker);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await WorkerService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
