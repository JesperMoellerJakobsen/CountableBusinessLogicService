using System.Threading.Tasks;
using Domain.Model.Config;
using Domain.Model.Entities;
using Integrations.CounterRestService;
using Integrations.UserRestService;

namespace Domain.Services
{
    public class CounterBusinessLogicService : ICounterBusinessLogicService
    {
        private readonly ICounterRestService _counterService;
        private readonly IUserRestService _userService;

        public CounterBusinessLogicService(ICounterRestService counterService, IUserRestService userService)
        {
            _counterService = counterService;
            _userService = userService;
        }

        public async Task<ICounter> GetCounter(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (!user.ActionsAllowed.Any(Permissions.Read))
                throw new ActionNotPermittedException();

            return await _counterService.GetCounter();
        }

        public async Task<bool> TryIncrement(int userId, byte[] currentVersion)
        {
            var user = await _userService.GetUserById(userId);

            if (!user.ActionsAllowed.Any(Permissions.Write, Permissions.WriteNegative))
                throw new ActionNotPermittedException();

            return await _counterService.TryIncrement(currentVersion);
        }

        public async Task<bool> TryDecrement(int userId, byte[] currentVersion)
        {
            var user = await _userService.GetUserById(userId);

            if (!user.ActionsAllowed.Any(Permissions.Write, Permissions.WriteNegative))
                throw new ActionNotPermittedException();

            var counter = await GetCounter(userId);

            if (counter.Value <= 0 && !user.ActionsAllowed.Any(Permissions.WriteNegative))
                throw new ActionNotPermittedException();

            return await _counterService.TryDecrement(currentVersion);
        }
    }
}
