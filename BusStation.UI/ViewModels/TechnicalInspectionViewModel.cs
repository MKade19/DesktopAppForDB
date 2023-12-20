using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class TechnicalInspectionViewModel : ViewModelBase
    {
        private TechnicalInspection _technicalInspection = new TechnicalInspection();
        private ITechnicalInspectionDataService TechnicalInspectionDataService { get; }
        private IBusDataService BusDataService { get; }
        private ObservableCollection<TechnicalInspection> _technicalInspections;
        private ObservableCollection<Bus> _buses;
        private Bus _currentBus;

        public TechnicalInspectionViewModel(ITechnicalInspectionDataService technicalInspectionDataService, IBusDataService busDataService)
        {
            TechnicalInspectionDataService = technicalInspectionDataService;
            BusDataService = busDataService;
        }

        public Bus CurrentBus
        {
            get => _currentBus;
            set
            {
                _currentBus = value;
                BusId = _currentBus.Id;
                OnPropertyChanged(nameof(CurrentBus));
            }
        }

        public ObservableCollection<TechnicalInspection> TechnicalInspections
        {
            get => _technicalInspections;
            set
            {
                _technicalInspections = value;
                OnPropertyChanged(nameof(TechnicalInspections));
            }
        }

        public ObservableCollection<Bus> Buses
        {
            get => _buses;
            set
            {
                _buses = value;
                OnPropertyChanged(nameof(Buses));
            }
        }

        public int Id
        {
            get { return _technicalInspection.Id; }
            set
            {
                _technicalInspection.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public DateTime InspectionDate
        {
            get { return _technicalInspection.InspectionDate; }
            set
            {
                _technicalInspection.InspectionDate = value;
                OnPropertyChanged(nameof(InspectionDate));
            }
        }

        public int BusId
        {
            get { return _technicalInspection.BusId; }
            set
            {
                _technicalInspection.BusId = value;
                OnPropertyChanged(nameof(BusId));
            }
        }

        public bool IsAllowed
        {
            get { return _technicalInspection.IsAllowed; }
            set
            {
                _technicalInspection.IsAllowed = value;

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
            get { return _technicalInspection.DenialReason; }
            set
            {
                string? newValue = value;

                if (string.IsNullOrEmpty(newValue))
                {
                    newValue = null;
                }

                _technicalInspection.DenialReason = newValue;
                OnPropertyChanged(nameof(DenialReason));
            }
        }

        public bool HasDenialReason
        {
            get => !IsAllowed;
        }

        public void ChangeCurrentTechnicalInspection(object tehnicalInspetion)
        {
            TechnicalInspection newTechnicalInspection = (TechnicalInspection)tehnicalInspetion;
            Id = newTechnicalInspection.Id;
            InspectionDate = newTechnicalInspection.InspectionDate;
            BusId = newTechnicalInspection.BusId;
            IsAllowed = newTechnicalInspection.IsAllowed;
            DenialReason = newTechnicalInspection.DenialReason;
            CurrentBus = Buses.First(w => w.Id == newTechnicalInspection.BusId);
        }

        public async Task LoadTechnicalInspectionsAsync()
        {
            var loadedTechnicalInspections = await TechnicalInspectionDataService.GetAllAsync();
            TechnicalInspections = new ObservableCollection<TechnicalInspection>(loadedTechnicalInspections);
        }

        public async Task LoadBusesAsync()
        {
            var loadedBuses = await BusDataService.GetAllAsync();
            Buses = new ObservableCollection<Bus>(loadedBuses);
            CurrentBus = Buses[0];
        }

        public async Task CreateTechnicalInspectionAsync()
        {
            await TechnicalInspectionDataService.CreateOneAsync(_technicalInspection);
        }

        public async Task UpdateTechnicalInspectionAsync()
        {
            await TechnicalInspectionDataService.UpdateByIdAsync(_technicalInspection);
        }

        public async Task DeleteTechnicalInspectionAsync()
        {
            await TechnicalInspectionDataService.DeleteByIdAsync(_technicalInspection.Id);
        }
    }
}
