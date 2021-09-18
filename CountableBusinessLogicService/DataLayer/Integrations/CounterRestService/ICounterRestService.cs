using System.Threading.Tasks;
using Domain.Model.Entities;

namespace Integrations.CounterRestService
{
    public interface ICounterRestService
    {
        public Task<ICounter> GetCounter();
        public Task<bool> TryIncrement(byte[] clientCounterVersion);
        public Task<bool> TryDecrement(byte[] clientCounterVersion);
    }
}
