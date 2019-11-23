using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLInDotNet.Data.Models
{
    public class Album : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public ICollection<Track> Tracks { get; set; }

        public string ExternalId { get; set; }
    }
}
