namespace Monsterkampfsimulator
{
    public class SpawnManager
    {
        public static SpawnManager Instance = new SpawnManager();

        private SpawnManager()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

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

        /// <summary>
        /// A helper function that asks the user for input. In this case a float.
        /// If the user entered an invalid float input we provide a proper error message
        /// and the user has to try entering again.
        /// </summary>
        /// <param name="message">output message</param>
        /// <param name="minValue">minimal float value</param>
        /// <param name="maxValue">maximal float value</param>
        /// <returns></returns>
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

        /// <summary>
        /// A helper function to read out the monster race from a user input.
        /// Basically every Monster Race is a option for the user.
        /// Optional: this func can receive a race that will be excluded from the
        /// available race options.
        /// Not valid input will be catched.
        /// The user has to try again if he entered wrong input.
        /// </summary>
        /// <param name="notAllowedRace">
        /// Optional: A race that is not allowed as input
        /// </param>
        /// <returns></returns>
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

        /// <summary>
        /// Initialized a List with monsters. The size is fixed to two.
        /// We compare both monsters entered speed attributes and sort the list
        /// accordingly.
        /// monsters[0] = the monster with more speed.
        /// monsters[1] = the monster with less speed.
        /// </summary>
        /// <returns>A list with two monsters</returns>
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


            // sorts the monsters by speed. monsters[0] will always be the first attacking monster
            monsters.Sort((monsterA, monsterB) => monsterA.GetSpeed() > monsterB.GetSpeed() ? -1 : 1);

            return monsters;
        }
    }
}

