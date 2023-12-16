using BusStation.Common.Models;
using BusStation.UI.Services;
using BusStation.UI.Services.Abstract;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class BusModelViewModel : ViewModelBase
    {
        private BusModel _busModel = new BusModel();
        private IBusModelDataService BusModelDataService { get; } = new BusModelDataService();
        private IBusProducerDataService BusProducerDataService { get; } = new BusProducerDataService();
        
        private ObservableCollection<BusModel> _models;
        private ObservableCollection<BusProducer> _producers;
        private BusProducer _currentBusProducer;
        public BusProducer CurrentBusProducer 
        {
            get => _currentBusProducer;
            set
            {
                _currentBusProducer = value;
                ProducerId = CurrentBusProducer.Id;
                OnPropertyChanged(nameof(CurrentBusProducer));
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

        public ObservableCollection<BusProducer> Producers
        {
            get => _producers;
            set
            {
                _producers = value;
                OnPropertyChanged(nameof(Producers));
            }
        }

        public int Id
        {
            get { return _busModel.Id; }
            set
            {
                _busModel.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get { return _busModel.Title; }
            set
            {
                _busModel.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public int ProducerId
        {
            get { return _busModel.ProducerId; }
            set
            {
                _busModel.ProducerId = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public void ChangeCurrentModel(object busModel)
        {
            BusModel newBusModel = (BusModel)busModel;
            Id = newBusModel.Id;
            Title = newBusModel.Title;
            CurrentBusProducer = Producers.First(p => p.Id == newBusModel.ProducerId);
        }

        public async Task LoadModelsAsync()
        {
            var loadedModels = await BusModelDataService.GetAllAsync();
            Models = new ObservableCollection<BusModel>(loadedModels);
        }

        public async Task LoadProducersAsync()
        {
            var loadedProducers = await BusProducerDataService.GetAllAsync();
            Producers = new ObservableCollection<BusProducer>(loadedProducers);
            CurrentBusProducer = Producers[0];
        }

        public async Task CreateModelAsync()
        {
            await BusModelDataService.CreateOneAsync(_busModel);
        }

        public async Task UpdateModelAsync()
        {
            await BusModelDataService.UpdateByIdAsync(_busModel);
        }

        public async Task DeleteModelAsync()
        {
            await BusModelDataService.DeleteByIdAsync(_busModel.Id);
        }
    }
}
