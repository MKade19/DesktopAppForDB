using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class MedicalInspectionService : IMedicalInspectionService
    {
        private IMedicalInspetionRepository MedicalInspetionRepository { get; }
        private IWorkerRepository WorkerRepository { get; }

        public MedicalInspectionService(IMedicalInspetionRepository medicalInspectionRepository , IWorkerRepository workerRepository)
        {
            MedicalInspetionRepository = medicalInspectionRepository;
            WorkerRepository = workerRepository;
        }
        public async Task CreateOneAsync(MedicalInspection medicalInspection)
        {
            await ValidateMedicalInspectionAsync(medicalInspection);
            await MedicalInspetionRepository.CreateOneAsync(medicalInspection);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await MedicalInspetionRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<MedicalInspection>> GetAllAsync()
        {
            return await MedicalInspetionRepository.GetAllAsync();
        }

        public async Task<MedicalInspection> GetByIdAsync(int id)
        {
            return await MedicalInspetionRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(MedicalInspection medicalInspection)
        {
            await ValidateMedicalInspectionAsync(medicalInspection);
            await MedicalInspetionRepository.UpdateByIdAsync(medicalInspection);
        }

        private async Task ValidateMedicalInspectionAsync(MedicalInspection medicalInspection)
        {
            Worker worker = await WorkerRepository.GetByIdAsync(medicalInspection.WorkerId);

            if (worker.Id == -1)
            {
                throw new BadRequestException("There is no such a worker!");
            }
        }

        public async Task<IEnumerable<MedicalInspection>> GetAllAscAsync()
        {
            return await MedicalInspetionRepository.GetAllAscAsync();
        }

        public async Task<IEnumerable<MedicalInspection>> GetAllDescAsync()
        {
            return await MedicalInspetionRepository.GetAllDescAsync();
        }
    }
}
