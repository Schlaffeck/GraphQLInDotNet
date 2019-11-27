﻿using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class TrackType : ObjectType<Track>
    {
        protected override void Configure(IObjectTypeDescriptor<Track> descriptor)
        {
            descriptor.Field(t => t.ExternalId).Type<StringType>();
            descriptor.Field(t => t.Duration).Type<DateTimeType>();
            descriptor.Field(t => t.Id).Type<IdType>();
            descriptor.Field(t => t.UrlLink).Type<UrlType>();
            descriptor.Field(t => t.Title).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Album).Type<NonNullType<AlbumType>>()
                .Resolver(async ctx => await ctx.Service<IDataContext>()
                .Albums.QueryNoTracking().FirstOrDefaultAsync(a => a.Id == ctx.Parent<Track>().AlbumId));
        }
    }
}