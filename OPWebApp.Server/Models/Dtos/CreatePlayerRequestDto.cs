using Newtonsoft.Json;

namespace OPWebApp.Server.Models.Dtos
{
    /// <summary>
    /// DTO for creating new player entries.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public record class CreatePlayerRequestDto(
        [property: JsonProperty("name")] string Name,
        [property: JsonProperty("description")] string Description);
}
