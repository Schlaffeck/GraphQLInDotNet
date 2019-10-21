using GraphQlInDotNet.Domain.InMemory.Data;
using GraphQLInDotNet.Domain.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlInDotNet.Domain.InMemory
{
    public static class ServicesRegistration
    {
        public static IServiceCollection UseInMemoryDomain(this IServiceCollection services)
        {
            services.AddScoped<IDataContext, InMemoryDataContext>();
            return services;
        }
    }
}
