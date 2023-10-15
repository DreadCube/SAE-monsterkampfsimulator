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
    }
}

