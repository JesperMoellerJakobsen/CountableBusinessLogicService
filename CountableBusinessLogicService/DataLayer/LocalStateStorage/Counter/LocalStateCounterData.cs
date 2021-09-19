using System;
using System.Threading.Tasks;
using Domain.Model.Entities;

namespace LocalStateStorage.Counter
{
    /// <summary>
    /// Stubbed data representing some sort of local state persistence.
    /// Only used for showing business logic in some of the other areas.
    /// </summary>
    public class LocalStateCounterData : ILocalStateCounterData
    {
        private readonly ICounter _counter;

        public LocalStateCounterData()
        {
            _counter = GenerateDummyData();
        }

        public Task<ICounter> GetCounter()
        {
            return Task.FromResult(_counter);
        }

        public ICounter GenerateDummyData()
        {
            return new Domain.Model.Entities.Counter
            {
                Id = 1,
                Value = 10,
                Version = Convert.FromBase64String("AAAAAAAAB9M=")
            };
        }
    }
}