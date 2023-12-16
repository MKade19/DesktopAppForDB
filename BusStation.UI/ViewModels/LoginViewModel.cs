using System;

namespace BusStation.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _login = String.Empty;
        private string _password = String.Empty;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
