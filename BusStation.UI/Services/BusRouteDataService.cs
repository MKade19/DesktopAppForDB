using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class BusRouteDataService : HttpDataServiceBase, IBusRouteDataService
    {
        public const string BUS_ROUTE_URL = "bus-routes";

        public async Task CreateOneAsync(BusRoute busRoute)
        {
            JsonContent content = JsonContent.Create(busRoute);
            await PostAsync(content, BUS_ROUTE_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(BUS_ROUTE_URL + "/" + id);
        }

        public async Task<IEnumerable<BusRoute>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<BusRoute>>(await GetAsync(BUS_ROUTE_URL)) ?? new List<BusRoute>();
        }

        public async Task<BusRoute> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<BusRoute>(await GetAsync(BUS_ROUTE_URL + "/" + id)) ?? new BusRoute();
        }

        public async Task UpdateByIdAsync(BusRoute busRoute)
        {
            JsonContent content = JsonContent.Create(busRoute);
            await PutAsync(content, BUS_ROUTE_URL);
        }
    }
}
