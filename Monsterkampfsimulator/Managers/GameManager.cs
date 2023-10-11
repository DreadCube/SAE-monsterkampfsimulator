﻿using System;
namespace Monsterkampfsimulator
{
    public class GameManager
    {
        public static GameManager Instance = new GameManager();



        public GameManager()
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

        private void WaitForInput()
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

        // Todo: Set Console Width and height...
		public void Start()
		{
            List<Monster> monsters = SpawnManager.Instance.Initialize();

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
            }


            WaitForInput();
        }
	}
}
