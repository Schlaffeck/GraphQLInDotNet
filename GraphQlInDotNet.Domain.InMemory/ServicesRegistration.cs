using GraphQlInDotNet.Data.InMemory.Seed;
using GraphQlInDotNet.Domain.InMemory.Data;
using GraphQLInDotNet.Data;
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

        public static IServiceCollection UseInMemoryDataSeeder(this IServiceCollection services)
        {
            services.AddSingleton<ISeeder, Seeder>();
            return services;
        }
    }
}
