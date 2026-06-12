using Newtonsoft.Json;

namespace OPWebApp.Server.Models.Dtos
{
    public record class ModifyPlayerXpRequestDto(
        [property: JsonProperty("xp")] int Xp);
}
