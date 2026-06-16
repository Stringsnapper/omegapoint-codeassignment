using Microsoft.EntityFrameworkCore;
using OPWebApp.Server.Models;
using OPWebApp.Server.Models.Dtos;
using OPWebApp.Server.Repositories;
using System.Diagnostics;

namespace OPWebApp.Server.Services
{
    public class PlayerService(PlayerDb dbContext)
    {
        private readonly PlayerDb _dbContext = dbContext;

        public async Task<PlayerDto[]> GetPlayersAsync()
        {
            return await _dbContext.Players.Select(p => PlayerDto.FromModel(p)).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<PlayerDto?> GetPlayerByIdAsync(Guid id)
        {
            var playerModel = await _dbContext.Players.FindAsync(id).ConfigureAwait(false);
            return playerModel == null ? null : PlayerDto.FromModel(playerModel);
        }

        public async Task UpdatePlayerAsync(Guid id, PlayerDto playerDto)
        {
            var playerModel = await _dbContext.Players.FindAsync(id).ConfigureAwait(false);
            if (playerModel == null)
            {
                throw new PlayerNotFoundException($"Failed to retrieve player with id {id}");
            }

            playerModel.Name = playerDto.Name;
            playerModel.Xp = playerDto.Xp;
            playerModel.Description = playerDto.Description;
            await UpdatePlayerAsync(id, playerModel);
        }

        public async Task UpdatePlayerXp(Guid id, ModifyPlayerXpRequestDto dto)
        {
            var playerModel = await _dbContext.Players.FindAsync(id).ConfigureAwait(false);
            if (playerModel == null)
            {
                throw new PlayerNotFoundException($"Failed to retrieve player with id {id}");
            }
            playerModel.Xp = dto.Xp;

            await UpdatePlayerAsync(id, playerModel).ConfigureAwait(false);
        }

        private async Task UpdatePlayerAsync(Guid id, Models.PlayerModel playerModel)
        {
            _dbContext.Entry(playerModel).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerModelExists(id))
                {
                    throw new PlayerNotFoundException($"Player with id {id} does not exist in the database.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<PlayerDto> CreatePlayerAsync(CreatePlayerRequestDto requestDto)
        {
            var playerModel = new PlayerModel { Name = requestDto.Name, Description = requestDto.Description };
            _dbContext.Players.Add(playerModel);
            var numberOfEntries = await _dbContext.SaveChangesAsync();
            if(numberOfEntries < 1)
            {
                throw new PlayerCreationException("Failed to create new player");
            }
            return PlayerDto.FromModel(playerModel);

        }

        public async Task DeletePlayerAsync(Guid id)
        {
            var playerModel = await _dbContext.Players.FindAsync(id);
            if (playerModel == null)
            {
                throw new PlayerNotFoundException($"Could not find Player with id {id}");
            }

            _dbContext.Players.Remove(playerModel);
            await _dbContext.SaveChangesAsync();
        }

        private bool PlayerModelExists(Guid id)
        {
            return _dbContext.Players.Any(e => e.Id == id);
        }
    }

    public class PlayerNotFoundException(string msg) : Exception(msg);
    public class PlayerCreationException(string msg) : Exception(msg);

    
}
