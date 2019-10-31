using GraphQLInDotNet.Data.Models;
using GraphQLInDotNet.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQlInDotNet.Catalog.GetAllCategories
{
    public class GetAllCategoriesQuery
    {
        private readonly IDataContext dataContext;

        public GetAllCategoriesQuery(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Category> Execute()
        {
            return this.dataContext.Categories.Query();
        }
    }
}
