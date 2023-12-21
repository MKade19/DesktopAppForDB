using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        private IBusDataService BusDataService { get; }
        private IRepairmentDataService RepairmentDataService { get; }

        public ChartViewModel(IBusDataService busDataService, IRepairmentDataService repairmentDataService)
        {
            BusDataService=busDataService;
            RepairmentDataService=repairmentDataService;
        }

        public List<BusColorWithCount> BusColorsWithCount { get; set; }
        public List<RepairmentYearWithCount> RepairmentYearsWithCount { get; set; }

        public async Task LoadBusColorsWithCount()
        {
            var loadedColors = await BusDataService.GetColorsWithCountAsync();
            BusColorsWithCount = new List<BusColorWithCount>(loadedColors);
        }

        public async Task LoadRepairmentYearsWithCount()
        {
            var loadedYears = await RepairmentDataService.GetYearsWithCountAsync();
            RepairmentYearsWithCount = new List<RepairmentYearWithCount>(loadedYears);
        }
    }
}
