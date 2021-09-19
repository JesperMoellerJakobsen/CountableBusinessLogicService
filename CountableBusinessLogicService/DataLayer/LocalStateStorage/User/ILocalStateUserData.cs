using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities;

namespace LocalStateStorage.User
{
    public interface ILocalStateUserData
    {
        Task<IUser> GetUserById(int id);
        Task<IEnumerable<IUser>> GetAllUsers();
    }
}
