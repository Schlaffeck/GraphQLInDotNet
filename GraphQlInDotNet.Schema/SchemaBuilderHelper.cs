using GraphQlInDotNet.Catalog;
using GraphQlInDotNet.Catalog.CreateCategory;
using GraphQlInDotNet.Catalog.GetAllCategories;
using HotChocolate;

namespace GraphQlInDotNet.Schema
{
    public static class SchemaBuilderHelper
    {
        public static ISchemaBuilder AddCatalogDomain(this ISchemaBuilder builder)
        {
            return builder
                .AddQueryType<GetAllCategoriesQuery>()
                .AddMutationType<CreateCategoryCommand>();
        }
    }
}
