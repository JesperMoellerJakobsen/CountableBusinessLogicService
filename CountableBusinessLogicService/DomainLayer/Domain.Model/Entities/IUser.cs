using System.Collections.Generic;

namespace Domain.Model.Entities
{
    public interface IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IList<Permissions> ActionsAllowed { get; set; }
    }
}