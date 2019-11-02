using GraphQLInDotNet.Data;
using GraphQLInDotNet.Data.Models;
using GraphQLInDotNet.Domain.Data;
using System.Collections.Generic;
using System.Linq;

namespace GraphQlInDotNet.Data.InMemory.Seed
{
    public class Seeder : ISeeder
    {
        private bool dataSeeded = false;
        private readonly IDataContext dataContext;

        public Seeder(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SeedData()
        {
            if(dataSeeded)
            {
                return;
            }

            dataSeeded = true;
            foreach (var category in CreateCategories())
            {
                this.dataContext.Categories.Add(category);
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
