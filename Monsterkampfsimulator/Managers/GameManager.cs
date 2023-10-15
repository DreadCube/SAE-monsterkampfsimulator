namespace Monsterkampfsimulator
{
    public class GameManager
    {
        public static GameManager Instance = new GameManager();

        private GameManager()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Quit()
        {
            Environment.Exit(0);
        }

        private void WaitForUserInput()
        {
            Output.WriteLineAtPosition("Press Esc to quit, R to restart", Console.WindowWidth / 2);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Escape)
                    {
                        Quit();
                    }

                    if (key == ConsoleKey.R)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Start();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the fight is still running.
        /// Will return false if the fight is no more running.
        /// true in the other hand.
        /// The fight is no more running if one of following conditions happend:
        /// <list type="bullet">
        ///     <item>In round 2 if both monsters still have the initial health we have a draw</item>
        ///     <item>If one of the monsters has zero health</item>
        /// </list>
        /// We output proper messages to the user if the fight stopped.
        /// </summary>
        /// <param name="monsters"> The fighting monsters</param>
        /// <param name="roundCount">The current roundCount</param>
        /// <returns></returns>
        private bool isFightRunning(List<Monster> monsters, int roundCount)
        {
            /*
             * After two rounds if theres no damage to any of the monsters we
             * have a draw.
             */
            if (roundCount == 2 && monsters[0].GetHealth() == monsters[0].GetInitialHealth() && monsters[1].GetHealth() == monsters[1].GetInitialHealth())
            {
                Output.WriteLineAtPosition("It's a draw!", Console.WindowWidth / 2);
                return false;
            }

            if (monsters.Exists(monster => monster.GetHealth() <= 0f))
            {
                Monster? winningMonster = monsters.Find(monster => monster.GetHealth() > 0f);
                if (winningMonster != null)
                {
                    Interpolation.AnimateLinear(
                      winningMonster.GetPosition(),
                      new Vector2(Console.WindowWidth / 2, Console.WindowHeight / 2),
                      (Vector2 interpolatedPositionX) =>
                      {
                          Console.Clear();
                          winningMonster.Render(interpolatedPositionX);
                      });


                    Output.WriteLineAtPosition("The winner is " + winningMonster.GetRace() + "!", Console.WindowWidth / 2);
                    Output.WriteLineAtPosition("This fight took " + roundCount + " rounds!", Console.WindowWidth / 2);

                    return false;
                }
            }


            return true;
        }

        // Todo: Set Console Width and height...
		public void Start()
		{
            List<Monster> monsters = SpawnManager.Instance.Initialize();

            int roundCount = 0;

            while (isFightRunning(monsters, roundCount))
            {
                roundCount++;

                Monster attackingMonster = monsters[0];
                Monster targetMonster = monsters[1];

                attackingMonster.Attack(targetMonster);


                // switch list. So we toggle the actual attacking monster
                monsters.Reverse();
            }

            WaitForUserInput();
        }
	}
}

