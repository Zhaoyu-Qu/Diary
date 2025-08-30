using Microsoft.EntityFrameworkCore;
using Diary.EntityModels;

namespace Diary.DataContext
{
    public class DiaryDbContext : DbContext
    {
        public DiaryDbContext(DbContextOptions<DiaryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Diary.db");
            }
        }
    }
}