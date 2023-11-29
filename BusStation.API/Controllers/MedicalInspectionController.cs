using BusStation.API.Services.Abstract;
using BusStation.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.API.Controllers
{
    [ApiController]
    [Route("api/bus-station/medical-inspections")]
    public class MedicalInspectionController : ControllerBase
    {
        private IMedicalInspectionService MedicalInspectionService { get; }

        public MedicalInspectionController(IMedicalInspectionService medicalInspectionService)
        {
            MedicalInspectionService = medicalInspectionService;
        }

        [HttpGet]
        public async Task<IEnumerable<MedicalInspection>> GetAllAsync()
        {
            return await MedicalInspectionService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<MedicalInspection> GetByIdAsync(int id)
        {
            return await MedicalInspectionService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOneAsync([FromBody] MedicalInspection medicalInspection)
        {
            await MedicalInspectionService.CreateOneAsync(medicalInspection);
            return Created(new Uri(string.Empty), medicalInspection);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateByIdAsync([FromBody] MedicalInspection medicalInspection)
        {
            await MedicalInspectionService.UpdateByIdAsync(medicalInspection);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            await MedicalInspectionService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
