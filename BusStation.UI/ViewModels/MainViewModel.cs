using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using BusStation.UI.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IBusModelDataService BusModelDataService { get; }
        private ITechnicalInspectionDataService TechnicalInspectionDataService { get; }
        private IVoyageDataService VoyageDataService { get; }
        private int _numberBoxValue { get; set; }
        private bool _checkBoxValue { get; set; }

        public MainViewModel(IBusModelDataService busModelDataService, ITechnicalInspectionDataService technicalInspectionDataService, IVoyageDataService voyageDataService) 
        {
            BusModelDataService = busModelDataService;
            TechnicalInspectionDataService = technicalInspectionDataService;
            VoyageDataService = voyageDataService;
        }

        public int NumberBoxValue
        {
            get => _numberBoxValue;
            set
            {
                _numberBoxValue = value;
                OnPropertyChanged(nameof(NumberBoxValue));
            }
        }

        public bool CheckBoxValue
        {
            get => _checkBoxValue;
            set
            {
                _checkBoxValue = value;
                OnPropertyChanged(nameof(CheckBoxValue));
            }
        }

        public List<BusModelWithDistance> BusModelsWithDistance { get; set; }

        public List<TechnicalInspection> TechnicalInspectionsByYearAndAllowance { get; set; }

        public List<Voyage> Voyages { get; set; }

        private async Task LoadFirstReportDataAsync()
        {
            var loadedModels = await BusModelDataService.GetWithTotalDistanceAsync();
            BusModelsWithDistance = new List<BusModelWithDistance>(loadedModels);
        }

        private async Task LoadSecondReportDataAsync()
        {
            var loadedTechnicalInspections = await TechnicalInspectionDataService.GetByYearAndAllowanceAsync(NumberBoxValue, CheckBoxValue);
            TechnicalInspectionsByYearAndAllowance = new List<TechnicalInspection>(loadedTechnicalInspections);
        }
        
        private async Task LoadThirdReportDataAsync()
        {
            var loadedVoyages = await VoyageDataService.GetAllAsync();
            Voyages = new List<Voyage>(loadedVoyages);
        }

        public async Task MakeReportAsync(int reportIndex)
        {
            switch(reportIndex)
            {
                case 0:
                    await LoadFirstReportDataAsync();
                    ReportsStore.ModelsWithDistance(BusModelsWithDistance);
                    break;
                case 1:
                    await LoadSecondReportDataAsync();
                    ReportsStore.TechnicalInspectionsByYearAndAllowance(TechnicalInspectionsByYearAndAllowance, NumberBoxValue, CheckBoxValue);
                    break;
                case 2:
                    await LoadThirdReportDataAsync();
                    ReportsStore.VoyagesInfo(Voyages);
                    break;
            }
        }
    }
}
