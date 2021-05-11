using Microsoft.EntityFrameworkCore;

namespace Linq2dbBulkCopyIssue
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
