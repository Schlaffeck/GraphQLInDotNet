using GraphQlInDotNet.Domain.InMemory.Data;
using GraphQLInDotNet.Domain.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlInDotNet.Domain.InMemory
{
    public static class ServicesRegistration
    {
        public static IServiceCollection UseInMemoryData(this IServiceCollection services)
        {
            services.AddSingleton<IDataContext, InMemoryDataContext>();
            return services;
        }
    }
}
