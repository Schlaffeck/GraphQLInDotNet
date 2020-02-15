using GraphQLInDotNet.Data.Models;
using HotChocolate.Types.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQlInDotNet.Schema.Catalog.Types.Filter
{
    public class TrackFilterType : FilterInputType<Track>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Track> descriptor)
        {
            descriptor.BindFieldsExplicitly()
                .Filter(t => t.Title);
        }
    }
}
