using GraphQlInDotNet.MusicCatalog.Models;
using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Helpers;
using GraphQLInDotNet.Data.Models;
using HotChocolate;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Schema.Catalog
{
    public class MusicCatalogMutation
    {
        private readonly IDataContext dataContext;

        public MusicCatalogMutation(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Artist> CreateArtist([GraphQLNonNullType] CreateArtistModel input)
        {
            var newArtist = new Artist
            {
                Name = input.Name
            };

            this.dataContext.AssignGenresToArtist(newArtist, input.Genres);
            await this.dataContext.SaveChangesAsync();
            return this.dataContext.Artists.Get(newArtist.Id);
        }
    }
}
