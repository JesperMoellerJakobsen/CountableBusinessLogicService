using System;
using Domain.Model.Entities;
using GraphQL.Types;

namespace API.GraphQL.Model
{
    public class CounterGraphType : ObjectGraphType<ICounter>
    {
        public CounterGraphType()
        {
            Field<IntGraphType>("Id", "Id of the counter", resolve: context => context.Source.Id);
            Field<IntGraphType>("Value", "Value of the counter", resolve: context => context.Source.Value);
            Field<StringGraphType>("Version", "Version of the counter", resolve: context => Convert.ToBase64String(context.Source.Version));
        }
    }
}
