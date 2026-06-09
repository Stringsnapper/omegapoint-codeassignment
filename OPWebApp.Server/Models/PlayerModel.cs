using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPWebApp.Server.Models
{
    /// <summary>
    /// Model class representing a player with description and level.
    /// </summary>
    /// <param name="Id">The unique identifier of the player.</param>
    /// <param name="UserName">The name visible to other players.</param>
    /// <param name="Level">The current level of the player.</param>
    /// <param name="XP">The amount of experience points gained during the current player level.</param>
    /// <param name="Description">A description of the player.</param>
    [Table("players")]
    public class PlayerModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; } = 0;
        public int XP { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }
}
