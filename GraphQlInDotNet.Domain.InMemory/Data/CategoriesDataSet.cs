using GraphQlInDotNet.Domain.InMemory.Data;
using GraphQLInDotNet.Data.Models;

namespace GraphQlInDotNet.Data.InMemory.Data
{
    internal class CategoriesDataSet : InMemoryDataSet<Category>
    {
        public override int Add(Category entity)
        {
            var id = base.Add(entity);

            if(id > 0)
            {
                foreach (var subCat in entity.SubCategories)
                {
                    Add(subCat);
                }
            }

            return id;
        }
    }
}
