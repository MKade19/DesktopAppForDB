using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class RepairmentViewModel : ViewModelBase
    {
        private Repairment _repairment = new Repairment();
        private IRepairmentDataService RepairmentDataService { get; }
        private IWorkerDataService WorkerDataService {  get; }
        private IBusDataService BusDataService {  get; }
        private ObservableCollection<Repairment> _repairments;
        private ObservableCollection<Worker> _workers;
        private ObservableCollection<Bus> _buses;
        private Worker _currentWorker;
        private Bus _currentBus;

        public RepairmentViewModel(IRepairmentDataService repairmentDataService, IBusDataService busDataService, IWorkerDataService workerDataService)
        {
            RepairmentDataService = repairmentDataService;
            BusDataService = busDataService;
            WorkerDataService = workerDataService;
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

        public ObservableCollection<Repairment> Repairments
        {
            get => _repairments;
            set
            {
                _repairments = value;
                OnPropertyChanged(nameof(Repairments));
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
            get { return _repairment.Id; }
            set
            {
                _repairment.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public DateTime BeginDate
        {
            get { return _repairment.BeginDate; }
            set
            {
                _repairment.BeginDate = value;
                OnPropertyChanged(nameof(BeginDate));
            }
        }
        
        public DateTime EndDate
        {
            get { return _repairment.EndDate; }
            set
            {
                _repairment.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public int WorkerId
        {
            get { return _repairment.WorkerId; }
            set
            {
                _repairment.WorkerId = value;
                OnPropertyChanged(nameof(WorkerId));
            }
        }
        
        public int BusId
        {
            get { return _repairment.BusId; }
            set
            {
                _repairment.BusId = value;
                OnPropertyChanged(nameof(BusId));
            }
        }

        public string Malfunction
        {
            get { return _repairment.Malfunction; }
            set
            {
                _repairment.Malfunction = value;
                OnPropertyChanged(nameof(Malfunction));
            }
        }

        public void ChangeCurrentRepairment(object repairment)
        {
            Repairment newRepairment = (Repairment)repairment;
            Id = newRepairment.Id;
            BeginDate = newRepairment.BeginDate;
            EndDate = newRepairment.EndDate;
            WorkerId = newRepairment.WorkerId;
            BusId = newRepairment.BusId;
            Malfunction = newRepairment.Malfunction;
            CurrentWorker = Workers.First(w => w.Id == newRepairment.WorkerId);
            CurrentBus = Buses.First(b => b.Id == newRepairment.BusId);
        }

        public async Task LoadRepairmentsAsync()
        {
            var loadedRepairments = await RepairmentDataService.GetAllAsync();
            Repairments = new ObservableCollection<Repairment>(loadedRepairments);
        }

        public async Task LoadWorkersAsync()
        {
            var loadedWorkers = await WorkerDataService.GetMechanicsAsync();
            Workers = new ObservableCollection<Worker>(loadedWorkers);
            CurrentWorker = Workers[0];
        }
        
        public async Task LoadBusesAsync()
        {
            var loadedBuses = await BusDataService.GetAllAsync();
            Buses = new ObservableCollection<Bus>(loadedBuses);
            CurrentBus = Buses[0];
        }

        public async Task CreateRepairmentAsync()
        {
            await RepairmentDataService.CreateOneAsync(_repairment);
        }

        public async Task UpdateRepairmentAsync()
        {
            await RepairmentDataService.UpdateByIdAsync(_repairment);
        }

        public async Task DeleteRepairmentAsync()
        {
            await RepairmentDataService.DeleteByIdAsync(_repairment.Id);
        }
    }
}
