using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraphQLInDotNet.Data.Models
{
    public class Genre : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Artist> Artists{ get; set; }
    }
}
