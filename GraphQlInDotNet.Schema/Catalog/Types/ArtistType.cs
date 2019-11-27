using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class ArtistType : ObjectType<Artist>
    {
        protected override void Configure(IObjectTypeDescriptor<Artist> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(a => a.Name).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.ExternalId).Type<StringType>();
            descriptor.Field(a => a.Genres).Type<NonNullType<ListType<StringType>>>()
                .Resolver(ctx => ctx.Parent<Artist>().Genres.Select(g => g.Genre.Name));
        }
    }
}
