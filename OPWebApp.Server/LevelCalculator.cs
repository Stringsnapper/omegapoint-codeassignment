namespace OPWebApp.Server
{
    public static class LevelCalculator
    {
        // This could be made configurable instead but static function and a constant is fine for first implementation
        public const double LEVEL_CALC_MODIFYER = 0.4473;

        /// <summary>
        /// Calculates current level based on xp value.
        /// </summary>
        /// <remarks>
        /// Adds 
        /// </remarks>
        /// <param name="xp"></param>
        /// <returns>The calculated level value as a positive integer</returns>
        /// <exception cref="ArgumentException">If the provided xp value is less than 0.</exception>
        public static int CalculateLevel(int xp) 
        {
            if (xp < 0)
            {
                throw new ArgumentException("Xp needs to be a positive integer!");
            }
            
            return (int) Math.Floor(LEVEL_CALC_MODIFYER * Math.Sqrt(xp)) + 1;
        }
    }
}
    