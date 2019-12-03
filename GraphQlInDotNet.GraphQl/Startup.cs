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
using System;
using HotChocolate.Types;

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

            // Add GraphQL Services
            services.AddGraphQL(sp => SchemaBuilder.New()
            .BindClrType<TimeSpan, DateTimeType>()
                .Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL(new QueryMiddlewareOptions
            {
                Path = "/graphql",
            })
            .UsePlayground(new PlaygroundOptions { 
                QueryPath = "/graphql",
            });
        }
    }
}
