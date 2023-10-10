namespace Monsterkampfsimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: Yeah. If time left.

            //Console.ReadKey();


            SpawnManager spawnManager = new SpawnManager();
            List <Monster> monsters = spawnManager.Initialize();

            // sorts the monsters by speed. monsters[0] will always be the first attacking monster
            monsters.Sort((monsterA, monsterB) => monsterA.GetSpeed() > monsterB.GetSpeed() ? -1 : 1);

            int roundCount = 0;


            float initialHealthMonsterA = monsters[0].GetHealth();
            float initialHealthMonsterB = monsters[1].GetHealth();


            while (!monsters.Exists(monster => monster.GetHealth() <= 0f))
            {
                roundCount++;


                Monster attackingMonster = monsters[0];
                Monster targetMonster = monsters[1];

                attackingMonster.Attack(targetMonster);


                // switch list. So we toggle the actual attacking monster
                monsters.Reverse();

                /*
                 * After two rounds if theres no damage to any of the monsters we can
                 * abort the fight to not have an Infinite Loop. The fight would never end.
                 */
                if (roundCount == 2 && monsters[0].GetHealth() == initialHealthMonsterA && monsters[1].GetHealth() == initialHealthMonsterB)
                {
                    break;
                }
            }



            List<Monster> monstersWithHealth = monsters.FindAll(monster => monster.GetHealth() > 0f);


            // Draw condition (both monsters still have health)
            if (monstersWithHealth.Count() >= 2)
            {
                Output.WriteLineAtPosition("It's a draw!", Console.WindowWidth / 2);
            }
            else
            {
                Monster winningMonster = monstersWithHealth[0];

                /**
                 * Animate the winning monster to the center of the screen.
                 */
                Interpolation.AnimateLinear(winningMonster.GetPosition().X, Console.WindowWidth / 2, (int interpolatedX) =>
                {
                    Console.Clear();
                    winningMonster.Render(new Vector2(interpolatedX, winningMonster.GetPosition().Y));
                });

                Output.WriteLineAtPosition("The winner is " + winningMonster.GetRace() + "!", Console.WindowWidth / 2);
                Output.WriteLineAtPosition("This fight took " + roundCount + " rounds!", Console.WindowWidth / 2);

            }

            // TODO: Add ESC for quit, R for restart
            Console.ReadKey();

            Console.Clear();

            Main(args);
        }
    }
}

