using GraphQlInDotNet.Data.EntityFramework.Data;
using GraphQLInDotNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlInDotNet.Data.EntityFramework
{
    public static class ServiceRegistration
    {
        public static IServiceCollection UseSqlServerData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataContext, MusicDataContext>();
            services.AddDbContext<DomainDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));
            return services;
        }
    }
}
