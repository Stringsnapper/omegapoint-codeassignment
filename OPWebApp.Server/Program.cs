using Microsoft.EntityFrameworkCore;
using OPWebApp.Server.Repositories;
using OPWebApp.Server.Services;
using System.Threading.RateLimiting;

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

// Add rate limiting to prevent DDos attacks
// Limits are set to 10 requests per minute
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 10,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));
});

builder.Services.AddHttpLogging(o => { });

// Use in-memory database during development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PlayerDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Players") ?? throw new InvalidOperationException("Connection string 'Players' not found")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<PlayerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(allowSpecificOriginsPolicy);
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();
