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
        /// <remarks>GET: api/players</remarks>
        /// <returns>All available Player data or an empty collection if no player data is available.</returns>
        /// <response code="200">Returns a collection of Player data.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerModel>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/players/9b624c6f-084a-40e4-98e4-de28e198cd84
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


        // PUT: api/players/9b624c6f-084a-40e4-98e4-de28e198cd84
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

        /// <summary>
        /// Endpoint for modifying the player xp value.
        /// </summary>
        /// <param name="id">the unique Identifier of the player model</param>
        /// <param name="dto">Data transfer object to hold the new XP value</param>
        /// <returns>IActionResult with status 204 on success, or status 404 </returns>
        // PATCH: api/9b624c6f-084a-40e4-98e4-de28e198cd84/xp
        [HttpPatch("{id}/xp")]
        public async Task<IActionResult> ModifyPlayerXp(Guid id, ModifyPlayerXpRequestDto dto)
        {
            var playerModel = await _context.Players.FindAsync(id);

            if (playerModel == null)
            {
                return NotFound();
            }

            playerModel.XP = dto.Xp;
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

        // POST: api/players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerModel>> PostPlayerModel(CreatePlayerRequestDto playerDto)
        {
            var playerModel = new PlayerModel { Name = playerDto.Name, Description = playerDto.Description };
            _context.Players.Add(playerModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerModel", new { id = playerModel.Id }, playerModel);
        }

        // DELETE: api/players/9b624c6f-084a-40e4-98e4-de28e198cd84
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
