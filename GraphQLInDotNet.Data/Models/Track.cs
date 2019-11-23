using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLInDotNet.Data.Models
{
    public class Track : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public string UrlLink { get; set; }

        public string ExternalId { get; set; }
    }
}
