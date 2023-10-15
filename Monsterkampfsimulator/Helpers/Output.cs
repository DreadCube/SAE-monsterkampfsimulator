namespace Monsterkampfsimulator
{
    /**
     * The static Output class represents helper funcs to handle Console Output.
     */
    public static class Output
    {
        /// <summary>
        /// Writes a line of text at a specific position.
        /// </summary>
        /// <param name="text">text to write</param>
        /// <param name="x">Optional: x start position to write text (Fallbacks to Console.CursorLeft)</param>
        /// <param name="y">Optional: y start position to write text (Fallbacks to Console.CursorTop)</param>
        /// <param name="minWidth">Optional: A minimal width of the text.</param>
        public static void WriteLineAtPosition(string text, int? x, int? y = null, int minWidth = 0)
        {
            Console.SetCursorPosition(x ?? Console.CursorLeft, y ?? Console.CursorTop);
            Console.WriteLine(text.PadRight(minWidth, ' '));
        }

        /// <summary>
        /// Writes string text. Optional a foreground Color can be provided.
        /// </summary>
        public static void Write(string text, ConsoleColor? foregroundColor = null)
        {
            Console.ForegroundColor = foregroundColor ?? Console.ForegroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes float text. Optional a foreground Color can be provided.
        /// </summary>
        public static void Write(float text, ConsoleColor? foregroundColor = null)
        {
            Console.ForegroundColor = foregroundColor ?? Console.ForegroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes int text. Optional a foreground Color can be provided.
        /// </summary>
        public static void Write(int text, ConsoleColor? foregroundColor = null)
        {
            Console.ForegroundColor = foregroundColor ?? Console.ForegroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Clears the content of the current line.
        /// </summary>
        public static void ClearCurrentLine()
        {
            Console.CursorLeft = 0;
            Console.Write("".PadRight(Console.WindowWidth - 1));
            Console.CursorLeft = 0;
        }

        /// <summary>
        /// Clears previous line of the console. If we are already on the
        /// first top line we do clear the current line.
        /// </summary>
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

