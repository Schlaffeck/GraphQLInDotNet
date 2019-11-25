using GraphQlInDotNet.MusicCatalog.Models;
using HotChocolate.Types;

namespace GraphQlInDotNet.Schema.Catalog.Types
{
    public class CreateArtistModelInput : InputObjectType<CreateArtistModel>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateArtistModel> descriptor)
        {
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Genres).Type<NonNullType<ListType<StringType>>>();
        }
    }
}
