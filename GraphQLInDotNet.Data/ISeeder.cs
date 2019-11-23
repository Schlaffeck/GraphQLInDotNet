using System.Threading.Tasks;

namespace GraphQLInDotNet.Data
{
    public interface ISeeder
    {
        Task SeedDataAsync(IDataContext dataContext);
    }
}
