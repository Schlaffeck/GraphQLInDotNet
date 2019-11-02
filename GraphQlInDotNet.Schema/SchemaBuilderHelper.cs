using GraphQlInDotNet.Catalog.CreateCategory;
using GraphQlInDotNet.Schema.Catalog;
using GraphQlInDotNet.Schema.Catalog.Types;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQlInDotNet.Schema
{
    public static class SchemaBuilderHelper
    {
        public static ISchemaBuilder AddCatalogDomain(this ISchemaBuilder builder)
        {
            return builder
                .AddQueryType<CatalogQuery>(td => td.Field(q => q.Categories()).UseFiltering())
                .AddMutationType<CatalogMutation>()
                .AddType<CreateCategoryModelInput>();
        }
    }
}
