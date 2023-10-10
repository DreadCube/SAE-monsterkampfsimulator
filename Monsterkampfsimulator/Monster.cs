namespace Monsterkampfsimulator
{
    public class Monster
    {
        // HP
        private float health;

        // AP
        private float attack;

        // DP
        private float defense;

        // S
        private float speed;

        // position of the monster
        private Vector2 position;

        // the healthbar instance of the monster
        private HealthBar healthBar;


        /**
		 * The race has to defined from the child class
		 */
        protected virtual Race race { get; }

        /**
		 * Represents all possible races
		 * a Monster can be
		 */
        public enum Race
        {
            Ork = 1,
            Troll = 2,
            Goblin = 3
        }

        protected Monster(float health, float attack, float defense, float speed, Vector2 position)
        {
            this.health = health;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
            this.position = position;
            healthBar = new HealthBar(health);
        }

        /**
		 * Every child clas of Monster (Ork, Troll, Goblin) has to define a image
		 * that will be rendered
		 */
        protected virtual void RenderImage(Vector2 position) { }

        /**
         * Getter for the health
         */
        public float GetHealth() => health;

        /**
         * Getter for the speed
         */
        public float GetSpeed() => speed;

        /**
         * Getter for the race of the monster
         */
        public Race GetRace() => race;

        /**
         * Getter for the current position of the monster 
         */
        public Vector2 GetPosition() => position;

        public void Attack(Monster targetMonster)
        {
            int movingDirectionX = position.X - targetMonster.position.X < 0 ? 1 : -1;

            /**
			 * Attack animation forward to the target
			 */
            Interpolation.AnimateLinear
            (
                position.X,
                targetMonster.position.X - (26 * movingDirectionX),
                (int currentPositionX) =>
                {
                    Console.Clear();
                    Render(new Vector2(currentPositionX, position.Y));
                    targetMonster.Render(targetMonster.position);
                }
            );

            // TODO: frage an Supi.
            // Eigener attack wert minusdefensiv wert des zu angreifendes Monsters i guess?
            // Muss angreifendes Monster auch schaden bekommen?
            float damage = Math.Max(0, attack - targetMonster.defense);
            targetMonster.health -= damage;
            targetMonster.health = Math.Max(0, targetMonster.health);
            targetMonster.healthBar.SetHealth(targetMonster.health);


            /**
			 * If theres damage to the target monster we render the target monster
			 * red for a split of a time
			 */
            if (damage > 0f)
            {
                Thread.Sleep(100);
                targetMonster.Render(targetMonster.position, ConsoleColor.Red);
                Thread.Sleep(100);
            }

            /**
			 * Attack animation back from the target
			 */
            Interpolation.AnimateLinear
            (
                targetMonster.position.X - (26 * movingDirectionX),
                position.X,
                (currentPositionX) =>
                {
                    Console.Clear();
                    Render(new Vector2(currentPositionX, position.Y));
                    targetMonster.Render(targetMonster.position);
                }
            );
        }

        /**
         * Renders the visual representation of the monster at the specified
         * render position. This includes:
         * - The monster image
         * - The monster healthbar
         * - The monster Stats
         */
        public void Render(Vector2 renderPosition, ConsoleColor imageForegroundColor = ConsoleColor.White)
        {
            // TODO: If no need for optional renderPosition, use renderPosition directly
            Vector2 pos = renderPosition;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = imageForegroundColor;

            RenderImage(pos);

            healthBar.Render(new Vector2(pos.X, Console.CursorTop));

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            Output.WriteLineAtPosition($"{race}", pos.X, Console.CursorTop + 1);


            Console.BackgroundColor = ConsoleColor.Black;

            Output.WriteLineAtPosition($"HEALTH: {Math.Round(health, 2)}", pos.X);
            Output.WriteLineAtPosition($"ATTACK: {Math.Round(attack, 2)}", pos.X);
            Output.WriteLineAtPosition($"DEFENSE: {Math.Round(defense, 2)}", pos.X);
            Output.WriteLineAtPosition($"SPEED: {Math.Round(speed, 2)}", pos.X);

            Console.ResetColor();
        }

    }
}