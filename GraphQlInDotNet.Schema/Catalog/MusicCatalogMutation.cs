using GraphQlInDotNet.MusicCatalog.Models;
using GraphQlInDotNet.Schema.Catalog.Types;
using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Helpers;
using GraphQLInDotNet.Data.Models;
using HotChocolate;
using HotChocolate.Subscriptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Schema.Catalog
{
    public class MusicCatalogMutation
    {
        private readonly IDataContext dataContext;
        private readonly IEventSender eventSender;

        public MusicCatalogMutation(IDataContext dataContext, IEventSender eventSender)
        {
            this.dataContext = dataContext;
            this.eventSender = eventSender;
        }

        public async Task<Artist> CreateArtist([GraphQLNonNullType] CreateArtistModel input)
        {
            var newArtist = new Artist
            {
                Name = input.Name,
                Genres = new List<ArtistGenre>()
            };

            this.dataContext.AssignGenresToArtist(newArtist, input.Genres);
            this.dataContext.Artists.Add(newArtist);
            await this.dataContext.SaveChangesAsync();
            var artist = this.dataContext.Artists.Get(newArtist.Id);
            foreach (var genre in artist.Genres)
            {
                await this.eventSender.SendAsync(new NewArtistInGenreCreatedMessage(genre.GenreId, artist));
            }
            return artist;
        }

        public async Task<Album> ReleaseArtistAlbum(int artistId, [GraphQLNonNullType]string albumTitle)
        {
            var newAlbum = new Album
            {
                Title = albumTitle,
                ArtistId = artistId,
            };

            this.dataContext.Albums.Add(newAlbum);
            await this.dataContext.SaveChangesAsync();
            
            var albumReleased = await dataContext.Albums.GetAsync(newAlbum.Id);
            await this.eventSender.SendAsync(new NewAlbumReleasedMessage(artistId, albumReleased));
            return albumReleased;
        }
    }
}
