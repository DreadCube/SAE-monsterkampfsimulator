namespace Monsterkampfsimulator
{
    public class Monster
    {
        // The size of the monster
        private Size size = new Size(26, 19);

        // List of Attribute Transitions (Buffs)
        private List<AttributeTransition> attributeTransitions = new List<AttributeTransition>();

        // SP (Speed Points)
        private float speed;

        // position of the monster
        private Vector2 position;

        // HP (Health Points)
        protected float health;

        // AP (Attack Points)
        protected float attack;

        // DP (Defense Points)
        protected float defense;

        // the healthbar instance of the monster
        protected HealthBar healthBar;

        // The race has to defiend from the child class
        protected virtual Race race { get; }

        protected Random random = new Random();

        // Represents all possible races a Monster can be
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
            healthBar = new HealthBar(health, size.Width);
        }

        private float GetDefense() => defense;

        /// <summary>
        /// Handles a single Attribute Transition Animation at provided position
        /// and value.
        /// With Thread Sleep we simulate a blinking animation of the new Attribute value.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="value"></param>
        private void AnimateAttributeTransition(Vector2 position, float value)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;

                Console.CursorLeft = position.X;
                Console.CursorTop = position.Y;

                Thread.Sleep(100);

                Output.Write(" -> ", ConsoleColor.White);
                Console.BackgroundColor = ConsoleColor.DarkGray;

                Output.Write($"{Math.Round(value, 2)}", i % 2 == 1 ? ConsoleColor.Green : ConsoleColor.White);
            }
        }

        /// <summary>
        /// We wanna enforce that every child class of monster has a RenderImage
        /// method. To define a RenderImage implementation in the Monster class
        /// itself doesn't make sense, because we just have instances of the child classes.
        /// So defining the method here as virtual, so every child class can override.
        /// </summary>
        /// <param name="position">
        /// Child classes wil receive the position
        /// so the image can be rendered at the provided position.
        /// </param>
        protected virtual void RenderImage(Vector2 position) { }

        /// <summary>
        /// Default Damage Calculation.
        /// attacking monster (this) attack minus targetMonster defense gives us
        /// the actual damage.
        /// </summary>
        /// <param name="targetMonster"></param>
        /// <returns></returns>
        protected virtual float CalcDamage(Monster targetMonster)
        {
            return Math.Max(0, attack - targetMonster.GetDefense());
        }

        /// <summary>
        /// Applies damage to the monster (if damage is there).
        /// health of the monster will not go under 0.
        ///
        /// If theres damage to the target monster we render the target monster red
        /// for a split of a time
        /// </summary>
        /// <param name="damage">the damage to take</param>
        protected virtual void TakeDamage(float damage)
        {
            health = Math.Max(0, health - damage);

            healthBar.SetHealth(health);

            if (damage > 0f)
            {
                Thread.Sleep(100);
                Render(position, ConsoleColor.Red);
                Thread.Sleep(100);
            }
        }

        public float GetInitialHealth() => healthBar.GetInitialHealth();

        public float GetHealth() => health;

        public float GetSpeed() => speed;

        public Race GetRace() => race;

        public Vector2 GetPosition() => position;

        public Size GetSize() => size;

        /// <summary>
        /// Handles the attack logic.
        /// <list type="number">
        /// <item>Attacking monster will be forward animated to the target</item>
        /// <item>The actual damage will be calculated and applied</item>
        /// <item>The attacking monster will be animated backwards</item>
        /// </list>
        /// </summary>
        /// <param name="targetMonster">The target monster to attack</param>
        public void Attack(Monster targetMonster)
        {
            int offset = position.X < targetMonster.position.X ? size.Width : -size.Width;

            Vector2 targetPosition = new Vector2(targetMonster.position.X - offset, targetMonster.position.Y);

            // Attack animation forward to the target
            Interpolation.AnimateLinear
            (
                position,
                targetPosition,
                (Vector2 interpolatedPosition) =>
                {
                    Console.Clear();
                    Output.ShowBuffTable();
                    Render(interpolatedPosition);
                    targetMonster.Render(targetMonster.GetPosition());
                }
            );

            float damage = CalcDamage(targetMonster);
            Render(targetPosition);

            targetMonster.TakeDamage(damage);
            Render(targetMonster.GetPosition());

            // Attack animation back from the target
            Interpolation.AnimateLinear
            (
                targetPosition,
                position,
                (Vector2 interpolatedPosition) =>
                {
                    Console.Clear();
                    Output.ShowBuffTable();
                    Render(interpolatedPosition);
                    targetMonster.Render(targetMonster.GetPosition());
                }
            );
        }

        /// <summary>
        /// Renders the visual representation of the monster at the specified
        /// render position. This includes:
        /// <list type="bullet">
        /// <item>The monster image</item>
        /// <item>The monster healthbar</item>
        /// <item>The monster stats</item>
        /// <item>Buff informations</item>
        /// </list>
        /// </summary>
        /// <param name="renderPosition">Vector2 position</param>
        /// <param name="imageForegroundColor">rendered foreground color for the image</param>
        public void Render(Vector2 renderPosition, ConsoleColor imageForegroundColor = ConsoleColor.White)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = imageForegroundColor;

            RenderImage(renderPosition);

            healthBar.Render(new Vector2(renderPosition.X, Console.CursorTop));

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            Output.WriteLineAtPosition($"{race}", renderPosition.X, Console.CursorTop + 1, minWidth: size.Width);

            Console.BackgroundColor = ConsoleColor.DarkGray;

            string healthText = $"HEALTH: {Math.Round(health, 2)}";
            Output.WriteLineAtPosition(healthText, renderPosition.X, minWidth: size.Width);

            string attackText = $"ATTACK: {Math.Round(attack, 2)}";
            Output.WriteLineAtPosition(attackText, renderPosition.X, minWidth: size.Width);

            string defenseText = $"DEFENSE: {Math.Round(defense, 2)}";
            Output.WriteLineAtPosition(defenseText, renderPosition.X, minWidth: size.Width);

            string speedText = $"SPEED: {Math.Round(speed, 2)}";
            Output.WriteLineAtPosition(speedText, renderPosition.X, minWidth: size.Width);

            Vector2 position;

            /*
             * Render the Attribute Transitions (if existing) out to the Attribute Stats 
             */
            foreach (AttributeTransition transition in attributeTransitions)
            {
                if (transition.Message != null)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Output.WriteLineAtPosition(transition.Message, renderPosition.X, renderPosition.Y + size.Height, size.Width);
                }

                switch (transition.AttributeName)
                {
                    case Attribute.Health:
                        position = new Vector2(renderPosition.X + healthText.Length, renderPosition.Y + size.Height - 4);
                        AnimateAttributeTransition(position, transition.Value);
                        break;

                    case Attribute.Attack:
                        position = new Vector2(renderPosition.X + attackText.Length, renderPosition.Y + size.Height - 3);
                        AnimateAttributeTransition(position, transition.Value);
                        break;

                    case Attribute.Defense:
                        position = new Vector2(renderPosition.X + defenseText.Length, renderPosition.Y + size.Height - 2);
                        AnimateAttributeTransition(position, transition.Value);
                        break;
                    default:
                        position = new Vector2(renderPosition.X + speedText.Length, renderPosition.Y + size.Height - 1);
                        AnimateAttributeTransition(position, transition.Value);
                        break;
                }
                transition.OnTransitionEnd();
            }

            // Cleanup Transitions after end
            attributeTransitions.Clear();

            Console.ResetColor();
        }

        public void AddAttributeTransition(AttributeTransition attributeTransition)
        {
            attributeTransitions.Add(attributeTransition);
        }
    }
}