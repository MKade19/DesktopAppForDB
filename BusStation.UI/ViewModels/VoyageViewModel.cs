using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class VoyageViewModel : ViewModelBase
    {
        private Voyage _voyage = new Voyage();
        private IVoyageDataService VoyageDataService { get; }
        private IBusDataService BusDataService { get; }
        private IWorkerDataService WorkerDataService { get; }
        private IBusRouteDataService BusRouteDataService { get; }

        private ObservableCollection<Voyage> _voyages;
        private ObservableCollection<Worker> _workers;
        private ObservableCollection<BusRoute> _routes;
        private ObservableCollection<Bus> _buses;
        private BusRoute _currentBusRoute;
        private Worker _currentWorker;
        private Bus _currentBus;
        private BusRoute _currentBusRouteForFilter;

        public VoyageViewModel(IVoyageDataService voyageDataService, IBusRouteDataService busRouteDataService, IWorkerDataService workerDataService, IBusDataService busDataService)
        {
            VoyageDataService = voyageDataService;
            BusDataService = busDataService;
            WorkerDataService = workerDataService;
            BusRouteDataService = busRouteDataService;
        }

        public BusRoute CurrentBusRoute
        {
            get => _currentBusRoute;
            set
            {
                _currentBusRoute = value;
                RouteId = CurrentBusRoute.Id;
                OnPropertyChanged(nameof(CurrentBusRoute));
            }
        }

        public Worker CurrentWorker
        {
            get => _currentWorker;
            set
            {
                _currentWorker = value;
                WorkerId = CurrentWorker.Id;
                OnPropertyChanged(nameof(CurrentWorker));
            }
        }

        public Bus CurrentBus
        {
            get => _currentBus;
            set
            {
                _currentBus = value;
                BusId = CurrentBus.Id;
                OnPropertyChanged(nameof(CurrentBus));
            }
        }

        public ObservableCollection<Voyage> Voyages
        {
            get => _voyages;
            set
            {
                _voyages = value;
                OnPropertyChanged(nameof(Voyages));
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

        public ObservableCollection<BusRoute> Routes
        {
            get => _routes;
            set
            {
                _routes = value;
                OnPropertyChanged(nameof(Routes));
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
            get { return _voyage.Id; }
            set
            {
                _voyage.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public DateTime VoyageDate
        {
            get { return _voyage.VoyageDate; }
            set
            {
                _voyage.VoyageDate = value;
                OnPropertyChanged(nameof(VoyageDate));
            }
        }
        
        public DateTime DepartureTime
        {
            get { return _voyage.DepartureTime; }
            set
            {
                _voyage.DepartureTime = value;
                OnPropertyChanged(nameof(DepartureTime));
            }
        }
        
        public DateTime ArrivalTime
        {
            get { return _voyage.ArrivalTime; }
            set
            {
                _voyage.ArrivalTime = value;
                OnPropertyChanged(nameof(ArrivalTime));
            }
        }

        public int RouteId
        {
            get { return _voyage.BusRouteId; }
            set
            {
                _voyage.BusRouteId = value;
                OnPropertyChanged(nameof(RouteId));
            }
        }

        public int WorkerId
        {
            get { return _voyage.WorkerId; }
            set
            {
                _voyage.WorkerId = value;
                OnPropertyChanged(nameof(WorkerId));
            }
        }

        public int BusId
        {
            get { return _voyage.BusId; }
            set
            {
                _voyage.BusId = value;
                OnPropertyChanged(nameof(BusId));
            }
        }

        public BusRoute CurrentBusRouteForFilter
        {
            get { return _currentBusRouteForFilter; }
            set
            {
                _currentBusRouteForFilter = value;
                OnPropertyChanged(nameof(CurrentBusRouteForFilter));
            }
        }

        public void ChangeCurrentVoyage(object voyage)
        {
            Voyage newVoyage = (Voyage)voyage;
            Id = newVoyage.Id;
            VoyageDate = newVoyage.VoyageDate;
            DepartureTime = newVoyage.DepartureTime;
            ArrivalTime = newVoyage.ArrivalTime;
            RouteId = newVoyage.BusRouteId;
            WorkerId = newVoyage.WorkerId;
            BusId = newVoyage.BusId;
            CurrentBusRoute = Routes.First(p => p.Id == newVoyage.BusRouteId);
            CurrentWorker = Workers.First(p => p.Id == newVoyage.WorkerId);
            CurrentBus = Buses.First(p => p.Id == newVoyage.BusId);
        }

        public async Task LoadVoyagesAsync()
        {
            var loadedVoyages = await VoyageDataService.GetAllAsync();
            Voyages = new ObservableCollection<Voyage>(loadedVoyages);
        }

        public async Task LoadVoyagesByRouteNumberAsync()
        {
            var loadedVoyages = await VoyageDataService.GetByRouteNumberAsync(CurrentBusRouteForFilter.RouteNumber);
            Voyages = new ObservableCollection<Voyage>(loadedVoyages);
        }

        public async Task LoadRoutesAsync()
        {
            var loadedRoutes = await BusRouteDataService.GetAllAsync();
            Routes = new ObservableCollection<BusRoute>(loadedRoutes);
            CurrentBusRoute = Routes[0];
        }

        public async Task LoadWorkersAsync()
        {
            var loadedWorkers = await WorkerDataService.GetDriversAsync();
            Workers = new ObservableCollection<Worker>(loadedWorkers);
            CurrentWorker = Workers[0];
        }

        public async Task LoadBusesAsync()
        {
            var loadedBuses = await BusDataService.GetAllAsync();
            Buses = new ObservableCollection<Bus>(loadedBuses);
            CurrentBus = Buses[0];
        }

        public async Task CreateVoyageAsync()
        {
            await VoyageDataService.CreateOneAsync(_voyage);
        }

        public async Task UpdateVoyageAsync()
        {
            await VoyageDataService.UpdateByIdAsync(_voyage);
        }

        public async Task DeleteVoyageAsync()
        {
            await VoyageDataService.DeleteByIdAsync(_voyage.Id);
        }
    }
}
