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
        /**
         * Calculates absolute position based on
         * percentage between from and to points.
         * 
         * percentage is a value between 0f and 1f.
         * 
         * Example:
         * from = 10f
         * to = 20f
         * by = 0.5f
         * result = 15f
         */
        private static float Linear(float from, float to, float by)
        {
            return from * (1 - by) + to * by;
        }

        /**
         * Overload: Based on the upper float Linear helper func
         * but guarantees us an integer as output instead of
         * a float
         */
        private static int Linear(int from, int to, float by)
        {
            float value = Linear(from + 0f, to + 0f, by);

            return (int)Math.Round(value);
        }

        /**
         * A linear interpolation animator.
         * 
         * A callback will be executed for every frame.
         * The callback receives the interpolated position at the current frame.
         */
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

