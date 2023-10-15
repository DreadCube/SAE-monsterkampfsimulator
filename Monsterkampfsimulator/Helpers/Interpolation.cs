namespace Monsterkampfsimulator
{
    /**
     * Represents helper functions
     * for interpolation between 2 points.
     * 
     * Inspired by:
     * https://en.wikipedia.org/wiki/Linear_interpolation
     */
    public static class Interpolation
    {
        /// <summary>
        /// Calculates point between two given points and distance amount.
        ///
        /// <example> 
        /// Example:
        /// <code>
        ///  Linear(10f, 20f, 0.5f); // result is 15f;
        /// </code>
        /// </example>
        /// </summary>
        /// 
        /// <param name="by">distance amount (between 0f and 1f)</param>
        private static float Linear(float from, float to, float by)
        {
            return from * (1 - by) + to * by;
        }

        /// <summary>
        /// Calculates point between two given points and distance amount.
        /// <example> 
        /// Example:
        /// <code>
        ///  Linear(10, 20, 0.5f); // result is 15;
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="by">distance amount (between 0f and 1f)</param>
        private static int Linear(int from, int to, float by)
        {
            float value = Linear(from + 0f, to + 0f, by);

            return (int)Math.Round(value);
        }

        /// <summary>
        /// A linear interpolation animator that can be used to provide
        /// a animation for moving between 2 points.
        ///
        /// A callback will be executed for every frame.
        /// The callback receives the interpolated Position at the current frame.
        /// </summary>
        public static void AnimateLinear(Vector2 from, Vector2 to, Action<Vector2> frameCallback, uint frames = 20, int frameTime = 50)
        {
            for (uint i = 1; i <= frames; i++)
            {
                float by = (1f / frames) * i;
                int interpolatedPositionX = Linear(from.X, to.X, by);
                int interpolatedPositionY = Linear(from.Y, to.Y, by);

                frameCallback(new Vector2(interpolatedPositionX, interpolatedPositionY));

                Thread.Sleep(frameTime);
            }
        }
    }
}

