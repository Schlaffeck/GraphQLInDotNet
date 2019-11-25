using System.Collections.Generic;

namespace GraphQlInDotNet.MusicCatalog.Models
{
    public class CreateArtistModel
    {
        public string Name { get; set; }

        public IEnumerable<string> Genres { get; set; }
    }
}
