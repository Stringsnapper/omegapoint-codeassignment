using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OPWebApp.Server.Database;
using OPWebApp.Server.Models;

var builder = WebApplication.CreateBuilder(args);
// Use in-memory database during development
builder.Services.AddDbContext<PlayerDb>(opt => opt.UseInMemoryDatabase("Players"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Minimal api setup 
// TODO: Remove this in favor of controllers
app.MapGet("/players", async (PlayerDb db) => await db.Players.ToListAsync());

app.MapPost("/players", async (PlayerModel player, PlayerDb db) => 
{
    db.Players.Add(player);
    await db.SaveChangesAsync();

    return Results.Created($"/players/{player.Id}", player);
});

app.MapPut("/players/{id}", async (Guid id, PlayerModel inputPlayer, PlayerDb db) =>
{
    var player = await db.Players.FindAsync(id);
    if (player is null) return Results.NotFound();

    player.Name = inputPlayer.Name;
    player.Level = inputPlayer.Level;
    player.XP = inputPlayer.XP;
    player.Description = inputPlayer.Description;

    return Results.NoContent();
});

app.MapDelete("/players/{id}", async (Guid id, PlayerDb db) =>
{
    if (await db.Players.FindAsync(id) is PlayerModel player)
    {
        db.Remove(player);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});



app.Run();
