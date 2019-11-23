using System.Collections.Generic;

namespace GraphQLInDotNet.Data.Models
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Category> SubCategories { get; set; }
    }
}
