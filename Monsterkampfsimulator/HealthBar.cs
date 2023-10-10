namespace Monsterkampfsimulator
{
    public class HealthBar
    {
        private float initialHealth;
        private float currentHealth;

        public HealthBar(float initialHealth)
        {
            this.initialHealth = initialHealth;
            currentHealth = initialHealth;
        }

        public void SetHealth(float health) => currentHealth = health;


        /**
         * Rendering (visual representation) the healthbar
         */
        public void Render(Vector2 position)
        {
            Console.SetCursorPosition(position.X, position.Y);

            // Determines how many green blocks we have to render (basically percentage of health left)
            // We are rounding up. 0.1 should render 1 green Block Count. Only absolute 0 should be greenBlockCount = 0;
            uint greenBlockCount = (uint)Math.Ceiling((26 / initialHealth) * currentHealth);

            for (byte i = 1; i <= 26; i++)
            {
                Console.ForegroundColor = i <= greenBlockCount ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write("█");
            }
            Console.ResetColor();
        }
    }
}

