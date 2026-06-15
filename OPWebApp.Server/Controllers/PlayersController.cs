using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OPWebApp.Server.Database;
using OPWebApp.Server.Models;
using OPWebApp.Server.Models.Dtos;
using OPWebApp.Server.Services;

namespace OPWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController(PlayerService playerService) : ControllerBase
    {
        private readonly PlayerService _playerService = playerService;

        /// <summary>
        /// Returns all players in the database.
        /// </summary>
        /// <remarks>GET: api/players</remarks>
        /// <returns>All available Player data or an empty collection if no player data is available.</returns>
        /// <response code="200">Returns a collection of Player data.</response>
        [HttpGet]
        public async Task<ActionResult<PlayerDto[]>> GetPlayers()
        {
            return await _playerService.GetPlayersAsync().ConfigureAwait(false);
        }

        // GET: api/players/9b624c6f-084a-40e4-98e4-de28e198cd84
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayer(Guid id)
        {
           var responseDto = await _playerService.GetPlayerByIdAsync(id).ConfigureAwait(false);

            return responseDto == null ? NotFound() : responseDto;
        }


        // PUT: api/players/9b624c6f-084a-40e4-98e4-de28e198cd84
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerModel(Guid id, PlayerDto playerDto)
        {
            if (id != playerDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _playerService.UpdatePlayerAsync(id, playerDto);
            }
            catch (PlayerNotFoundException)
            {
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return Problem();
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
        public async Task<IActionResult> ModifyPlayerXp(Guid id, [FromBody] ModifyPlayerXpRequestDto dto)
        {
            try
            {
                await _playerService.UpdatePlayerXp(id, dto);
            }
            catch (PlayerNotFoundException)
            {
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return Problem();
            }

            return NoContent();
        }

        // POST: api/players
        [HttpPost]
        public async Task<ActionResult<PlayerDto>> PostPlayer(CreatePlayerRequestDto playerRequestDto)
        {
            try
            {
                var responseDto = await _playerService.CreatePlayerAsync(playerRequestDto).ConfigureAwait(false);
                return CreatedAtAction("GetPlayer", new { id = responseDto.Id }, responseDto);
            }
            catch (Exception)
            {
                return Problem();
            }

        }

        // DELETE: api/players/9b624c6f-084a-40e4-98e4-de28e198cd84
        // TODO: Require authorization
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerModel(Guid id)
        {
            try
            {
                await _playerService.DeletePlayerAsync(id).ConfigureAwait(false);
                return NoContent();
            } catch (PlayerNotFoundException)
            {
                return NotFound();
            } catch
            {
                return Problem();
            }
        }

        
    }
}
