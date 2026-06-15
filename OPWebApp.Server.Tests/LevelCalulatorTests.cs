using Xunit.Abstractions;

namespace OPWebApp.Server.Tests
{
    public class LevelCalulatorTests(ITestOutputHelper outputHelper)
    {
        private ITestOutputHelper _outputHelper = outputHelper;

        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(20)]
        [InlineData(150)]
        [InlineData(200)]
        [InlineData(3000)]
        [InlineData(124856)]
        public void CalculateLevel_PositiveOrZeroXpValues_SuccessfullyReturnsPositiveInteger(int xp)
        {
            _outputHelper.WriteLine($"Calculate level with xp value {xp}");

            // Act
            var level = LevelCalculator.CalculateLevel(xp);
            _outputHelper.WriteLine($"LevelCalculator returned Level value of {level}");

            // Assert
            Assert.True( level > 0 );
            _outputHelper.WriteLine($"Verified: Retuned level value is larger than 0: xp {xp} -> level {level}");
        }
    }
}
