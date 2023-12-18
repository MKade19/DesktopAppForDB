using BusStation.Common.Models;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IAuthDataService
    {
        public Task LoginAsync(User user);
    }
}
