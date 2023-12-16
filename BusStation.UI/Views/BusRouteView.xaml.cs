using BusStation.UI.Services;
using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для BusRouteView.xaml
    /// </summary>
    public partial class BusRouteView : UserControl
    {
        public BusRouteView()
        {
            InitializeComponent();
            Loaded += BusRouteView_Loaded;
            DataContext = new BusRouteViewModel();
        }

        private async void BusRouteView_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRoutesAsync();
        }

        private async Task LoadRoutesAsync()
        {
            await ((BusRouteViewModel)DataContext).LoadRoutesAsync();
        }

        private void ClearCurrentRecord()
        {
            ((BusRouteViewModel)DataContext).Id = -1;
            RouteNumberBox.Text = string.Empty;
            DepartureBox.Text = string.Empty;
            DestinationBox.Text = string.Empty;
            DistanceBox.Text = string.Empty;
            RoutesGrid.UnselectAll();
        }

        private void ToggleEditCreateForms()
        {
            if (((BusRouteViewModel)DataContext).Id != -1)
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

        private void RoutesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((BusRouteViewModel)DataContext).ChangeCurrentRoute(e.AddedItems[0]);
            ToggleEditCreateForms();
            RouteNumberBox.Focus();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((BusRouteViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((BusRouteViewModel)DataContext).CreateRouteAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((BusRouteViewModel)DataContext).UpdateRouteAsync();
            }

            await LoadRoutesAsync();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            RouteNumberBox.Focus();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((BusRouteViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((BusRouteViewModel)DataContext).DeleteRouteAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadRoutesAsync();
        }
    }
}
