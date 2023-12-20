using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class BusViewModel : ViewModelBase
    {
        private Bus _bus = new Bus();
        private IBusModelDataService BusModelDataService { get; }
        private IBusDataService BusDataService { get; }
        private ObservableCollection<Bus> _buses;
        private ObservableCollection<BusModel> _models;
        private BusModel _currentBusModel;

        public BusViewModel(IBusDataService busDataService, IBusModelDataService busModelDataService)
        {
            BusModelDataService = busModelDataService;
            BusDataService = busDataService;
        }

        public BusModel CurrentBusModel
        {
            get => _currentBusModel;
            set
            {
                _currentBusModel = value;
                ModelId = _currentBusModel.Id;
                OnPropertyChanged(nameof(CurrentBusModel));
            }
        }

        public ObservableCollection<BusModel> Models
        {
            get => _models;
            set
            {
                _models = value;
                OnPropertyChanged(nameof(Models));
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
            get { return _bus.Id; }
            set
            {
                _bus.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string StateNumber
        {
            get { return _bus.StateNumber; }
            set
            {
                _bus.StateNumber = value;
                OnPropertyChanged(nameof(StateNumber));
            }
        }
        
        public DateTime DeliveryDate
        {
            get { return _bus.DeliveryDate; }
            set
            {
                _bus.DeliveryDate = value;
                OnPropertyChanged(nameof(DeliveryDate));
            }
        }

        public string Color
        {
            get { return _bus.Color; }
            set
            {
                _bus.Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public int GarageNumber
        {
            get { return _bus.GarageNumber; }
            set
            {
                _bus.GarageNumber = value;
                OnPropertyChanged(nameof(GarageNumber));
            }
        }

        public int ModelId
        {
            get { return _bus.BusModelId; }
            set
            {
                _bus.BusModelId = value;
                OnPropertyChanged(nameof(ModelId));
            }
        }

        public void ChangeCurrentBus(object bus)
        {
            Bus newBus = (Bus)bus;
            Id = newBus.Id;
            StateNumber = newBus.StateNumber;
            DeliveryDate = newBus.DeliveryDate;
            Color = newBus.Color;
            GarageNumber = newBus.GarageNumber;
            ModelId = newBus.BusModelId;
            CurrentBusModel = Models.First(m => m.Id == newBus.BusModelId);
        }

        public void ChangeCurrentModel(object busModel)
        {
            CurrentBusModel = (BusModel)busModel;
        }
        public async Task LoadBusesAsync()
        {
            var loadedBuses = await BusDataService.GetAllAsync();
            Buses = new ObservableCollection<Bus>(loadedBuses);
        }

        public async Task LoadModelsAsync()
        {
            var loadedModels = await BusModelDataService.GetAllAsync();
            Models = new ObservableCollection<BusModel>(loadedModels);
            CurrentBusModel = Models[0];
        }

        public async Task CreateBusAsync()
        {
            await BusDataService.CreateOneAsync(_bus);
        }

        public async Task UpdateBusAsync()
        {
            await BusDataService.UpdateByIdAsync(_bus);
        }

        public async Task DeleteBusAsync()
        {
            await BusDataService.DeleteByIdAsync(_bus.Id);
        }
    }
}
