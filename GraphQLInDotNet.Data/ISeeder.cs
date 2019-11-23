namespace GraphQLInDotNet.Data
{
    public interface ISeeder
    {
        void SeedData(IDataContext dataContext);
    }
}
