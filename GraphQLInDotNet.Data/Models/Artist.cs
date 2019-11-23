using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLInDotNet.Data.Models
{
    public class Artist : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UrlLink { get; set; }

        public string ExternalId { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}
