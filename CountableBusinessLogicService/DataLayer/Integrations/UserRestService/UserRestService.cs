using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities;

namespace Integrations.UserRestService
{
    /// <summary>
    /// Stubbed data. Only used for showing business logic in some of the other areas.
    /// </summary>
    public class UserRestService : IUserRestService
    {
        private readonly IEnumerable<IUser> _users;

        public UserRestService()
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
                new User(1, "Pernille", "Sørensen", "Mail@provider.dk", new List<Permissions>()),
                new User(2, "Lars", "Jensen", "Mail@google.eu", new List<Permissions> {Permissions.Read}),
                new User(3, "Arne", "Jakobsen", "Mail@hotmail.dk", new List<Permissions> { Permissions.Read, Permissions.Write }),
                new User(4, "Camilla", "Petersen", "Mail@email.com", new List<Permissions> { Permissions.Read, Permissions.WriteNegative })
            };
        }
    }
}