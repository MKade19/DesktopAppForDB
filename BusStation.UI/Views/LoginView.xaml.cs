using System.Windows.Controls;
using BusStation.UI.ViewModels;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private async void SignInButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await ((LoginViewModel)DataContext).LoginAsync();
            EventAggregator.Instance.RaiseUserAuthorizedEvent();
        }
    }
}
