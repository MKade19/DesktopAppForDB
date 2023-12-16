using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BusStation.UI.ViewModels;
using BusStation.UI.Util;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для BusView.xaml
    /// </summary>
    public partial class BusView : UserControl
    {
        public BusView()
        {
            InitializeComponent();
            Loaded += BusView_Loaded;
            DataContext = new BusViewModel();
        }

        private async void BusView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadBusesAsync();
            await LoadModelsAsync();
        }

        private void ToggleEditCreateForms()
        {
            if (((BusViewModel)DataContext).Id != -1)
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

        private async Task LoadBusesAsync()
        {
            await ((BusViewModel)DataContext).LoadBusesAsync();
        }

        private async Task LoadModelsAsync()
        {
            await ((BusViewModel)DataContext).LoadModelsAsync();
        }

        private void ClearCurrentRecord()
        {
            ((BusViewModel)DataContext).Id = -1;
            StateNumberBox.Text = string.Empty;
            ((BusViewModel)DataContext).DeliveryDate = DateTime.Today;
            ColorBox.Text = string.Empty;
            GarageNumberBox.Text = "0";
            ModelsComboBox.SelectedIndex = 0;
            BusesGrid.UnselectAll();
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            StateNumberBox.Focus();
        }

        private async void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (((BusViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((BusViewModel)DataContext).DeleteBusAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadBusesAsync();
        }

        private async void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (((BusViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await((BusViewModel)DataContext).CreateBusAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await((BusViewModel)DataContext).UpdateBusAsync();
            }

            await LoadBusesAsync();
        }

        private void BusesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((BusViewModel)DataContext).ChangeCurrentBus(e.AddedItems[0]);
            ToggleEditCreateForms();
            StateNumberBox.Focus();
        }
    }
}
