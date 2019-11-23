using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GraphQLInDotNet.Data.Models
{
    public class Artist : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string UrlLink { get; set; }

        public string ExternalId { get; set; }

        public ICollection<Genre> Genres { get; set; }

        [InverseProperty(nameof(Album.Artist))]
        public ICollection<Album> Albums { get; set; }
    }
}
