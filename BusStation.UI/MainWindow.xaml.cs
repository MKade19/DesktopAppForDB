using BusStation.UI.Services.Abstract;
using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using BusStation.UI.Views;
using System.Windows;

namespace BusStation.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBusProducerDataService BusProducerDataService {  get; }
        private IBusModelDataService BusModelDataService {  get; }
        private IBusDataService BusDataService {  get; }
        private IBusRouteDataService BusRouteDataService {  get; }
        private IPositionDataService PositionDataService {  get; }
        private IWorkerDataService WorkerDataService{  get; }
        private IMedicalInspectionDataService MedicalInspectionDataService {  get; }
        private ITechnicalInspectionDataService TechnicalInspectionDataService {  get; }
        private IRepairmentDataService RepairmentDataService{  get; }
        private IVoyageDataService VoyageDataService {  get; }
        private IAuthDataService AuthDataService {  get; }

        public MainWindow(
            IBusProducerDataService busProducerDataService, 
            IBusModelDataService busModelDataService, 
            IBusDataService busDataService, 
            IBusRouteDataService busRouteDataService, 
            IPositionDataService positionDataService,
            IWorkerDataService workerDataService,
            IMedicalInspectionDataService medicalInspectionDataService,
            ITechnicalInspectionDataService technicalInspectionDataService,
            IRepairmentDataService repairmentDataService,
            IVoyageDataService voyageDataService,
            IAuthDataService authDataService
        )
        {
            InitializeComponent();
            DataContext = new MainViewModel(busModelDataService, technicalInspectionDataService, voyageDataService);
            Loaded += MainWindow_Loaded;
            EventAggregator.Instance.UserAuthorized += Instance_UserAuthorized;

            BusProducerDataService = busProducerDataService;
            BusModelDataService = busModelDataService;
            BusDataService = busDataService;
            BusRouteDataService = busRouteDataService;
            PositionDataService = positionDataService;
            WorkerDataService = workerDataService;
            MedicalInspectionDataService = medicalInspectionDataService;
            TechnicalInspectionDataService = technicalInspectionDataService;
            RepairmentDataService = repairmentDataService;
            VoyageDataService = voyageDataService;
            AuthDataService = authDataService;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTab.Content = new LoginView(AuthDataService);
        }

        private void Instance_UserAuthorized(object? sender, System.EventArgs e)
        {
            MainWindowContainer.SelectedIndex = 1;
        }

        private void TablesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch(TablesListBox.SelectedIndex)
            {
                case 0:
                    ViewContainer.Content = new BusProducerView(BusProducerDataService);                    
                    break;
                case 1:
                    ViewContainer.Content = new BusModelView(BusModelDataService, BusProducerDataService);
                    break; 
                case 2:
                    ViewContainer.Content = new BusView(BusDataService, BusModelDataService);
                    break;
                case 3:
                    ViewContainer.Content = new BusRouteView(BusRouteDataService);
                    break;
                case 4:
                    ViewContainer.Content = new PositionView(PositionDataService);
                    break;
                case 5:
                    ViewContainer.Content = new WorkerView(WorkerDataService, PositionDataService);
                    break;
                case 6:
                    ViewContainer.Content = new MedicalInspectionView(MedicalInspectionDataService, WorkerDataService);
                    break;
                case 7:
                    ViewContainer.Content = new TechnicalInspectionView(TechnicalInspectionDataService, BusDataService);
                    break;
                case 8:
                    ViewContainer.Content = new RepairmentView(RepairmentDataService, BusDataService, WorkerDataService);
                    break;
                case 9:
                    ViewContainer.Content = new VoyageView(VoyageDataService, BusRouteDataService, WorkerDataService, BusDataService);
                    break;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContainer.SelectedIndex = 0;
            Properties.Settings.Default.AccessToken = null;
            EventAggregator.Instance.RaiseUserUnauthorizedEvent();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContainer.SelectedIndex = 2;
        }

        private void BackToTablesButtnon_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContainer.SelectedIndex = 1;
        }

        private async void OpenReportButnon_Click(object sender, RoutedEventArgs e)
        {
            if (ReportsListBox.SelectedIndex == -1)
            {
                MessageBoxStore.Warning("Отчёт не выбран!");
                return;
            }

            await ((MainViewModel)DataContext).MakeReportAsync(ReportsListBox.SelectedIndex);
        }

        private void ReportsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ReportsListBox.SelectedIndex == 1)
            {
                ReportForm.Visibility = Visibility.Visible;
            }
            else
            {
                ReportForm.Visibility = Visibility.Collapsed;
            }
        }
    }
}
