using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLInDotNet.Data.Models
{
    public class Track : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string UrlLink { get; set; }

        public string ExternalId { get; set; }

        public int AlbumId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public Album Album { get; set; }
    }
}
