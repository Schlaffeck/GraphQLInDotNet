using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Linq;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class ArtistType : ObjectType<Artist>
    {
        protected override void Configure(IObjectTypeDescriptor<Artist> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(a => a.Name).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.ExternalId).Type<StringType>();

            descriptor.Field(a => a.Genres).Type<NonNullType<ListType<GenreType>>>()
                .Resolver(ctx => ctx.Parent<Artist>().Genres.Select(g => g.Genre))
                .UseFiltering<Genre>(descriptor => descriptor.BindFieldsExplicitly()
                    .Filter(g => g.Name));

            descriptor.Field(a => a.Albums).Type<NonNullType<ListType<AlbumType>>>()
                .Resolver(AlbumsResolver);
        }

        private IQueryable<Album> AlbumsResolver(IResolverContext ctx)
        {
            return ctx.Service<IDataContext>().
                Albums.QueryNoTracking().Where(a => a.ArtistId == ctx.Parent<Artist>().Id);
        }
    }
}
