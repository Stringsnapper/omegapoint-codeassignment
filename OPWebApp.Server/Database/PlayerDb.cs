using Microsoft.EntityFrameworkCore;
using OPWebApp.Server.Models;

namespace OPWebApp.Server.Database
{

    public class PlayerDb : DbContext
    {
        public PlayerDb(DbContextOptions<PlayerDb> contextOptions) : base(contextOptions) {}

        public DbSet<PlayerModel> Players => Set<PlayerModel>();

    }
}
