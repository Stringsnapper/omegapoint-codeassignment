namespace OPWebApp.Server.Models.Dtos
{
    public record class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Xp { get; set; } = 0;
        public int Level => LevelCalculator.CalculateLevel(Xp);
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Maps a <see cref="PlayerModel"/> to a <see cref="PlayerDto"/> 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PlayerDto FromModel(PlayerModel p)
        {
            return new PlayerDto()
            {
                Id = p.Id,
                Name = p.Name,
                Xp = p.Xp,
                Description = p.Description
            };
        }
    }
}
