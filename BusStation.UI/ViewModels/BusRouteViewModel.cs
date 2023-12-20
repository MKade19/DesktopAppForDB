using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class BusRouteViewModel : ViewModelBase
    {
        private BusRoute _busRoute = new BusRoute();

        private IBusRouteDataService BusRouteDataService { get; }

        private ObservableCollection<BusRoute> _routes;

        public BusRouteViewModel(IBusRouteDataService busRouteDataService)
        {
            BusRouteDataService = busRouteDataService;
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

        public int Id
        {
            get { return _busRoute.Id; }
            set
            {
                _busRoute.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string RouteNumber
        {
            get { return _busRoute.RouteNumber; }
            set
            {
                _busRoute.RouteNumber = value;
                OnPropertyChanged(nameof(RouteNumber));
            }
        }
        
        public string Departure
        {
            get { return _busRoute.Departure; }
            set
            {
                _busRoute.Departure = value;
                OnPropertyChanged(nameof(Departure));
            }
        }
        
        public string Destination
        {
            get { return _busRoute.Destination; }
            set
            {
                _busRoute.Destination = value;
                OnPropertyChanged(nameof(Destination));
            }
        }

        public int Distance
        {
            get { return _busRoute.Distance; }
            set
            {
                _busRoute.Distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        public void ChangeCurrentRoute(object busRoute)
        {
            BusRoute newBusRoute = (BusRoute)busRoute;
            Id = newBusRoute.Id;
            RouteNumber = newBusRoute.RouteNumber;
            Departure = newBusRoute.Departure;
            Destination = newBusRoute.Destination;
            Distance = newBusRoute.Distance;
        }

        public async Task LoadRoutesAsync()
        {
            var loadedRoutes = await BusRouteDataService.GetAllAsync();
            Routes = new ObservableCollection<BusRoute>(loadedRoutes);
        }

        public async Task CreateRouteAsync()
        {
            await BusRouteDataService.CreateOneAsync(_busRoute);
        }

        public async Task UpdateRouteAsync()
        {
            await BusRouteDataService.UpdateByIdAsync(_busRoute);
        }

        public async Task DeleteRouteAsync()
        {
            await BusRouteDataService.DeleteByIdAsync(_busRoute.Id);
        }
    }
}
