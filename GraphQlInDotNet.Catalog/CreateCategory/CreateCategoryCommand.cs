using GraphQLInDotNet.Data.Models;
using GraphQLInDotNet.Domain.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlInDotNet.Catalog.CreateCategory
{
    public class CreateCategoryCommand
    {
        private readonly IDataContext dataContext;

        public CreateCategoryCommand(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Category> ExecuteAsync(CreateCategoryModel model)
        {
            var id = this.dataContext.Categories.Add(Map(model));
            return await this.dataContext.Categories.GetAsync(id);
        }

        private Category Map(CreateCategoryModel model)
        {
            return new Category
            {
                Name = model.Name,
                SubCategories = model.NewSubCategories.Select(Map).ToList()
            };
        }
    }
}
