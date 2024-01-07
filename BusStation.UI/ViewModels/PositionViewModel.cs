using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class PositionViewModel : ViewModelBase
    {
        private Position _position = new Position();
        private IPositionDataService PositionDataService { get; }

        private ObservableCollection<Position> _positions;

        public PositionViewModel(IPositionDataService positionDataService)
        {
            PositionDataService = positionDataService;
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
            get { return _position.Id; }
            set
            {
                _position.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get { return _position.Title; }
            set
            {
                _position.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public decimal Salary
        {
            get { return _position.Salary; }
            set
            {
                _position.Salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        public void ChangeCurrentPosition(object position)
        {
            Position newPosition = (Position)position;
            Id = newPosition.Id;
            Title = newPosition.Title;
            Salary = newPosition.Salary;
        }

        public async Task LoadPositionsAsync()
        {
            var loadedPositions = await PositionDataService.GetAllAsync();
            Positions = new ObservableCollection<Position>(loadedPositions);
        }

        public async Task CreatePositionAsync()
        {
            await PositionDataService.CreateOneAsync(_position);
        }

        public async Task UpdatePositionAsync()
        {
            await PositionDataService.UpdateByIdAsync(_position);
        }

        public async Task DeletePositionAsync()
        {
            await PositionDataService.DeleteByIdAsync(_position.Id);
        }
    }
}
