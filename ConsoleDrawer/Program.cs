namespace ConsoleDrawer
{
    internal class Program
    {
        static void DrawSymbol(char? character = null)
        {
            var cursorPosition = Console.GetCursorPosition();
            Console.Write(character ?? symbol);
            Console.SetCursorPosition(cursorPosition.Left, cursorPosition.Top);
        }
        private static char symbol = '█';
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        static void Main(string[] args)
        {
            ConsoleKeyInfo info;
            do
            {
                info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow: if (0 < Console.CursorTop) Console.CursorTop -= 1; break;
                    case ConsoleKey.DownArrow: if (Console.CursorTop < Console.WindowHeight - 1) Console.CursorTop += 1; break;
                    case ConsoleKey.LeftArrow: if (0 < Console.CursorLeft) Console.CursorLeft -= 1; break;
                    case ConsoleKey.RightArrow: if (Console.CursorLeft < Console.WindowWidth - 1) Console.CursorLeft += 1; break;
                    case ConsoleKey.F1: symbol = '█'; break;
                    case ConsoleKey.F2: symbol = '▓'; break;
                    case ConsoleKey.F3: symbol = '▒'; break;
                    case ConsoleKey.F4: symbol = '░'; break;
                    case ConsoleKey.Spacebar:
                        DrawSymbol(); break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        Console.ResetColor(); break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.ForegroundColor = ConsoleColor.White; break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.ForegroundColor = ConsoleColor.Red; break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.ForegroundColor = ConsoleColor.Green; break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.ForegroundColor = ConsoleColor.Blue; break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.ForegroundColor = ConsoleColor.Cyan; break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.ForegroundColor = ConsoleColor.Magenta; break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        Console.ForegroundColor = ConsoleColor.Gray; break;
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        Console.ForegroundColor = ConsoleColor.Black; break;
                    case ConsoleKey.Add:
                        switch (Console.ForegroundColor)
                        {
                            case ConsoleColor.DarkBlue: Console.ForegroundColor = ConsoleColor.Blue; break;
                            case ConsoleColor.DarkGreen: Console.ForegroundColor = ConsoleColor.Green; break;
                            case ConsoleColor.DarkCyan: Console.ForegroundColor = ConsoleColor.Cyan; break;
                            case ConsoleColor.DarkRed: Console.ForegroundColor = ConsoleColor.Red; break;
                            case ConsoleColor.DarkMagenta: Console.ForegroundColor = ConsoleColor.Magenta; break;
                            case ConsoleColor.DarkYellow: Console.ForegroundColor = ConsoleColor.Yellow; break;
                            case ConsoleColor.DarkGray: Console.ForegroundColor = ConsoleColor.Gray; break;
                        }
                        break;
                    case ConsoleKey.Subtract:
                        switch (Console.ForegroundColor)
                        {
                            case ConsoleColor.Blue: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                            case ConsoleColor.Green: Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                            case ConsoleColor.Cyan: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                            case ConsoleColor.Red: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                            case ConsoleColor.Magenta: Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                            case ConsoleColor.Yellow: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                            case ConsoleColor.Gray: Console.ForegroundColor = ConsoleColor.DarkGray; break;
                        }
                        break;
                    case ConsoleKey.Delete:
                    case ConsoleKey.Backspace:
                        var color = Console.ForegroundColor;
                        Console.ResetColor();
                        DrawSymbol(' ');
                        Console.ForegroundColor = color;
                        break;
                }
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        if (Console.CapsLock)
                            DrawSymbol();
                        break;
                }
            } while (info.Key != ConsoleKey.Escape);
        }
    }
}