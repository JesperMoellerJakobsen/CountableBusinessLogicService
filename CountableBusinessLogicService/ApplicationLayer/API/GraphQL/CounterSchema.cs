using GraphQL.Types;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace API.GraphQL
{
	public class CounterSchema : Schema
	{
		public CounterSchema(IServiceProvider provider)
			: base(provider)
		{
			Query = provider.GetRequiredService<CounterQuery>();
			Mutation = provider.GetRequiredService<CounterMutation>();
        }
	}
}
