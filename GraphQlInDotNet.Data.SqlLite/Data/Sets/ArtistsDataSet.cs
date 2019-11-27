using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    internal class ArtistsDataSet : DataSet<Artist>
    {
        public ArtistsDataSet(DomainDbContext context) : base(context)
        {
        }

        public override IQueryable<Artist> QueryWithIncludes()
        {
            return base.DbSet.Include(a => a.Albums)
                .ThenInclude(al => al.Tracks)
                .Include(a => a.Genres)
                .ThenInclude(g => g.Genre);
        }
    }
}
