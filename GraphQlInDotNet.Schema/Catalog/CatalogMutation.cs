using GraphQlInDotNet.Catalog.CreateCategory;
using GraphQLInDotNet.Data.Models;
using HotChocolate;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Schema.Catalog
{
    public class CatalogMutation
    {
        private readonly CreateCategoryCommand createCategoryCommand;

        public CatalogMutation(CreateCategoryCommand createCategoryCommand)
        {
            this.createCategoryCommand = createCategoryCommand;
        }

        public async Task<Category> CreateCategory([GraphQLNonNullType] CreateCategoryModel input)
        {
            return await this.createCategoryCommand.ExecuteAsync(input);
        }
    }
}
