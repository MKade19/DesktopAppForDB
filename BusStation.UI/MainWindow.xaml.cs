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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            EventAggregator.Instance.UserAuthorized += Instance_UserAuthorized;
        }

        private void Instance_UserAuthorized(object? sender, System.EventArgs e)
        {
            MainWindowContainer.SelectedIndex = 1;
            LogoutButton.IsEnabled = true;
        }

        private void TablesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch(TablesListBox.SelectedIndex)
            {
                case 0:
                    ViewContainer.Content = new BusProducerView();                    
                    break;
                case 1:
                    ViewContainer.Content = new BusModelView();
                    break; 
                case 2:
                    ViewContainer.Content = new BusView();
                    break;
                case 3:
                    ViewContainer.Content = new BusRouteView();
                    break;
                case 4:
                    ViewContainer.Content = new PositionView();
                    break;
                case 5:
                    ViewContainer.Content = new WorkerView();
                    break;
                case 6:
                    ViewContainer.Content = new MedicalInspectionView();
                    break;
                case 7:
                    ViewContainer.Content = new TechnicalInspectionView();
                    break;
                case 8:
                    ViewContainer.Content = new RepairmentView();
                    break;
                case 9:
                    ViewContainer.Content = new VoyageView();
                    break;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContainer.SelectedIndex = 0;
            Properties.Settings.Default.AccessToken = null;
            LogoutButton.IsEnabled = false;
        }
    }
}
