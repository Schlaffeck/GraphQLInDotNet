using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraphQLInDotNet.Data.Models
{
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
