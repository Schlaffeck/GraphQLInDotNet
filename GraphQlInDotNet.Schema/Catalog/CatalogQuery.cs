using GraphQlInDotNet.Catalog.FindCategories;
using GraphQLInDotNet.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace GraphQlInDotNet.Schema.Catalog
{
    public class CatalogQuery
    {
        private readonly FindCategoriesQuery getAllCategoriesQuery;

        public CatalogQuery(FindCategoriesQuery getAllCategoriesQuery)
        {
            this.getAllCategoriesQuery = getAllCategoriesQuery;
        }

        public IQueryable<Category> Categories()
        {
            return this.getAllCategoriesQuery.Execute();
        }
    }
}
