using GraphQlInDotNet.Schema.Catalog;
using GraphQlInDotNet.Schema.Catalog.Types;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQlInDotNet.Schema
{
    public static class SchemaBuilderHelper
    {
        public static ISchemaBuilder AddMusicCatalogDomain(this ISchemaBuilder builder)
        {
            return builder
                .AddQueryType<MusicCatalogQuery>(td => td.Field(q => q.Artists()))
                .AddMutationType<MusicCatalogMutation>();
        }
    }
}
