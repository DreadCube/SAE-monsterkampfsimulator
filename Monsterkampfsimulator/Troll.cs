namespace Monsterkampfsimulator
{
	public class Troll: Monster
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

        // TODO: Would be cool if we can have a actual width for the Monster
        // Than just have to define everywhere 26.
        protected override void RenderImage(Vector2 position)
        {
            // Inspired by : https://ascii.co.uk/art/gnome
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
    }
}