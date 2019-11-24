using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GraphQLInDotNet.Data.Models
{
    public class Album : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        [InverseProperty(nameof(Track.Album))]
        public virtual ICollection<Track> Tracks { get; set; }

        public string ExternalId { get; set; }

        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist Artist { get; set; }

        public string UrlLink { get; set; }
    }
}
