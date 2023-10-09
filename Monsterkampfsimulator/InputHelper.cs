namespace Monsterkampfsimulator
{
    public static class InputHelper
    {
        // TODO: Potentielles feature. Input values würfeln?
        public static float GetFloatInput(string message, float minValue = 0f, float maxValue = 100f)
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

        public static Monster.Race GetRaceInput(Monster.Race? notAllowedRace = null)
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
    }
}

