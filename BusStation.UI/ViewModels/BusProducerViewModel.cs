using BusStation.Common.Models;
using BusStation.UI.Services;
using BusStation.UI.Services.Abstract;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class BusProducerViewModel : ViewModelBase
    {
        private BusProducer _busProducer = new BusProducer();
        private IBusProducerDataService BusProducerDataService { get; } = new BusProducerDataService();

        private ObservableCollection<BusProducer> _producers;

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
            get { return _busProducer.Id; }
            set
            {
                _busProducer.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get { return _busProducer.Title; }
            set
            {
                _busProducer.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Town
        {
            get { return _busProducer.Town; }
            set
            {
                _busProducer.Town = value;
                OnPropertyChanged(nameof(Town));
            }
        }

        public void ChangeCurrentProducer(object busProducer)
        {
            BusProducer newBusProducer = (BusProducer)busProducer;
            Id = newBusProducer.Id;
            Title = newBusProducer.Title;
            Town = newBusProducer.Town;
        }

        public async Task LoadProducersAsync()
        {
            var loadedProducers = await BusProducerDataService.GetAllAsync();
            Producers = new ObservableCollection<BusProducer>(loadedProducers);
        }

        public async Task CreateProducerAsync()
        {
            await BusProducerDataService.CreateOneAsync(_busProducer);
        }

        public async Task UpdateProducerAsync()
        {
            await BusProducerDataService.UpdateByIdAsync(_busProducer);
        }

        public async Task DeleteProducerAsync()
        {
            await BusProducerDataService.DeleteByIdAsync(_busProducer.Id);
        }
    }
}
