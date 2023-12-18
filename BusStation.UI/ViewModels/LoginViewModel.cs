using BusStation.Common.Models;
using BusStation.UI.Services;
using BusStation.UI.Services.Abstract;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private User _user = new User();
        private IAuthDataService AuthDataService = new AuthDataService();

        public string Username
        {
            get { return _user.Username; }
            set
            {
                _user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get { return _user.Password; }
            set
            {
                _user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public async Task LoginAsync()
        {
            await AuthDataService.LoginAsync(_user);
        }
    }
}
