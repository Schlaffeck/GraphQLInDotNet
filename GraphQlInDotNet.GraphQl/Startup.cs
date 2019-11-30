using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.AspNetCore;
using HotChocolate;
using GraphQlInDotNet.Schema;
using GraphQlInDotNet.Data.EntityFramework;
using Microsoft.Extensions.Configuration;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Subscriptions;
using HotChocolate.AspNetCore.Playground;

namespace GraphQlInDotNet.GraphQl
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
            services.UseSqlServerData(Configuration);
            //services.UseSpotifyDataSeeder();
            services.AddInMemorySubscriptionProvider();

            // Add GraphQL Services
            services.AddCommonGraphQLTypes();
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddMusicCatalogDomain()
                .Create());
            services
                .AddGraphQLSubscriptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
                //app.UseMiddleware<SeedDataMiddleware>();
            }

            // enable this if you want tu support subscription.
            app.UseWebSockets();
            app.UseGraphQLSubscriptions(new SubscriptionMiddlewareOptions
            {
                Path = "/"
            });
            app.UseGraphQL(new QueryMiddlewareOptions
            {
                EnableSubscriptions = true,
                Path = "/graphql",
                SubscriptionPath = "/"
            })
            .UsePlayground(new PlaygroundOptions { 
                EnableSubscription = true,
                QueryPath = "/graphql",
                SubscriptionPath = "/"
            });
        }
    }
}
