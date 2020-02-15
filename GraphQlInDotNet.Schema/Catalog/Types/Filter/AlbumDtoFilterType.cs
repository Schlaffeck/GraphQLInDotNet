using HotChocolate.Types.Filters;

namespace GraphQlInDotNet.Schema.Catalog.Types.Filter
{
    public class AlbumDtoFilterType : FilterInputType<AlbumDto>
    {
        protected override void Configure(IFilterInputTypeDescriptor<AlbumDto> descriptor)
        {
            descriptor.BindFieldsExplicitly()
                .Filter(a => a.Title)
                .BindFiltersImplicitly();
            descriptor.BindFieldsExplicitly()
                .Filter(a => a.ReleaseDate)
                .BindFiltersExplicitly()
                .AllowGreaterThan()
                .And().AllowGreaterThanOrEquals()
                .And().AllowLowerThan()
                .And().AllowLowerThanOrEquals();
        }
    }
}
