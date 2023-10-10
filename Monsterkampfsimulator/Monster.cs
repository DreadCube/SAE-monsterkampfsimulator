namespace Monsterkampfsimulator
{
    public class Monster
    {
        // HP (Health Points)
        private float health;

        // AP (Attack Points)
        private float attack;

        // DP (Defense Points)
        private float defense;

        // SP (Speed Points)
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
         * Applies damage to the monster (if damage is there)
         */
        private void TakeDamage(float damage)
        {
            health = Math.Max(0, health - damage);
            healthBar.SetHealth(health);

            /**
             * If theres damage to the target monster we render the target monster
             * red for a split of a time
             */
            if (damage > 0f)
            {
                Thread.Sleep(100);
                Render(position, ConsoleColor.Red);
                Thread.Sleep(100);
            }
        }

        /**
         * Getter for the defense
         */ 
        private float GetDefense() => defense;

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


        /**
         * Handles the attack logic.
         * 1. Attacking Monster wll be forward animated to the target
         * 2. The actual damage will be calculated and applied
         * 3. The Attacking monster will be moved backwards
         */
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



            float damage = Math.Max(0, attack - targetMonster.GetDefense());
            targetMonster.TakeDamage(damage);


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