using GraphQLInDotNet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    internal class GenreDataSet : DataSet<Genre>
    {
        public GenreDataSet(DomainDbContext context) : base(context)
        {
        }

        public override IQueryable<Genre> QueryWithIncludes()
        {
            return base.DbSet.Include(a => a.Artists)
                .ThenInclude(al => al.Artist);
        }
    }
}
