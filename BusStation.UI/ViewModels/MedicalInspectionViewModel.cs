using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class MedicalInspectionViewModel : ViewModelBase
    {
        private MedicalInspection _medicalInspection = new MedicalInspection();
        private IMedicalInspectionDataService MedicalInspectionDataService { get; }
        private IWorkerDataService WorkerDataService { get; }
        private ObservableCollection<MedicalInspection> _medicalInspections;
        private ObservableCollection<Worker> _workers;
        private Worker _currentWorker;

        public MedicalInspectionViewModel(IMedicalInspectionDataService medicalInspectionDataService, IWorkerDataService workerDataService)
        {
            MedicalInspectionDataService = medicalInspectionDataService;
            WorkerDataService = workerDataService;
        }

        public Worker CurrentWorker
        {
            get => _currentWorker;
            set
            {
                _currentWorker = value;
                WorkerId = _currentWorker.Id;
                OnPropertyChanged(nameof(CurrentWorker));
            }
        }

        public ObservableCollection<MedicalInspection> MedicalInspections
        {
            get => _medicalInspections;
            set
            {
                _medicalInspections = value;
                OnPropertyChanged(nameof(MedicalInspections));
            }
        }

        public ObservableCollection<Worker> Workers
        {
            get => _workers;
            set
            {
                _workers = value;
                OnPropertyChanged(nameof(Workers));
            }
        }

        public int Id
        {
            get { return _medicalInspection.Id; }
            set
            {
                _medicalInspection.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public DateTime InspectionDate
        {
            get { return _medicalInspection.InspectionDate; }
            set
            {
                _medicalInspection.InspectionDate = value;
                OnPropertyChanged(nameof(InspectionDate));
            }
        }

        public int WorkerId
        {
            get { return _medicalInspection.WorkerId; }
            set
            {
                _medicalInspection.WorkerId = value;
                OnPropertyChanged(nameof(WorkerId));
            }
        }

        public bool IsAllowed
        {
            get { return _medicalInspection.IsAllowed; }
            set
            {
                _medicalInspection.IsAllowed = value;

                if (value)
                {
                    DenialReason = null;
                }

                OnPropertyChanged(nameof(IsAllowed));
                OnPropertyChanged(nameof(HasDenialReason));
            }
        }

        public string? DenialReason
        {
            get { return _medicalInspection.DenialReason; }
            set
            {
                string? newValue = value;

                if (string.IsNullOrEmpty(newValue))
                {
                    newValue = null;
                }

                _medicalInspection.DenialReason = newValue;
                OnPropertyChanged(nameof(DenialReason));
            }
        }

        public bool HasDenialReason
        {
            get => !IsAllowed;
        }

        public void ChangeCurrentMedicalInspection(object medicalInspetion)
        {
            MedicalInspection newMedicalInspection = (MedicalInspection)medicalInspetion;
            Id = newMedicalInspection.Id;
            InspectionDate = newMedicalInspection.InspectionDate;
            WorkerId = newMedicalInspection.WorkerId;
            IsAllowed = newMedicalInspection.IsAllowed;
            DenialReason = newMedicalInspection.DenialReason;
            CurrentWorker = Workers.First(w => w.Id == newMedicalInspection.WorkerId);
        }

        public async Task LoadMedicalInspectionsAsync()
        {
            var loadedMedicalInspections = await MedicalInspectionDataService.GetAllAsync();
            MedicalInspections = new ObservableCollection<MedicalInspection>(loadedMedicalInspections);
        }

        public async Task LoadMedicalInspectionsAscAsync()
        {
            var loadedMedicalInspections = await MedicalInspectionDataService.GetAllAscAsync();
            MedicalInspections = new ObservableCollection<MedicalInspection>(loadedMedicalInspections);
        }

        public async Task LoadMedicalInspectionsDecsAsync()
        {
            var loadedMedicalInspections = await MedicalInspectionDataService.GetAllDescAsync();
            MedicalInspections = new ObservableCollection<MedicalInspection>(loadedMedicalInspections);
        }

        public async Task LoadWorkersAsync()
        {
            var loadedWorkers = await WorkerDataService.GetAllAsync();
            Workers = new ObservableCollection<Worker>(loadedWorkers);
            CurrentWorker = Workers[0];
        }

        public async Task CreateMedicalInspectionAsync()
        {
            await MedicalInspectionDataService.CreateOneAsync(_medicalInspection);
        }

        public async Task UpdateMedicalInspectionAsync()
        {
            await MedicalInspectionDataService.UpdateByIdAsync(_medicalInspection);
        }

        public async Task DeleteMedicalInspectionAsync()
        {
            await MedicalInspectionDataService.DeleteByIdAsync(_medicalInspection.Id);
        }
    }
}
