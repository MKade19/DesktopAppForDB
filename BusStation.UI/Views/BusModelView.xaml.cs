using System.Threading.Tasks;
using System.Windows.Controls;
using BusStation.UI.ViewModels;
using System.Windows;
using BusStation.UI.Util;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для BusModelView.xaml
    /// </summary>
    public partial class BusModelView : UserControl
    {
        public BusModelView()
        {
            InitializeComponent();
            Loaded += BusModelView_Loaded;
            DataContext = new BusModelViewModel();
        }

        private async void BusModelView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadModelsAsync();
            await LoadProducersAsync();
        }

        private void ClearCurrentRecord()
        { 
            ((BusModelViewModel)DataContext).Id = -1;
            TitleBox.Text = string.Empty;
            ProducerComboBox.SelectedIndex = 0;
            ModelsGrid.UnselectAll();
        }

        private async Task LoadModelsAsync()
        {
            await ((BusModelViewModel)DataContext).LoadModelsAsync();
        }

        private async Task LoadProducersAsync()
        {
            await ((BusModelViewModel)DataContext).LoadProducersAsync();
        }

        private void ToggleEditCreateForms()
        {
            if (((BusModelViewModel)DataContext).Id != -1)
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

        private void ModelsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((BusModelViewModel)DataContext).ChangeCurrentModel(e.AddedItems[0]);
            ToggleEditCreateForms();
            TitleBox.Focus();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            TitleBox.Focus();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (((BusModelViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((BusModelViewModel)DataContext).CreateModelAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((BusModelViewModel)DataContext).UpdateModelAsync();
            }

            await LoadModelsAsync();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((BusModelViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((BusModelViewModel)DataContext).DeleteModelAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadModelsAsync();
        }


    }
}
