using System.Threading.Tasks;
using Domain.Model.Entities;

namespace LocalStateStorage.Counter
{
    public interface ILocalStateCounterData
    {
        Task<ICounter> GetCounter();
    }
}
