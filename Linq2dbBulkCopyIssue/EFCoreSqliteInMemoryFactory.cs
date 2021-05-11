using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Linq2dbBulkCopyIssue
{
    public sealed class EfCoreSqliteInMemoryDbFactory : IDisposable, IAsyncDisposable
    {
        private readonly string _connectionString;
        private SqliteConnection _connection;

        public EfCoreSqliteInMemoryDbFactory()
        {
            _connectionString = "DataSource=:memory:";
        }

        public EfCoreSqliteInMemoryDbFactory(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async ValueTask DisposeAsync()
        {
            await Task.Run(Dispose);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T CreateDbContext<T>(Action<string> logger = null) where T : DbContext
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<DbContext>().UseSqlite(_connection);

            if (logger != null)
                optionsBuilder.LogTo(logger);

            var context = (T) Activator.CreateInstance(typeof(T), optionsBuilder.Options);

            context?.Database.EnsureCreated();

            return context;
        }

        private void ReleaseUnmanagedResources()
        {
            _connection.Dispose();
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();

            if (disposing)
            {
            }
        }

        ~EfCoreSqliteInMemoryDbFactory()
        {
            Dispose(false);
        }
    }
}
