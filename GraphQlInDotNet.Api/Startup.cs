using HotChocolate;
using HotChocolate.AspNetCore;
using GraphQlInDotNet.Catalog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GraphQlInDotNet.Domain.InMemory;
using GraphQlInDotNet.Schema;

namespace GraphQlInDotNet.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseInMemoryDomain();
            services.AddCatalogDomain();
            services.AddGraphQL(sp => SchemaBuilder.New()
            .AddCatalogDomain()
            .AddServices(sp)
            .Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL("/gql");
            app.UsePlayground("/gql", "/playground");
        }
    }
}
