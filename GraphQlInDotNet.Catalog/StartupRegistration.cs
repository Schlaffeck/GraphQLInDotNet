using Microsoft.Extensions.DependencyInjection;

namespace GraphQlInDotNet.Catalog
{
    public static class StartupRegistration
    {
        public static IServiceCollection AddCatalogDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<GetAllCategories.GetAllCategoriesQuery>()
                .AddScoped<CreateCategory.CreateCategoryCommand>();
        }
    }
}
