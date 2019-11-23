using GraphQlInDotNet.Common.Queries;
using GraphQLInDotNet.Data.Models;
using GraphQLInDotNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQlInDotNet.Catalog.FindCategories
{
    public class FindCategoriesQuery : IQuery<IQueryable<Category>>
    {
        private readonly IDataContext dataContext;

        public FindCategoriesQuery(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Category> Execute()
        {
            return this.dataContext.Categories.Query();
        }
    }
}
