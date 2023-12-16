using BusStation.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IWorkerDataService : IDataService<Worker>
    {
        public Task<IEnumerable<Worker>> GetDriversAsync();
        public Task<IEnumerable<Worker>> GetMechanicsAsync();
    }
}
