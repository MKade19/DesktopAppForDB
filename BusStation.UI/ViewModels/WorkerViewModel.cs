using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class WorkerViewModel : ViewModelBase
    {
        private Worker _worker = new Worker();

        private IWorkerDataService WorkerDataService { get; }
        private IPositionDataService PositionDataService { get; }

        private ObservableCollection<Worker> _workers;
        private ObservableCollection<Position> _positions;
        private Position _currentPosition;

        public WorkerViewModel(IWorkerDataService workerDataService, IPositionDataService positionDataService)
        {
            WorkerDataService = workerDataService;
            PositionDataService = positionDataService;
        }

        public Position CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
                PositionId = CurrentPosition.Id;
                OnPropertyChanged(nameof(CurrentPosition));
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

        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set
            {
                _positions = value;
                OnPropertyChanged(nameof(Positions));
            }
        }

        public int Id
        {
            get { return _worker.Id; }
            set
            {
                _worker.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Fullname
        {
            get { return _worker.Fullname; }
            set
            {
                _worker.Fullname = value;
                OnPropertyChanged(nameof(Fullname));
            }
        }

        public DateTime BirthDate
        {
            get { return _worker.BirthDate; }
            set
            {
                _worker.BirthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public int Experience
        {
            get { return _worker.Experience; }
            set
            {
                _worker.Experience = value;
                OnPropertyChanged(nameof(Experience));
            }
        }

        public int PositionId
        {
            get { return _worker.PositionId; }
            set
            {
                _worker.PositionId = value;
                OnPropertyChanged(nameof(PositionId));
            }
        }

        public void ChangeCurrentWorker(object worker)
        {
            Worker newWorker = (Worker)worker;
            Id = newWorker.Id;
            Fullname = newWorker.Fullname;
            BirthDate = newWorker.BirthDate;
            Experience = newWorker.Experience;
            CurrentPosition = Positions.First(p => p.Id == newWorker.PositionId);
        }

        public async Task LoadWorkersAsync()
        {
            var loadedWorkers = await WorkerDataService.GetAllAsync();
            Workers = new ObservableCollection<Worker>(loadedWorkers);
        }

        public async Task LoadPositionsAsync()
        {
            var loadedPositions = await PositionDataService.GetAllAsync();
            Positions = new ObservableCollection<Position>(loadedPositions);
            CurrentPosition = Positions[0];
        }

        public async Task CreateWorkerAsync()
        {
            await WorkerDataService.CreateOneAsync(_worker);
        }

        public async Task UpdateWorkerAsync()
        {
            await WorkerDataService.UpdateByIdAsync(_worker);
        }

        public async Task DeleteWorkerAsync()
        {
            await WorkerDataService.DeleteByIdAsync(_worker.Id);
        }
    }
}
