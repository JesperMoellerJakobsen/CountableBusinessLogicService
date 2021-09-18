using System.Threading.Tasks;
using Domain.Model.Entities;

namespace Domain.Services
{
    public interface ICounterBusinessLogicService
    {
        public Task<ICounter> GetCounter(int userId);
        public Task<bool> TryIncrement(int userId, byte[] clientCounterVersion);
        public Task<bool> TryDecrement(int userId, byte[] clientCounterVersion);
    }
}
