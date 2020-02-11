using GraphQLInDotNet.Data.Models;
using HotChocolate.Subscriptions;

namespace GraphQlInDotNet.Schema.Catalog
{
    public class MusicCatalogSubscription
    {
        public Album OnNewAlbumReleased(int artistId, IEventMessage eventMessage)
        {
            return (Album)eventMessage.Payload;
        }

        public Artist OnNewArtistInGenreCreated(int genreId, IEventMessage eventMessage)
        {
            return (Artist)eventMessage.Payload;
        }
    }
}
