using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPWebApp.Server.Models
{
    /// <summary>
    /// Model class representing a player with description, name and experience points.
    /// </summary>
    /// <param name="Id">The unique identifier of the player.</param>
    /// <param name="UserName">The name visible to other players.</param>
    /// <param name="XP">The amount of experience points gained.</param>
    /// <param name="Description">A description of the player.</param>
    [Table("players")]
    public class PlayerModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Xp { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }
}
