using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using HotChocolate.Types;
using System.Linq;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class GenreType : ObjectType<Genre>
    {
        protected override void Configure(IObjectTypeDescriptor<Genre> descriptor)
        {
            descriptor.Field(g => g.Id).Type<IdType>();
            descriptor.Field(g => g.Name).Type<NonNullType<StringType>>();
            descriptor.Field(g => g.Artists).Type<NonNullType<ListType<ArtistType>>>()
                .Resolver(ctx => ctx.Service<IDataContext>()
                .Artists.QueryNoTracking().Where(a => a.Genres.Any(ag => ag.GenreId == ctx.Parent<Genre>().Id)));
        }
    }
}
