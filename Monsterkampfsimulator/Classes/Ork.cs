namespace Monsterkampfsimulator
{
    public class Ork : Monster
    {
        private int roundsSinceBuff = 0;

        protected override Race race
        {
            get
            {
                return Race.Ork;
            }
        }

        public Ork(float health, float attack, float defense, float speed, Vector2 position)
            : base(health, attack, defense, speed, position)
        {
        }

        protected override void RenderImage(Vector2 position)
        {
            // parts copied from: https://ascii.co.uk/art/ogre
            //////////////////////////
            //                      //
            //                      //
            //    __,='`````'=/__   //
            //   '//  (o) \(o) \ `' //
            //   //|     ,_)   (`\  //
            // ,-~~~\  `'==='  /-,  //
            ///        `----'     `\//
            //                      //
            //                      //
            //                      //
            //                      //
            //////////////////////////

            Output.WriteLineAtPosition("//////////////////////////", position.X, position.Y);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 1);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 2);
            Output.WriteLineAtPosition("//    __,='`````'=/__   //", position.X, position.Y + 3);
            Output.WriteLineAtPosition("//   '//  (o) \\(o) \\ `' //", position.X, position.Y + 4);
            Output.WriteLineAtPosition("//   //|     ,_)   (`\\  //", position.X, position.Y + 5);
            Output.WriteLineAtPosition("// ,-~~~\\  `'==='  /-,  //", position.X, position.Y + 6);
            Output.WriteLineAtPosition("///        `----'     `\\//", position.X, position.Y + 7);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 8);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 9);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 10);
            Output.WriteLineAtPosition("//                      //", position.X, position.Y + 11);
            Output.WriteLineAtPosition("//////////////////////////", position.X, position.Y + 12);
        }

        /// <summary>
        /// Orks buff is a critical hit. The changes are 25%. In this case
        /// the damage is Orks Attack. But his defense goes zo zero afterwards.
        /// </summary>
        /// <param name="targetMonster"></param>
        /// <returns></returns>
        protected override float CalcDamage(Monster targetMonster)
        {
            if (random.Next(0, 4) == 0)
            {
                AddAttributeTransition(new AttributeTransition(Attribute.Defense, 0, "Buff: Critical hit!", () =>
                {
                    defense = 0f;
                }));
                return attack;
            }

            return base.CalcDamage(targetMonster);
        }
    }
}
