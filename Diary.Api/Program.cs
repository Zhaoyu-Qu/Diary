using Diary.DataContext;
using Diary.Services;
using Diary.Services.Database;
using Diary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<DiaryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DiaryDatabase")));
builder.Services.AddOpenApi();
// registers the controller services and enables dependency injection for controllers.
builder.Services.AddControllers();
// registers the NoteService class as the implementation for the INoteService interface in the dependency injection container.
builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

// Ensure database is created at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DiaryDbContext>();
    DatabaseInitialiser.InitialiseDatabase(dbContext);
}

// enable Swagger UI
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.MapGet("/home", () => "Welcome to the Diary API");

// maps the controller endpoints to the routing system so that HTTP requests are routed to your controller actions.
app.MapControllers();

app.Run();