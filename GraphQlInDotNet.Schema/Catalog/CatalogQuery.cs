using GraphQlInDotNet.Catalog.GetAllCategories;
using GraphQLInDotNet.Data.Models;
using System.Collections.Generic;

namespace GraphQlInDotNet.Schema.Catalog
{
    public class CatalogQuery
    {
        private readonly GetAllCategoriesQuery getAllCategoriesQuery;

        public CatalogQuery(GetAllCategoriesQuery getAllCategoriesQuery)
        {
            this.getAllCategoriesQuery = getAllCategoriesQuery;
        }

        public IEnumerable<Category> Categories()
        {
            return this.getAllCategoriesQuery.Execute();
        }
    }
}
