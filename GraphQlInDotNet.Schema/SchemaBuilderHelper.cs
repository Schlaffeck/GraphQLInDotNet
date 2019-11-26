using GraphQlInDotNet.Schema.Catalog;
using GraphQlInDotNet.Schema.Catalog.Types;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GraphQlInDotNet.Schema
{
    public static class SchemaBuilderHelper
    {
        public static ISchemaBuilder AddMusicCatalogDomain(this ISchemaBuilder builder)
        {
            return builder
                .BindClrType<TimeSpan, DateTimeType>()
                .AddQueryType<MusicCatalogQuery>(td => td.Field(q => q.Artists()))
                .AddMutationType<MusicCatalogMutation>();
        }

        public static IServiceCollection AddCommonGraphQLTypes(this IServiceCollection services)
        {
            TypeConversion.Default.Register<TimeSpan, DateTime>(from => new DateTime(from.Ticks));
            TypeConversion.Default.Register<DateTime, TimeSpan>(from => new TimeSpan(from.Ticks));
            return services;
        }
    }
}
