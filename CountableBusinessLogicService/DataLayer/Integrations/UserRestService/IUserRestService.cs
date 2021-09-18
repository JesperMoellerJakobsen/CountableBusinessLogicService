using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities;

namespace Integrations.UserRestService
{
    public interface IUserRestService
    {
        Task<IUser> GetUserById(int id);
        Task<IEnumerable<IUser>> GetAllUsers();
    }
}
