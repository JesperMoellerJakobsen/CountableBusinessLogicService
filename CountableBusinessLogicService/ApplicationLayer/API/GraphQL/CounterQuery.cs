using System;
using Domain.Services;
using GraphQL.Types;
using API.GraphQL.Model;
using GraphQL;
using Integrations.UserRestService;
using Microsoft.Extensions.DependencyInjection;

namespace API.GraphQL
{
    public class CounterQuery : ObjectGraphType
    {
        public CounterQuery(IServiceProvider serviceProvider)
        {
            Name = "query";

            FieldAsync<CounterGraphType>("count", "Retrieves the counter based on a current user",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "User" }),
                async context =>
            {
                var user = context.GetArgument<int>("User");
                var counterService = serviceProvider.GetService<ICounterBusinessLogicService>();
                return await counterService.GetCounter(user);
            });

            FieldAsync<ListGraphType<UserGraphType>>("users", "Retrieves users", null,
                async _ =>
            {
                var userService = serviceProvider.GetService<IUserRestService>();
                return await userService.GetAllUsers();
            });
        }
    }
}
