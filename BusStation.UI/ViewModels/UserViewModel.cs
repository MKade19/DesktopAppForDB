using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BusStation.UI.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private User _user = new User();
        private ObservableCollection<User> _users;
        private IUserDataService UserDataService { get; }
        private List<string> _roles = new List<string>() { "admin", "user" };

        public UserViewModel(IUserDataService userDataService) 
        { 
            UserDataService = userDataService;
        }

        public int Id
        {
            get { return _user.Id; }
            set
            {
                _user.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Username
        {
            get { return _user.Username; }
            set
            {
                _user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string? Role
        {
            get { return _user.Role; }
            set
            {
                _user.Role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public List<string> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public async Task LoadUsersAsync()
        {
            var loadedUsers = await UserDataService.GetAllAsync();
            Users = new ObservableCollection<User>(loadedUsers);
        }

        public void ChangeCurrentUser(object user)
        {
            User newUser = (User)user;
            Id = newUser.Id;
            Username = newUser.Username;
            Role = newUser.Role;
        }

        public async Task UpdateUserAsync()
        {
            await UserDataService.UpdateByIdAsync(_user);
        }

        public async Task DeleteUserAsync()
        {
            await UserDataService.DeleteByIdAsync(_user.Id);
        }
    }
}
