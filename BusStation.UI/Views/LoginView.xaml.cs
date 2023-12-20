using System.Windows.Controls;
using BusStation.UI.Services.Abstract;
using BusStation.UI.ViewModels;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView(IAuthDataService authDataService)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(authDataService);
        }

        private async void SignInButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await ((LoginViewModel)DataContext).LoginAsync();
            EventAggregator.Instance.RaiseUserAuthorizedEvent();
        }
    }
}
