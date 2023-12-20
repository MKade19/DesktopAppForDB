using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BusStation.UI.Services.Abstract;
using BusStation.UI.Util;
using BusStation.UI.ViewModels;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для BusProducerView.xaml
    /// </summary>
    public partial class BusProducerView : UserControl
    {
        public BusProducerView(IBusProducerDataService busProducerDataService)
        {
            InitializeComponent();
            Loaded += BusProducerView_Loaded;
            DataContext = new BusProducerViewModel(busProducerDataService);
        }

        private async void BusProducerView_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadProducersAsync();
        }
                
        private void ClearCurrentRecord()
        {
            ((BusProducerViewModel)DataContext).Id = -1;
            TitleBox.Text = string.Empty;
            TownBox.Text = string.Empty;
            ProducersGrid.UnselectAll();
        }

        private void ToggleEditCreateForms()
        {
            if (((BusProducerViewModel)DataContext).Id != -1)
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

        private void ProducersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((BusProducerViewModel)DataContext).ChangeCurrentProducer(e.AddedItems[0]);
            ToggleEditCreateForms();
            TitleBox.Focus();
        }

        private async Task LoadProducersAsync()
        {
            await ((BusProducerViewModel)DataContext).LoadProducersAsync();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((BusProducerViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((BusProducerViewModel)DataContext).CreateProducerAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((BusProducerViewModel)DataContext).UpdateProducerAsync();
            }

            await LoadProducersAsync();
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            TitleBox.Focus();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((BusProducerViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((BusProducerViewModel)DataContext).DeleteProducerAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadProducersAsync();
        }
    }
}
