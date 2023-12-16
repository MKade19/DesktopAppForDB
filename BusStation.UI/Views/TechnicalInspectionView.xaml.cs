using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для TechnicalInspectionView.xaml
    /// </summary>
    public partial class TechnicalInspectionView : UserControl
    {
        public TechnicalInspectionView()
        {
            InitializeComponent();
            Loaded += TechnicalInspectionView_Loaded;
            DataContext = new TechnicalInspectionViewModel();
        }

        private async void TechnicalInspectionView_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTechnicalInspectionsAsync();
            await LoadBusesAsync();
        }

        private async Task LoadTechnicalInspectionsAsync()
        {
            await ((TechnicalInspectionViewModel)DataContext).LoadTechnicalInspectionsAsync();

        }

        private async Task LoadBusesAsync()
        {
            await ((TechnicalInspectionViewModel)DataContext).LoadBusesAsync();
        }

        private void ToggleEditCreateForms()
        {
            if (((TechnicalInspectionViewModel)DataContext).Id != -1)
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
            ((TechnicalInspectionViewModel)DataContext).Id = -1;
            ((TechnicalInspectionViewModel)DataContext).InspectionDate = DateTime.Today;
            BusComboBox.SelectedIndex = 0;
            IsAllowedCheckBox.IsChecked = true;
            DenialReasonTextBox.Text = string.Empty;
            TechnicalInspectionsGrid.UnselectAll();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            InspectionDatePicker.Focus();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((TechnicalInspectionViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((TechnicalInspectionViewModel)DataContext).DeleteTechnicalInspectionAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadTechnicalInspectionsAsync();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((TechnicalInspectionViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((TechnicalInspectionViewModel)DataContext).CreateTechnicalInspectionAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((TechnicalInspectionViewModel)DataContext).UpdateTechnicalInspectionAsync();
            }

            await LoadTechnicalInspectionsAsync();
        }

        private void TechnicalInspectionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((TechnicalInspectionViewModel)DataContext).ChangeCurrentTechnicalInspection(e.AddedItems[0]);
            ToggleEditCreateForms();
            InspectionDatePicker.Focus();
        }
    }
}
