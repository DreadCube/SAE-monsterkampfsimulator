namespace Monsterkampfsimulator
{
    public class HealthBar
    {
        private float initialHealth;
        private float currentHealth;

        private int healthBarWidth;

        public HealthBar(float initialHealth, int healthBarWidth)
        {
            this.initialHealth = initialHealth;
            currentHealth = initialHealth;

            this.healthBarWidth = healthBarWidth;
        }

        public void SetHealth(float health) => currentHealth = health;

        /// <summary>
        /// Renders the visual representation of the healthbar
        /// </summary>
        /// <param name="position">render position</param>
        public void Render(Vector2 position)
        {
            Console.SetCursorPosition(position.X, position.Y);

            // Determines how many green blocks we have to render (basically percentage of health left)
            // We are rounding up. 0.1 should render 1 green Block Count. Only absolute 0 should be greenBlockCount = 0;
            uint greenBlockCount = (uint)Math.Ceiling((healthBarWidth / initialHealth) * currentHealth);

            for (byte i = 1; i <= healthBarWidth; i++)
            {
                Console.ForegroundColor = i <= greenBlockCount ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write("█");
            }
            Console.ResetColor();
        }
    }
}

