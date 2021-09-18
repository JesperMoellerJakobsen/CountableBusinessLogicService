using System;
using GraphQL.Types;
using Domain.Services;
using GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace API.GraphQL
{
    public class CounterMutation : ObjectGraphType
    {
        public CounterMutation(IServiceProvider serviceProvider)
        {
            FieldAsync<BooleanGraphType>("Increment", "Increments the counter",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "User" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "Version" }),
                async context =>
                {
                    var input = MapInput(context);
                    var counterService = serviceProvider.GetService<ICounterBusinessLogicService>();
                    return await counterService.TryIncrement(input.userId, input.clientVersion);
                });

            FieldAsync<BooleanGraphType>("Decrement", "Decrements the counter",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "User" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "Version" }),
                async context =>
                {
                    var input = MapInput(context);
                    var counterService = serviceProvider.GetService<ICounterBusinessLogicService>();
                    return await counterService.TryDecrement(input.userId, input.clientVersion);
                });
        }

        private static (int userId, byte[] clientVersion) MapInput(IResolveFieldContext context)
        {
            var version = context.GetArgument<string>("Version");
            var user = context.GetArgument<int>("User");
            var versionByteArray = Convert.FromBase64String(version);
            return (user, versionByteArray);
        }
    }
}
