using BusStation.UI.Services.Abstract;
using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для VoyageView.xaml
    /// </summary>
    public partial class VoyageView : UserControl
    {
        public VoyageView(IVoyageDataService voyageDataService, IBusRouteDataService busRouteDataService, IWorkerDataService workerDataService, IBusDataService busDataService)
        {
            InitializeComponent();
            Loaded += VoyageView_Loaded;
            DataContext = new VoyageViewModel(voyageDataService, busRouteDataService, workerDataService, busDataService);
        }

        private async void VoyageView_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadVoyagesAsync();
            await LoadBusesAsync();
            await LoadWorkersAsync();
            await LoadRoutesAsync();
        }

        private async Task LoadVoyagesAsync()
        {
            await ((VoyageViewModel)DataContext).LoadVoyagesAsync();
        }

        private async Task LoadBusesAsync()
        {
            await ((VoyageViewModel)DataContext).LoadBusesAsync();
        }

        private async Task LoadWorkersAsync()
        {
            await ((VoyageViewModel)DataContext).LoadWorkersAsync();
        }
        
        private async Task LoadRoutesAsync()
        {
            await ((VoyageViewModel)DataContext).LoadRoutesAsync();
        }

        private void ClearCurrentRecord()
        {
            ((VoyageViewModel)DataContext).Id = -1;
            ((VoyageViewModel)DataContext).VoyageDate = DateTime.Today;
            ((VoyageViewModel)DataContext).DepartureTime = DateTime.Today;
            ((VoyageViewModel)DataContext).ArrivalTime = DateTime.Today;
            RouteComboBox.SelectedIndex = 0;
            WorkerComboBox.SelectedIndex = 0;
            BusComboBox.SelectedIndex = 0;
            VoyagesGrid.UnselectAll();
        }

        private void ToggleEditCreateForms()
        {
            if (((VoyageViewModel)DataContext).Id != -1)
            {
                CreateLable.Visibility = Visibility.Collapsed;
                EditLable.Visibility = Visibility.Visible;
            }
            else
            {
                CreateLable.Visibility = Visibility.Visible;
                EditLable.Visibility = Visibility.Collapsed;
            }
        }

        private void VoyageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((VoyageViewModel)DataContext).ChangeCurrentVoyage(e.AddedItems[0]);
            ToggleEditCreateForms();
            VoyageDatePicker.Focus();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((VoyageViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((VoyageViewModel)DataContext).CreateVoyageAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await((VoyageViewModel)DataContext).UpdateVoyageAsync();
            }

            await LoadVoyagesAsync();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((VoyageViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((VoyageViewModel)DataContext).DeleteVoyageAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadVoyagesAsync();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            VoyageDatePicker.Focus();
        }

        private async void RouteFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RouteFilterComboBox.SelectedIndex == -1) 
            {
                return;
            }

            await ((VoyageViewModel)DataContext).LoadVoyagesByRouteNumberAsync();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RouteFilterComboBox.SelectedIndex = -1;
            await LoadVoyagesAsync();
        }
    }
}
