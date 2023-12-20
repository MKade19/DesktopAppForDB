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
    /// Логика взаимодействия для WorkerView.xaml
    /// </summary>
    public partial class WorkerView : UserControl
    {
        public WorkerView(IWorkerDataService workerDataService, IPositionDataService positionDataService)
        {
            InitializeComponent();
            Loaded += WorkerView_Loaded;
            DataContext = new WorkerViewModel(workerDataService, positionDataService);
        }

        private async void WorkerView_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadWorkersAsync();
            await LoadPositionsAsync();
            SetDatePickerDefault();
        }

        private void SetDatePickerDefault()
        {
            BirthDatePicker.BlackoutDates.Add(
                new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.MaxValue));
        }

        private async Task LoadWorkersAsync()
        {
            await ((WorkerViewModel)DataContext).LoadWorkersAsync();
        }

        private async Task LoadPositionsAsync()
        {
            await ((WorkerViewModel)DataContext).LoadPositionsAsync();
        }

        private void ClearCurrentRecord()
        {
            ((WorkerViewModel)DataContext).Id = -1;
            FullnameBox.Text = string.Empty;
            ((WorkerViewModel)DataContext).BirthDate = DateTime.Today;
            ExperienceBox.Text = "0";
            PositionComboBox.SelectedIndex = 0;
            WorkersGrid.UnselectAll();
        }

        private void ToggleEditCreateForms()
        {
            if (((WorkerViewModel)DataContext).Id != -1)
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

        private void WorkersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((WorkerViewModel)DataContext).ChangeCurrentWorker(e.AddedItems[0]);
            ToggleEditCreateForms();
            FullnameBox.Focus();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((WorkerViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((WorkerViewModel)DataContext).CreateWorkerAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((WorkerViewModel)DataContext).UpdateWorkerAsync();
            }

            await LoadWorkersAsync();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            FullnameBox.Focus();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((WorkerViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((WorkerViewModel)DataContext).DeleteWorkerAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadWorkersAsync();
        }
    }
}
