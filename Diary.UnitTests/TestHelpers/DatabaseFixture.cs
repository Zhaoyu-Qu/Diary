using Diary.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Diary.UnitTests.TestHelpers
{
    public class DatabaseFixture : IDisposable
    {
        public DiaryDbContext Context { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DiaryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDiaryDb")
                .Options;

            Context = new DiaryDbContext(options);

            // Ensure database is created
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}