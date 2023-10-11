namespace Monsterkampfsimulator
{
    public static class Output
    {
        public static void WriteLineAtPosition(string text, int? x, int? y = null, int minWidth = 0)
        {
            Console.SetCursorPosition(x ?? Console.CursorLeft, y ?? Console.CursorTop);
            Console.WriteLine(text.PadRight(minWidth, ' '));
        }

        public static void Write(string text, ConsoleColor? foregroundColor = null)
        {
            Console.ForegroundColor = foregroundColor ?? Console.ForegroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void Write(float text, ConsoleColor? foregroundColor = null)
        {
            Console.ForegroundColor = foregroundColor ?? Console.ForegroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void Write(int text, ConsoleColor? foregroundColor = null)
        {
            Console.ForegroundColor = foregroundColor ?? Console.ForegroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void ClearCurrentLine()
        {
            Console.CursorLeft = 0;
            Console.Write("".PadRight(Console.WindowWidth - 1));
            Console.CursorLeft = 0;
        }

        public static void ClearPreviousLine()
        {
            if (Console.CursorTop > 0)
            {
                Console.CursorTop -= 1;
            }
            ClearCurrentLine();
        }

        // TODO: Finalize if time left.
        public static void WriteBackground()
        {
            // Blue Sky
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < Console.WindowHeight / 2; i++)
            {

                Console.CursorTop = i;
                Console.CursorLeft = 0;
                Console.Write(" ".PadRight(Console.WindowWidth - 1));
            }

            // Green Grass
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for (int i = Console.WindowHeight / 2; i < Console.WindowHeight; i++)
            {
                Console.CursorTop = i;
                Console.CursorLeft = 0;
                Console.Write(" ".PadRight(Console.WindowWidth - 1));
            }

            // Sun!
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.CursorTop = 2;

            for (int i = 0; i < 3; i++)
            {
                Console.CursorTop += 1;
                Console.CursorLeft = Console.WindowWidth - 15;
                Console.Write(" ".PadRight(6));
            }




            Console.CursorTop = 0;
            Console.CursorLeft = 0;

            Console.ResetColor();
        }
    }
}

