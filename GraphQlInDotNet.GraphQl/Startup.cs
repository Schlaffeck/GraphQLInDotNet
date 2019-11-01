using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.AspNetCore;
using HotChocolate;
using HotChocolate.AspNetCore.Playground;
using GraphQlInDotNet.Catalog;
using GraphQlInDotNet.Domain.InMemory;
using GraphQlInDotNet.Schema;

namespace GraphQlInDotNet.GraphQl
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // If you need dependency injection with your query object add your query type as a services.
            // services.AddSingleton<Query>();

            // enable InMemory messaging services for subscription support.
            // services.AddInMemorySubscriptionProvider();
            services.UseInMemoryDomain();
            services.AddCatalogDomain();

            // this enables you to use DataLoader in your resolvers.
            services.AddDataLoaderRegistry();

            // Add GraphQL Services
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddCatalogDomain()
                .Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            // enable this if you want tu support subscription.
            // app.UseWebSockets();
            app.UseGraphQL("/graphql")
            .UsePlayground("/graphql");
        }
    }
}
