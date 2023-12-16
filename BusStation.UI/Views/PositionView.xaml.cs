using BusStation.UI.ViewModels;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BusStation.UI.Util;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для PositionView.xaml
    /// </summary>
    public partial class PositionView : UserControl
    {
        public PositionView()
        {
            InitializeComponent();
            Loaded += PositionView_Loaded;
            DataContext = new PositionViewModel();
        }

        private async void PositionView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadPositionsAsync();
        }

        private async Task LoadPositionsAsync()
        {
            await ((PositionViewModel)DataContext).LoadPositionsAsync();
        }

        private void ClearCurrentRecord()
        {
            ((PositionViewModel)DataContext).Id = -1;
            TitleBox.Text = string.Empty;
            SalaryBox.Text = string.Empty;
            PositionsGrid.UnselectAll();
        }

        private void ToggleEditCreateForms()
        {
            if (((PositionViewModel)DataContext).Id != -1)
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


        private void PositionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            CreateEditForm.Visibility = Visibility.Visible;
            ((PositionViewModel)DataContext).ChangeCurrentPosition(e.AddedItems[0]);
            ToggleEditCreateForms();
            TitleBox.Focus();
        }

        private async void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (((PositionViewModel)DataContext).Id == -1)
            {
                if (MessageBoxStore.Confirmation("Вы точно желаете добавить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((PositionViewModel)DataContext).CreatePositionAsync();
                ClearCurrentRecord();
            }
            else
            {
                if (MessageBoxStore.Confirmation("Вы точно изменить данную запись?") == MessageBoxResult.No)
                {
                    return;
                }

                await ((PositionViewModel)DataContext).UpdatePositionAsync();
            }

            await LoadPositionsAsync();
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateEditForm.Visibility = Visibility.Visible;
            ClearCurrentRecord();
            ToggleEditCreateForms();
            TitleBox.Focus();
        }

        private async void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (((PositionViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Запись для удаления не выбрана!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((PositionViewModel)DataContext).DeletePositionAsync();
            ClearCurrentRecord();
            CreateEditForm.Visibility = Visibility.Collapsed;
            await LoadPositionsAsync();
        }
    }
}
