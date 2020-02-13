using GraphQlInDotNet.Schema.Catalog;
using GraphQlInDotNet.Schema.Catalog.Types;
using GraphQLInDotNet.Data.Models;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Filters;
using HotChocolate.Types.Sorting;
using HotChocolate.Types.Relay;
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
                .BindClrType<DateTime, DateType>()
                .BindClrType<TimeSpan, DateTimeType>()
                .AddType<ArtistType>()
                .AddType<TrackType>()
                .AddType<GenreType>()
                .AddType<AlbumType>()
                .AddQueryType<MusicCatalogQuery>(td =>
                {
                    td.Field(q => q.Artists())
                        .UseFiltering<Artist>(descriptor =>
                        {
                            descriptor
                            .BindFieldsExplicitly()
                            .Filter(a => a.Name)
                            .AllowContains()
                            .And().AllowEquals()
                            .And().AllowNotContains()
                            .And().AllowNotEquals();
                        })
                        .UseSorting<Artist>(descriptor =>
                        {
                            descriptor
                            .BindFieldsExplicitly()
                            .Sortable(a => a.Name);
                            descriptor.Sortable(a => a.Id);
                        });
                    td.Field(q => q.Albums())
                    .UsePaging<ObjectType<AlbumDto>>();
                })
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
