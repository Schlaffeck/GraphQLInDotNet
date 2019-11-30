using GraphQLInDotNet.Data.Models;
using HotChocolate.Language;
using HotChocolate.Subscriptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class NewAlbumReleasedMessage : EventMessage
    {
        public NewAlbumReleasedMessage(int artistId, Album album) 
            : base(CreateEventDescription(artistId), album)
        {
        }

        private static EventDescription CreateEventDescription(int artistId)
        {
            return new EventDescription("onNewAlbumReleased",
                new ArgumentNode("artistId", new IntValueNode(artistId)));
        }
    }
}
