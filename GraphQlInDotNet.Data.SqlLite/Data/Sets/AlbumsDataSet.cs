using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    internal class AlbumsDataSet : DataSet<Album>
    {
        public AlbumsDataSet(DomainDbContext context) : base(context)
        {
        }

        public override IQueryable<Album> QueryWithIncludes()
        {
            return base.DbSet.Include(a => a.Artist)
                .ThenInclude(a => a.Genres)
                .ThenInclude(g => g.Genre)
                .Include(a => a.Tracks);
        }
    }
}
