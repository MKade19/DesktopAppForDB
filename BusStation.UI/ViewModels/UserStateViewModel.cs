using System;

namespace BusStation.UI.ViewModels
{
    public class UserStateViewModel : ViewModelBase
    {
        private static UserStateViewModel? _instance;
        private bool _isUserAuthorized;
        private bool _isUserAdmin;

        public static UserStateViewModel Instance
        {
            get => _instance = _instance ?? (_instance = new UserStateViewModel());
        }

        private UserStateViewModel()
        {
            _isUserAuthorized = false;
            EventAggregator.Instance.UserAuthorized += Instance_UserAuthorized;
            EventAggregator.Instance.UserUnauthorized += Instance_UserUnauthorized;
        }

        private void Instance_UserUnauthorized(object? sender, EventArgs e)
        {
            IsUserAuthorized = false;
            IsUserAdmin = false;
        }

        private void Instance_UserAuthorized(object? sender, EventArgs e)
        {
            IsUserAuthorized = true;
            IsUserAdmin = Properties.Settings.Default.Role.Equals("admin");
        }

        public bool IsUserAuthorized
        {
            get => _isUserAuthorized;
            set
            {
                _isUserAuthorized = value;
                OnPropertyChanged(nameof(IsUserAuthorized));
            }
        }

        public bool IsUserAdmin
        {
            get => _isUserAdmin;
            set
            {
                _isUserAdmin = value;
                OnPropertyChanged(nameof(IsUserAdmin));
            }
        }
    }
}
