using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/technical-inspections")]
    public class TechnicalInspectionController : ControllerBase
    {
        private ITechnicalInspectionService TechnicalInspectionService { get; }

        public TechnicalInspectionController(ITechnicalInspectionService technicalInspectionService)
        {
            TechnicalInspectionService = technicalInspectionService;
        }

        [HttpGet]
        public async Task<IEnumerable<TechnicalInspection>> GetAllAsync()
        {
            return await TechnicalInspectionService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<TechnicalInspection> GetByIdAsync(int id)
        {
            return await TechnicalInspectionService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] TechnicalInspection technicalInspection)
        {
            await TechnicalInspectionService.CreateOneAsync(technicalInspection);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] TechnicalInspection technicalInspection)
        {
            await TechnicalInspectionService.UpdateByIdAsync(technicalInspection);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await TechnicalInspectionService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
