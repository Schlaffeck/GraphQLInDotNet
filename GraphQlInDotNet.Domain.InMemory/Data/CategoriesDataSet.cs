using GraphQlInDotNet.Domain.InMemory.Data;
using GraphQLInDotNet.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQlInDotNet.Data.InMemory.Data
{
    class CategoriesDataSet : InMemoryDataSet<Category>
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
