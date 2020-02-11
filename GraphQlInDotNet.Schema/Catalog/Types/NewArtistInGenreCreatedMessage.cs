using GraphQLInDotNet.Data.Models;
using HotChocolate.Language;
using HotChocolate.Subscriptions;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class NewArtistInGenreCreatedMessage : EventMessage
    {
        public NewArtistInGenreCreatedMessage(int genreId, Artist artist) 
            : base(CreateEventDescription(genreId), artist)
        {
        }

        private static EventDescription CreateEventDescription(int genreId)
        {
            return new EventDescription("onNewArtistInGenreCreated",
                new ArgumentNode("genreId", new IntValueNode(genreId)));
        }
    }
}
