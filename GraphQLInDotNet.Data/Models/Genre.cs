using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLInDotNet.Data.Models
{
    public class Genre : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(ArtistGenre.Genre))]
        public virtual ICollection<ArtistGenre> Artists { get; set; }
    }
}
