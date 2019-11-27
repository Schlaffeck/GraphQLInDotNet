using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class AlbumType : ObjectType<Album>
    {
        protected override void Configure(IObjectTypeDescriptor<Album> descriptor)
        {
            descriptor.Field(a => a.ArtistId).Type<NonNullType<IdType>>();
            descriptor.Field(a => a.ExternalId).Type<StringType>();
            descriptor.Field(a => a.UrlLink).Type<UrlType>();
            descriptor.Field(a => a.Id).Type<NonNullType<IdType>>();
            descriptor.Field(a => a.Title).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.ReleaseDate).Type<NonNullType<DateType>>();
            descriptor.Field(a => a.Artist).Type<NonNullType<ArtistType>>()
                .Resolver(async ctx => await ctx.Service<IDataContext>()
                .Artists.QueryNoTracking().FirstOrDefaultAsync(a => a.Id == ctx.Parent<Album>().ArtistId));
            descriptor.Field(ctx => ctx.Tracks).Type<NonNullType<ListType<TrackType>>>()
                .Resolver(ctx => ctx.Service<IDataContext>()
                .Tracks.QueryNoTracking().Where(t => t.AlbumId == ctx.Parent<Album>().Id));
        }
    }
}
