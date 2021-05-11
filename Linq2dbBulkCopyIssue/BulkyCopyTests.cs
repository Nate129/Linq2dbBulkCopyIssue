using System;
using System.Collections.Generic;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Linq2dbBulkCopyIssue
{
    public class BulkyCopyTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BulkyCopyTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void EFCore_Insert_Saves_Guid_As_String()
        {
            var dbFactory = new EfCoreSqliteInMemoryDbFactory("DataSource=ef.db");
            var context = dbFactory.CreateDbContext<MainContext>(s => _testOutputHelper.WriteLine(s));

            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = "John Doe"
            };

            context.Add(person);
            context.SaveChanges();
        }

        [Fact]
        public void BulkCopy_Insert_Saves_Guid_As_String()
        {
            var dbFactory = new EfCoreSqliteInMemoryDbFactory("DataSource=linq2db.db");
            var context = dbFactory.CreateDbContext<MainContext>(s => _testOutputHelper.WriteLine(s));

            var connection = context.CreateLinqToDbConnection();
            var peopleTable = connection.GetTable<Person>();

            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = "John Doe"
            };

            peopleTable.BulkCopy(new BulkCopyOptions {KeepIdentity = true}, new List<Person> {person});
        }
    }
}
