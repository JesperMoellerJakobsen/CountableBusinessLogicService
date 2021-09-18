using Domain.Model.Entities;
using GraphQL.Types;

namespace API.GraphQL.Model
{
    public class UserGraphType : ObjectGraphType<IUser>
    {
        public UserGraphType()
        {
            Field<IntGraphType>("Id", "Id of the user", resolve: context => context.Source.Id);
            Field<StringGraphType>("FirstName", "FirstName of the user", resolve: context => context.Source.FirstName);
            Field<StringGraphType>("LastName", "LastName of the user", resolve: context => context.Source.LastName);
            Field<StringGraphType>("Email", "Email of the user", resolve: context => context.Source.Email);
            Field<ListGraphType<PermissionGraphType>>("ActionsAllowed", "Action of the user", resolve: context => context.Source.ActionsAllowed);
        }
    }
}
