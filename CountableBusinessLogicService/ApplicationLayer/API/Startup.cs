using API.GraphQL;
using Domain.Model.Config;
using Domain.Services;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Integrations.CounterRestService;
using LocalStateStorage.Counter;
using LocalStateStorage.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();
            services.AddHttpContextAccessor();
            var connectionString = Configuration.GetValue<string>("CounterMicroservice:ServiceUrl");
            services.AddSingleton<CounterSchema>();
            services.AddSingleton<CounterMutation>();
            services.AddSingleton<CounterQuery>();
            services.AddSingleton(_ => new CounterMicroserviceConfiguration { ServiceUrl = connectionString });
            services.AddSingleton<ICounterRestService, CounterRestService>();
            services.AddSingleton<ILocalStateUserData, LocalStateUserData>();
            services.AddSingleton<ICounterBusinessLogicService, CounterBusinessLogicService>();
            services.AddSingleton<ILocalStateCounterData, LocalStateCounterData>();
            services.AddSingleton<ILocalStateUserData, LocalStateUserData>();
            services.AddHttpClient();
            services.AddGraphQL()
                .AddGraphTypes()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddSystemTextJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseGraphQL<CounterSchema>();
            app.UseGraphQLPlayground(new PlaygroundOptions { SchemaPollingEnabled = false });
        }
    }
}
