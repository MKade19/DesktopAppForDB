using BusStation.UI.Services.Abstract;
using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using Syncfusion.Windows.Shared;
using System.Windows;
using System.Windows.Controls;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView(IUserDataService userDataService)
        {
            InitializeComponent();
            Loaded += UserView_Loaded;
            DataContext = new UserViewModel(userDataService);
        }

        private async void UserView_Loaded(object sender, RoutedEventArgs e)
        {
            await ((UserViewModel)DataContext).LoadUsersAsync();
        }

        private void ClearCurrentRecord()
        {
            ((UserViewModel)DataContext).Id = -1;
            UserNameBox.Text = string.Empty;
            UsersGrid.UnselectAll();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (((UserViewModel)DataContext).Id == -1)
            {
                MessageBoxStore.Warning("Пользователь для удаления не выбран!");
                return;
            }

            if (MessageBoxStore.Confirmation("Вы точно желаете удалить данную запись?") == MessageBoxResult.No)
            {
                return;
            }

            await ((UserViewModel)DataContext).DeleteUserAsync();
            ClearCurrentRecord();
            EditForm.Visibility = Visibility.Collapsed;
            await ((UserViewModel)DataContext).LoadUsersAsync();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxStore.Confirmation("Вы точно изменить роль данного пользователя?") == MessageBoxResult.No)
            {
                return;
            }

            await((UserViewModel)DataContext).UpdateUserAsync();
            await ((UserViewModel)DataContext).LoadUsersAsync();
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            EditForm.Visibility = Visibility.Visible;
            ((UserViewModel)DataContext).ChangeCurrentUser(e.AddedItems[0]);
            RoleComboBox.Focus();
        }
    }
}
