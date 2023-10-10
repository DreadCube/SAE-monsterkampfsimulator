using System;

namespace Monsterkampfsimulator
{
	public class SpawnManager
	{
		private static SpawnManager? Instance;

		public SpawnManager()
		{
			if (Instance == null)
			{
				Instance = this;
			}
		}

        // TODO: Maybe create a SpawnManager Singleton class for this
        private Monster CreateMonster(Vector2 position, Monster.Race? notAllowedRace = null)
        {
            Monster.Race race = GetRaceInput(notAllowedRace);

            float health = GetFloatInput("Enter Health", 1);
            float attack = GetFloatInput("Enter Attack");
            float defense = GetFloatInput("Enter Defense");
            float speed = GetFloatInput("Enter Speed", 1);

            switch (race)
            {
                case Monster.Race.Goblin:
                    return new Goblin(health, attack, defense, speed, position);
                case Monster.Race.Ork:
                    return new Ork(health, attack, defense, speed, position);
                default:
                    return new Troll(health, attack, defense, speed, position);
            }
        }

        private float GetFloatInput(string message, float minValue = 0f, float maxValue = 100f)
        {
            string errorMessage = "";

            while (true)
            {
                Output.Write($"{message} ", ConsoleColor.Cyan);

                Output.Write("(minValue: ");
                Output.Write(minValue, ConsoleColor.Green);

                Output.Write(" maxValue: ");
                Output.Write(maxValue, ConsoleColor.Green);

                Output.Write("): ");

                if (errorMessage.Length > 0)
                {
                    Output.Write(errorMessage, ConsoleColor.Red);
                }

                bool isValid = float.TryParse(Console.ReadLine(), out float number);

                Output.ClearPreviousLine();

                if (!isValid)
                {
                    errorMessage = "Input is invalid. Try again: ";
                    continue;
                }
                if (number < minValue)
                {
                    errorMessage = "Number is too small. Try again: ";
                    continue;
                }
                if (number > maxValue)
                {
                    errorMessage = "Number is too big. Try again: ";
                    continue;
                }

                return number;
            }
        }

        private Monster.Race GetRaceInput(Monster.Race? notAllowedRace = null)
        {
            string errorMessage = "";

            while (true)
            {
                Console.Write("Enter Race (");
                foreach (int i in Enum.GetValues(typeof(Monster.Race)))
                {
                    if (notAllowedRace == (Monster.Race)i)
                    {
                        continue;
                    }
                    Output.Write(i, ConsoleColor.Green);
                    Output.Write(" for " + Enum.GetName(typeof(Monster.Race), i));
                    Output.Write(", ");
                }

                Console.CursorLeft -= 2;
                Output.Write("): ");

                if (errorMessage.Length > 0)
                {
                    Output.Write(errorMessage, ConsoleColor.Red);
                }

                bool isValid = int.TryParse(Console.ReadLine(), out int number);

                Output.ClearPreviousLine();


                if (!isValid)
                {
                    errorMessage = "Input is invalid. Try again. ";
                    continue;
                }
                if (!Enum.IsDefined(typeof(Monster.Race), number) || notAllowedRace == (Monster.Race)number)
                {
                    errorMessage = "Not a valid race. Try again. ";
                    continue;
                }

                return (Monster.Race)number;
            }
        }

        // TODO: I think array with 2 entries makes more sense if the size is fixed
        public List<Monster> Initialize()
        {
            List<Monster> monsters = new List<Monster>(2);

            Output.Write("Enter Attributes for the ");
            Output.Write("first ", ConsoleColor.Cyan);
            Output.Write("monster:");
            Console.WriteLine();

            monsters.Add(CreateMonster(new Vector2(0, 5)));

            Console.CursorTop = 0;
            Output.ClearCurrentLine();

            Output.Write("Enter Attributes for the ");
            Output.Write("second ", ConsoleColor.Cyan);
            Output.Write("monster:");
            Console.WriteLine();

            monsters.Add(CreateMonster(new Vector2(Console.WindowWidth - 30, 5), monsters[0].GetRace()));

            Console.CursorTop = 0;
            Output.ClearCurrentLine();

            return monsters;
        }
    }
}

