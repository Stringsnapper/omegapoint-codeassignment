using Microsoft.EntityFrameworkCore;
using OPWebApp.Server.Database;

var allowSpecificOriginsPolicy = "_allowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add cross origin requests
builder.Services.AddCors(options =>
{
    // TODO: Add origins to appsettings.json
    options.AddPolicy(name: allowSpecificOriginsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Make configurable
                .AllowAnyHeader() // Ideally this could be restricted to necessary headers
                .AllowAnyMethod();
        });
});

builder.Services.AddHttpLogging(o => { });

// Use in-memory database during development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PlayerDb>(opt => opt.UseInMemoryDatabase("Players"));

var app = builder.Build();

if ( app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(allowSpecificOriginsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();
