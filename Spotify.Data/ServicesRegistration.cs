using GraphQLInDotNet.Data;
using Microsoft.Extensions.DependencyInjection;
using Spotify.Data.Seed;

namespace Spotify.Data
{
    public static class ServicesRegistration
    {
        public static IServiceCollection UseSpotifyDataSeeder(this IServiceCollection services)
        {
            services.AddSingleton<ISeeder, RingobotSpotifyApiDataSeeder>();
            return services;
        }
    }
}
