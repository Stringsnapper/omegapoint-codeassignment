using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OPWebApp.Server.Database;
using OPWebApp.Server.Models;
using OPWebApp.Server.Models.Dtos;

namespace OPWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerDb _context;

        public PlayersController(PlayerDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all players in the database.
        /// </summary>
        /// <remarks>GET: api/Player</remarks>
        /// <returns>All available Player data or an empty collection if no player data is available.</returns>
        /// <response code="200">Returns a collection of Player data.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerModel>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Player/{<GUID>}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerModel>> GetPlayerModel(Guid id)
        {
            var playerModel = await _context.Players.FindAsync(id);

            if (playerModel == null)
            {
                return NotFound();
            }

            return playerModel;
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerModel(Guid id, PlayerModel playerModel)
        {
            if (id != playerModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerModel>> PostPlayerModel(CreatePlayerRequestDto playerDto)
        {
            var playerModel = new PlayerModel { Name = playerDto.Name, Description = playerDto.Description };
            _context.Players.Add(playerModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerModel", new { id = playerModel.Id }, playerModel);
        }

        // DELETE: api/Player/5
        // TODO: Require authorization
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerModel(Guid id)
        {
            var playerModel = await _context.Players.FindAsync(id);
            if (playerModel == null)
            {
                return NotFound();
            }

            _context.Players.Remove(playerModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerModelExists(Guid id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
