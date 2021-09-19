using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities;

namespace LocalStateStorage.User
{
    /// <summary>
    /// Stubbed data representing some sort of local state persistence.
    /// Only used for showing business logic in some of the other areas.
    /// </summary>
    public class LocalStateUserData : ILocalStateUserData
    {
        private readonly IEnumerable<IUser> _users;

        public LocalStateUserData()
        {
            _users = GenerateDummyData();
        }

        public Task<IUser> GetUserById(int id)
        {
            var user = _users.First(x => x.Id == id);
            return Task.FromResult(user);
        }

        public Task<IEnumerable<IUser>> GetAllUsers()
        {
            return Task.FromResult(_users);
        }

        private static IEnumerable<IUser> GenerateDummyData()
        {
            return new List<IUser>
            {
                new Domain.Model.Entities.User(1, "Pernille", "Sørensen", "Mail@provider.dk", new List<Action>()),
                new Domain.Model.Entities.User(2, "Lars", "Jensen", "Mail@google.eu", new List<Action> {Action.Read}),
                new Domain.Model.Entities.User(3, "Arne", "Jakobsen", "Mail@hotmail.dk", new List<Action> { Action.Read, Action.Write }),
                new Domain.Model.Entities.User(4, "Camilla", "Petersen", "Mail@email.com", new List<Action> { Action.Read, Action.WriteNegative })
            };
        }
    }
}