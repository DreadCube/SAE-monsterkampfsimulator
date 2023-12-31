﻿using static System.Net.Mime.MediaTypeNames;

namespace Monsterkampfsimulator
{
    public class Troll : Monster
    {
        protected override Race race
        {
            get
            {
                return Race.Troll;
            }
        }

        public Troll(float health, float attack, float defense, float speed, Vector2 position)
            : base(health, attack, defense, speed, position)
        {
        }

        protected override void RenderImage(Vector2 position)
        {
            // parts copied from: https://ascii.co.uk/art/gnome
            //            __
            //         .-'  |
            //        /   <\|
            //       /     \'
            //       |_.- o-o
            //       / C  -._)\
            //      /',        |
            //     |   `-,_,__,'

            Output.WriteLineAtPosition("//////////////////////////", position.X, position.Y);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 1);
            Output.WriteLineAtPosition("//            __        //", position.X, position.Y + 2);
            Output.WriteLineAtPosition("//         .-'  |       //", position.X, position.Y + 3);
            Output.WriteLineAtPosition("//        /   <\\|       //", position.X, position.Y + 4);
            Output.WriteLineAtPosition("//       /     \'        //", position.X, position.Y + 5);
            Output.WriteLineAtPosition("//       |_.- o-o       //", position.X, position.Y + 6);
            Output.WriteLineAtPosition("//       / C  -._)\\     //", position.X, position.Y + 7);
            Output.WriteLineAtPosition("//      /',        |    //", position.X, position.Y + 8);
            Output.WriteLineAtPosition("//     |   `-,_,__,'    //", position.X, position.Y + 9);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 10);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 11);
            Output.WriteLineAtPosition("//////////////////////////", position.X, position.Y + 12);
        }

        /// <summary>
        /// The Troll has a 33% change to perform his attack buff:
        ///  - steal 20% Health from enemy
        ///  - but lower its defense by stolen amount
        /// </summary>
        /// <param name="targetMonster"></param>
        /// <returns></returns>
        protected override float CalcDamage(Monster targetMonster)
        {
            float damage = base.CalcDamage(targetMonster);

            if (random.Next(0, 3) == 0)
            {
                float additionalDamage = targetMonster.GetHealth() * 0.2f;

                float newHealth = health + additionalDamage;
                float newDefense = defense - additionalDamage;

                AddAttributeTransition(new AttributeTransition(Attribute.Health, newHealth, "Buff: Steal Health", () =>
                {
                    health = newHealth;
                    healthBar.SetHealth(health);
                }));

                AddAttributeTransition(new AttributeTransition(Attribute.Defense, newDefense, "Buff: Reduce Defense", () =>
                {
                    defense = newDefense;
                }));

                return damage + additionalDamage;
            }

            return damage;
        }
    }
}