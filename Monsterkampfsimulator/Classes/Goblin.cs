﻿namespace Monsterkampfsimulator
{
    public class Goblin : Monster
    {
        protected override Race race
        {
            get
            {
                return Race.Goblin;
            }
        }

        public Goblin(float health, float attack, float defense, float speed, Vector2 position)
            : base(health, attack, defense, speed, position)
        {
        }


        protected override void RenderImage(Vector2 position)
        {
            // Copied from: https://ascii.co.uk/art/goblin
            //////////////////////////
            //       ,      ,       //
            //   / (.- "" -.)\      //
            //  |\  \/      \/  /|  //
            //  | \ / =.  .= \ / |  //
            //  \( \   o\/o   / )/  //
            //   \_, '-/  \-' , _/  //
            //     /   \__/   \     //
            //     \ \__/\__/ /     //
            //   ___\ \|--|/ /___   //
            // /`    \      /    `\ //
            ///       '----'       \//
            //////////////////////////

            Output.WriteLineAtPosition("//////////////////////////", position.X, position.Y);
            Output.WriteLineAtPosition("//       ,      ,       //", position.X, position.Y + 1);
            Output.WriteLineAtPosition("//   / (.- \"\" -.)\\      //", position.X, position.Y + 2);
            Output.WriteLineAtPosition("//  |\\  \\/      \\/  /|  //", position.X, position.Y + 3);
            Output.WriteLineAtPosition("//  | \\ / =.  .= \\ / |  //", position.X, position.Y + 4);
            Output.WriteLineAtPosition("//  \\( \\   o\\/o   / )/  //", position.X, position.Y + 5);
            Output.WriteLineAtPosition("//   \\_, '-/  \\-' , _/  //", position.X, position.Y + 6);
            Output.WriteLineAtPosition("//     /   \\__/   \\     //", position.X, position.Y + 7);
            Output.WriteLineAtPosition("//     \\ \\__/\\__/ /     //", position.X, position.Y + 8);
            Output.WriteLineAtPosition("//   ___\\ \\|--|/ /___   //", position.X, position.Y + 9);
            Output.WriteLineAtPosition("// /`    \\      /    `\\ //", position.X, position.Y + 10);
            Output.WriteLineAtPosition("///       '----'       \\//", position.X, position.Y + 11);
            Output.WriteLineAtPosition("//////////////////////////", position.X, position.Y + 12);
        }

        /// <summary>
        /// Goblin has a 50% change to receive no damage trough buff.
        /// But in this case his defense will be halfed.
        /// </summary>
        /// <param name="damage"></param>
        protected override void TakeDamage(float damage)
        {
            if (damage > 0f && random.Next(0, 2) == 0)
            {
                AddAttributeTransition(new AttributeTransition(Attribute.Defense, defense * 0.5f, "Buff: No damage!", () =>
                {
                    defense = defense * 0.5f;
                }));
                damage = 0;
            }
            base.TakeDamage(damage);
        }
    }
}