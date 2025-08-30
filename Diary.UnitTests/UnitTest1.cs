using System.IO;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Diary.DataContext;
using Diary.Services.Database;

namespace Diary.UnitTests
{
    public class DatabaseInitialiserTests
    {
        [Fact]
        public void InitialiseDatabase_CreatesDatabaseFile()
        {
            // Arrange: use a unique temp file for the test
            var dbPath = Path.GetTempFileName() + ".db";
            var options = new DbContextOptionsBuilder<DiaryDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            using (var context = new DiaryDbContext(options))
            {
                // Act
                DatabaseInitialiser.InitialiseDatabase(context);
            }

            // Assert
            Assert.True(File.Exists(dbPath));
            
            // Cleanup
            File.Delete(dbPath);
        }
    }
}