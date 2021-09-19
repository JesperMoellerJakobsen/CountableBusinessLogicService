using System.Threading.Tasks;
using Domain.Model.Config;
using Domain.Model.Entities;
using Integrations.CounterRestService;
using LocalStateStorage.Counter;
using LocalStateStorage.User;

namespace Domain.Services
{
    public class CounterBusinessLogicService : ICounterBusinessLogicService
    {
        private readonly ICounterRestService _counterService;
        private readonly ILocalStateCounterData _localStateCounterData;
        private readonly ILocalStateUserData _localStateUserData;

        public CounterBusinessLogicService(ICounterRestService counterService, ILocalStateCounterData localStateCounterData, ILocalStateUserData localStateUserData)
        {
            _counterService = counterService;
            _localStateCounterData = localStateCounterData;
            _localStateUserData = localStateUserData;
        }

        public async Task<ICounter> GetCounter(int userId)
        {
            var user = await _localStateUserData.GetUserById(userId);

            if (!user.ActionsAllowed.Any(Action.Read))
                throw new ActionNotPermittedException();

            return await _localStateCounterData.GetCounter();
        }

        public async Task<bool> TryIncrement(int userId, byte[] currentVersion)
        {
            var user = await _localStateUserData.GetUserById(userId);

            if (!user.ActionsAllowed.Any(Action.Write, Action.WriteNegative))
                throw new ActionNotPermittedException();

            return await _counterService.TryIncrement(currentVersion);
        }

        public async Task<bool> TryDecrement(int userId, byte[] currentVersion)
        {
            var user = await _localStateUserData.GetUserById(userId);

            if (!user.ActionsAllowed.Any(Action.Write, Action.WriteNegative))
                throw new ActionNotPermittedException();

            var counter = await _localStateCounterData.GetCounter();

            if (counter.Value <= 0 && !user.ActionsAllowed.Any(Action.WriteNegative))
                throw new ActionNotPermittedException();

            return await _counterService.TryDecrement(currentVersion);
        }
    }
}
