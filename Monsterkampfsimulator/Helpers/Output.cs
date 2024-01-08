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

        /// <summary>
        /// Shows Information about which Monster Type has which buffs
        /// </summary>
        public static void ShowBuffTable()
        {
            Console.CursorTop = Console.WindowHeight - 13;

            Console.WriteLine("Buff List");

            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "Race", "Buffs on Attack", "Buffs on getting Attacked"));
            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "", "", ""));

            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "Ork", "25% change:", ""));
            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "", " - critical hit. Enemy defense will be ignored", ""));
            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "", " - but defense goes to zero", ""));

            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "Troll", "33% change:", ""));
            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "", " - steal 20% health from enemy", ""));
            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "", " - defense will be reduced by stolen amount", ""));

            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "Goblin", "", "50% change:"));
            Console.WriteLine(string.Format("|{0,-10}|{1,-50}|{2,-50}|", "", "", "- receives zero damage but defense will be halfed"));
        }
    }
}

