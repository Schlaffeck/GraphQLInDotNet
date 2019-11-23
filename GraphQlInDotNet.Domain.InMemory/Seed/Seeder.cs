using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace GraphQlInDotNet.Data.InMemory.Seed
{
    public class Seeder : ISeeder
    {
        private bool dataSeeded = false;

        public void SeedData(IDataContext dataContext)
        {
            if(dataSeeded)
            {
                return;
            }

            dataSeeded = true;
            foreach (var category in CreateCategories())
            {
                dataContext.Categories.Add(category);
            }
        }

        private static IEnumerable<Category> CreateCategories(int count = 4, string namePrefix = "Main ")
        {
            for (var i = 0; i < count; i++)
            {
                yield return CreateCategory(i, i, namePrefix: namePrefix);
            }
        }

        private static Category CreateCategory(int index, int noOfSubCats = 0, string namePrefix = "", string nameSuffix = "")
        {
            return new Category
            {
                Name = $"{namePrefix}Category {index}{nameSuffix}",
                SubCategories = CreateCategories(noOfSubCats, namePrefix: "Sub").ToList()
            };
        }
    }
}
