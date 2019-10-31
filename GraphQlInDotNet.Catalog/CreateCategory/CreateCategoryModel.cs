using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQlInDotNet.Catalog.CreateCategory
{
    public class CreateCategoryModel
    {
        public string Name { get; set; }

        public IEnumerable<CreateCategoryModel> NewSubCategories { get; set; }
    }
}
