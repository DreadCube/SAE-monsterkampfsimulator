namespace Monsterkampfsimulator
{
    public class Ork : Monster
    {
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
    }
}
