using Microsoft.EntityFrameworkCore;

namespace Diary.Services.Database;

public static class DatabaseInitialiser
{
    public static void InitialiseDatabase(DbContext context)
    {
        context.Database.EnsureCreated();
    }
}