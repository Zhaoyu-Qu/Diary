using Diary.DataContext;
using Diary.Services.Database;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// OpenAPI is a specification for describing RESTful APIs in a standardized, machine-readable format. In ASP.NET Core, adding OpenAPI support (often via Swagger) automatically generates interactive API documentation and a JSON schema for your endpoints. This helps developers and clients understand, test, and integrate with your API easily, without needing to read the source code. The lines in your Program.cs enable this documentation and UI for your web API.
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DiaryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DiaryDatabase")));; // Register DbContext

var app = builder.Build();

// Ensure database is created at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DiaryDbContext>();
    DatabaseInitialiser.InitialiseDatabase(dbContext);
}

// These lines check if the app is running in the Development environment. If so, they call app.MapOpenApi(), which exposes the OpenAPI (Swagger) endpoint for your API. This allows you to view and interact with the API documentation and test endpoints in development, but keeps it hidden in production for security reasons.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();