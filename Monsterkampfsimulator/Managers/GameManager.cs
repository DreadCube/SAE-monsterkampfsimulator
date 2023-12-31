﻿namespace Monsterkampfsimulator
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
            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.CursorTop);
            Output.Write("Press ");
            Output.Write("Esc", ConsoleColor.Green);
            Output.Write(" to quit, ");
            Output.Write("R", ConsoleColor.Green);
            Output.Write(" to restart");

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
        ///     <item>If one of the monsters has zero health</item>
        /// </list>
        /// We output proper messages to the user if the fight stopped.
        /// </summary>
        /// <param name="monsters"> The fighting monsters</param>
        /// <param name="roundCount">The current roundCount</param>
        /// <returns></returns>
        private bool IsFightRunning(List<Monster> monsters, int roundCount)
        {
            if (monsters.Exists(monster => monster.GetHealth() <= 0f))
            {
                Monster? winningMonster = monsters.Find(monster => monster.GetHealth() > 0f);
                if (winningMonster != null)
                {
                    Vector2 targetPosition = new Vector2(
                        Console.WindowWidth / 2 - winningMonster.GetSize().Width / 2,
                        Console.WindowHeight / 2 - winningMonster.GetSize().Height / 2
                    );

                    Interpolation.AnimateLinear(
                      winningMonster.GetPosition(),
                      targetPosition,
                      (Vector2 interpolatedPositionX) =>
                      {
                          Console.Clear();
                          winningMonster.Render(interpolatedPositionX);
                      }
                    );

                    Output.WriteLineAtPosition($"The winner is {winningMonster.GetRace()}!", targetPosition.X);
                    Output.WriteLineAtPosition($"This fight took {roundCount} rounds!", targetPosition.X);

                    return false;
                }
            }


            return true;
        }

        public void Start()
        {
            Console.SetWindowSize(Console.WindowWidth, 50);
            Output.ShowBuffTable();
            List<Monster> monsters = SpawnManager.Instance.Initialize();

            int roundCount = 0;

            while (IsFightRunning(monsters, roundCount))
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

