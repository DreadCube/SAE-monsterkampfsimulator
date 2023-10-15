﻿namespace Monsterkampfsimulator
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


        // The size of the monster
        private Size size = new Size(26, 19);

        
        // The race has to defiend from the child class
        protected virtual Race race { get; }

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

        /// <summary>
        /// Applies damage to the monster (if damage is there).
        /// health of the monster will not go under 0.
        ///
        /// If theres damage to the target monster we render the target monster red
        /// for a split of a time
        /// </summary>
        /// <param name="damage">the damage to take</param>
        private void TakeDamage(float damage)
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

        private float GetDefense() => defense;

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
                    Render(interpolatedPosition);
                    targetMonster.Render(targetMonster.GetPosition());
                }
            );


            float damage = Math.Max(0, attack - targetMonster.GetDefense());
            targetMonster.TakeDamage(damage);

            // Attack animation back from the target
            Interpolation.AnimateLinear
            (
                targetPosition,
                position,
                (Vector2 interpolatedPosition) =>
                {
                    Console.Clear();
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

            Output.WriteLineAtPosition($"HEALTH: {Math.Round(health, 2)}", renderPosition.X, minWidth: size.Width);
            Output.WriteLineAtPosition($"ATTACK: {Math.Round(attack, 2)}", renderPosition.X, minWidth: size.Width);
            Output.WriteLineAtPosition($"DEFENSE: {Math.Round(defense, 2)}", renderPosition.X, minWidth: size.Width);
            Output.WriteLineAtPosition($"SPEED: {Math.Round(speed, 2)}", renderPosition.X, minWidth: size.Width);

            Console.ResetColor();
        }
    }
}