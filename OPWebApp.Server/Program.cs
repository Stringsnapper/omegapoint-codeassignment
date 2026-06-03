using Microsoft.EntityFrameworkCore;
using OPWebApp.Server.Database;
using OPWebApp.Server.Models;

var builder = WebApplication.CreateBuilder(args);
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();
