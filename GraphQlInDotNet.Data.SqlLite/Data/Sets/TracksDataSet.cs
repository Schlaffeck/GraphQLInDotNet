using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    internal class TracksDataSet : DataSet<Track>
    {
        public TracksDataSet(DomainDbContext context) : base(context)
        {
        }

        public override IQueryable<Track> QueryWithIncludes()
        {
            return base.DbSet.Include(a => a.Album)
                .ThenInclude(a => a.Artist)
                .ThenInclude(g => g.Genres)
                .ThenInclude(g => g.Genre);
        }
    }
}
