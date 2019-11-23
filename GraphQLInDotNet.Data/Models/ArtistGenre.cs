using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLInDotNet.Data.Models
{
    public class ArtistGenre
    {
        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist Artist { get; set; }

        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }
    }
}
