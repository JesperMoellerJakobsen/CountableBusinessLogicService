using System.Collections.Generic;

namespace Domain.Model.Entities
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IList<Action> ActionsAllowed { get; set; }

        public User(int id, string firstName, string lastName, string email, IList<Action> actionsAllowed)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ActionsAllowed = actionsAllowed;
        }
    }
}
