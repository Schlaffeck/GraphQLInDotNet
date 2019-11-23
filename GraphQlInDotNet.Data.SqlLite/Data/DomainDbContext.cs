using Microsoft.EntityFrameworkCore;

namespace GraphQlInDotNet.Data.EntityFramework.Data
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options)
        : base(options)
        {
        }
    }
}
