using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для RepairmentView.xaml
    /// </summary>
    public partial class RepairmentView : UserControl
    {
        public RepairmentView()
        {
            InitializeComponent();
            Loaded += RepairmentView_Loaded;
            DataContext = new RepairmentViewModel();
        }

        private async void RepairmentView_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRepairmentsAsync();
            await LoadWorkersAsync();
            await LoadBusesAsync();
        }

        private async Task LoadRepairmentsAsync()
        {
            await ((RepairmentViewModel)DataContext).LoadRepairmentsAsync();
        }

        private async Task LoadWorkersAsync()
        {
            await ((RepairmentViewModel)DataContext).LoadWorkersAsync();
        }

        private async Task LoadBusesAsync()
        {
            await ((RepairmentViewModel)DataContext).LoadBusesAsync();
        }

        private void ToggleEditCreateForms()
        {
            if (((RepairmentViewModel)DataContext).Id != -1)
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

        private void ClearCurrentRecord()
        {
            ((RepairmentViewModel)DataContext).Id = -1;
            ((RepairmentViewModel)DataContext).BeginDate = DateTime.Today;
            ((RepairmentViewModel)DataContext).EndDate = DateTime.Today;
            WorkerComboBox.SelectedIndex = 0;
            BusComboBox.SelectedIndex = 0;
            MalfunctionTextBox.Text = string.Empty;
            RepairmentsGrid.UnselectAll();
        }

        private void RepairmentsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((RepairmentViewModel)DataContext).ChangeCurrentRepairment(e.AddedItems[0]);
            ToggleEditCreateForms();
            BeginDatePicker.Focus();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            BeginDatePicker.Focus();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((RepairmentViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((RepairmentViewModel)DataContext).DeleteRepairmentAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadRepairmentsAsync();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((RepairmentViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await((RepairmentViewModel)DataContext).CreateRepairmentAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await((RepairmentViewModel)DataContext).UpdateRepairmentAsync();
            }

            await LoadRepairmentsAsync();
        }
    }
}
