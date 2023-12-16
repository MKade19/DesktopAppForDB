using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для MedicalInspectionView.xaml
    /// </summary>
    public partial class MedicalInspectionView : UserControl
    {
        public MedicalInspectionView()
        {
            InitializeComponent();
            Loaded += MedicalInspectionView_Loaded;
            DataContext = new MedicalInspectionViewModel();
        }

        private async void MedicalInspectionView_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadMedicalInspectionsAsync();
            await LoadWorkersAsync();
        }

        private async Task LoadMedicalInspectionsAsync()
        {
            await ((MedicalInspectionViewModel)DataContext).LoadMedicalInspectionsAsync();

        }

        private async Task LoadWorkersAsync()
        {
            await ((MedicalInspectionViewModel)DataContext).LoadWorkersAsync();
        }

        private void ToggleEditCreateForms()
        {
            if (((MedicalInspectionViewModel)DataContext).Id != -1)
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
            ((MedicalInspectionViewModel)DataContext).Id = -1;
            ((MedicalInspectionViewModel)DataContext).InspectionDate = DateTime.Today;
            WorkerComboBox.SelectedIndex = 0;
            IsAllowedCheckBox.IsChecked = true;
            DenialReasonTextBox.Text = string.Empty;
            MedicalInspectionsGrid.UnselectAll();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((MedicalInspectionViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((MedicalInspectionViewModel)DataContext).CreateMedicalInspectionAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((MedicalInspectionViewModel)DataContext).UpdateMedicalInspectionAsync();
            }

            await LoadMedicalInspectionsAsync();
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
            if (((MedicalInspectionViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((MedicalInspectionViewModel)DataContext).DeleteMedicalInspectionAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadMedicalInspectionsAsync();
        }

        private void MedicalInspectionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((MedicalInspectionViewModel)DataContext).ChangeCurrentMedicalInspection(e.AddedItems[0]);
            ToggleEditCreateForms();
            InspectionDatePicker.Focus();
        }
    }
}
