using GraphQlInDotNet.Catalog.CreateCategory;
using HotChocolate.Types;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class CreateCategoryModelInput : InputObjectType<CreateCategoryModel>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateCategoryModel> descriptor)
        {
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.NewSubCategories).Type<NonNullType<ListType<CreateCategoryModelInput>>>();
        }
    }
}
