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

        private void SignInButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //How to reach tab 1?

            EventAggregator.Instance.RaiseUserAuthorizedEvent();
        }
    }
}
