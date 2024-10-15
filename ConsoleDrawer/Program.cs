namespace ConsoleDrawer;
using System;
using System.IO;

class Program
{
    static char[,] screen = new char[25, 80];
    static ConsoleColor[,] screenColors = new ConsoleColor[25, 80];
    static int cursorX = 0, cursorY = 0;
    static ConsoleColor currentColor = ConsoleColor.White;
    static string currentChar = "█";

    static void InitScreen()
    {
        for (int y = 0; y < 25; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                screen[y, x] = ' ';
                screenColors[y, x] = ConsoleColor.Black;
            }
        }
    }

    static void DrawScreen()
    {
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < 25; y++)
        {
            for (int x = 0; x < 80; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = screenColors[y, x];
                Console.Write(screen[y, x]);
            }
        }
        Console.SetCursorPosition(cursorX, cursorY);
        Console.BackgroundColor = currentColor;
        Console.Write(" ");
        Console.ResetColor();
        Console.SetCursorPosition(cursorX, cursorY);
        Console.CursorVisible = false;
    }

    static void Backspace()
    {
        if (cursorX > 0)
        {
            cursorX--;
            screen[cursorY, cursorX] = ' ';
            screenColors[cursorY, cursorX] = ConsoleColor.Black;
            DrawScreen();
        }
    }

    static void MoveCursor(int dx, int dy)
    {
        int newX = cursorX + dx;
        int newY = cursorY + dy;
        if (newX >= 0 && newX < 80 && newY >= 0 && newY < 25)
        {
            cursorX = newX;
            cursorY = newY;
        }
    }

    static void DrawChar(string c, ConsoleColor color)
    {
        for (int i = 0; i < c.Length; i++)
        {
            if (cursorX + i < 80)
            {
                screen[cursorY, cursorX + i] = c[i];
                screenColors[cursorY, cursorX + i] = color;
            }
        }
    }


    static void DisplayMenu()
    {
        Console.Clear();
        ConsoleKeyInfo keyInfo;
        ConsoleKey key;
        int selectedOption = 1;
        bool optionSelected = false;
        Console.CursorVisible = false;

        int menuWidth = 30;
        int menuHeight = 4;
        int menuX = (Console.WindowWidth - menuWidth) / 2;
        int menuY = (Console.WindowHeight - menuHeight) / 2;

        for (int y = menuY; y <= menuY + menuHeight-1; y++)
        {
            Console.SetCursorPosition(menuX - 1, y);
            Console.Write("|");
            Console.SetCursorPosition(menuX + menuWidth, y);
            Console.Write("|");
        }
        Console.SetCursorPosition(menuX - 1, menuY - 1);
        Console.Write("┌");
        for (int x = 0; x < menuWidth; x++)
        {
            Console.Write("¯");
        }
        Console.Write("┐");

        Console.SetCursorPosition(menuX - 1, menuY + menuHeight);
        Console.Write("└");
        for (int x = 0; x < menuWidth; x++)
        {
            Console.Write("¯");
        }
        Console.Write("┘");
        Console.SetCursorPosition(menuX - 1, menuY - 1);
        Console.SetCursorPosition(menuX + menuWidth, menuY - 1);
        Console.SetCursorPosition(menuX - 1, menuY + menuHeight);
        Console.SetCursorPosition(menuX + menuWidth, menuY + menuHeight);

        do
        {
            Console.SetCursorPosition(menuX, menuY);
            Console.WriteLine(selectedOption == 1 ? "> Új rajz létrehozása <" : "  Új rajz létrehozása  ");
            Console.SetCursorPosition(menuX, menuY + 1);
            Console.WriteLine(selectedOption == 2 ? "> Rajz betöltése <" : "  Rajz betöltése  ");
            Console.SetCursorPosition(menuX, menuY + 2);
            Console.WriteLine(selectedOption == 3 ? "> Rajz törlése <" : "  Rajz törlése  ");
            Console.SetCursorPosition(menuX, menuY + 3);
            Console.WriteLine(selectedOption == 4 ? "> Kilépés <" : "  Kilépés  ");
            Console.SetCursorPosition(menuX, menuY + 4);
            keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOption > 1)
                    {
                        selectedOption--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedOption < 4)
                    {
                        selectedOption++;
                    }
                    break;
                case ConsoleKey.Enter:
                    optionSelected = true;
                    break;
            }
        } while (!optionSelected);

        switch (selectedOption)
        {
            case 1:
                CreateNewDrawing();
                break;
            case 2:
                LoadExistingDrawing();
                break;
            case 3:
                DeleteDrawing();
                break;
            case 4:
                Environment.Exit(0);
                break;
        }
    }

    static void DeleteDrawing()
    {
        string[] drawingFiles = Directory.GetFiles(".", "*.txt");

        if (drawingFiles.Length == 0)
        {
            Console.WriteLine("Nincs mentett rajzod.");
            return;
        }

        int selectedOption = 0;
        bool optionSelected = false;
        bool exitMenu = false;

        Console.Clear();

        do
        {
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("Válassz egy rajzot, amit törölni szeretnél!"); 
            int menuWidth = 30;
            int menuHeight = drawingFiles.Length + 2;
            int menuX = (Console.WindowWidth - menuWidth) / 2;
            int menuY = (Console.WindowHeight - menuHeight) / 2;

            for (int y = menuY - 1; y <= menuY + menuHeight; y++)
            {
                Console.SetCursorPosition(menuX - 1, y);
                Console.Write("│");
                Console.SetCursorPosition(menuX + menuWidth, y);
                Console.Write("│");
            }
            Console.SetCursorPosition(menuX - 1, menuY - 1);
            Console.Write("┌");
            Console.SetCursorPosition(menuX + menuWidth, menuY - 1);
            Console.Write("┐");
            Console.SetCursorPosition(menuX - 1, menuY + menuHeight);
            Console.Write("└");
            Console.SetCursorPosition(menuX + menuWidth, menuY + menuHeight);
            Console.Write("┘");

            for (int i = 0; i < drawingFiles.Length; i++)
            {
                Console.SetCursorPosition(menuX, menuY + i + 1);
                Console.WriteLine(selectedOption == i ? $"> {i + 1}. {Path.GetFileNameWithoutExtension(drawingFiles[i])} <" : $"  {i + 1}. {Path.GetFileNameWithoutExtension(drawingFiles[i])}  ");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOption > 0)
                    {
                        selectedOption--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedOption < drawingFiles.Length - 1)
                    {
                        selectedOption++;
                    }
                    break;
                case ConsoleKey.Enter:
                    optionSelected = true;
                    break;
                case ConsoleKey.Escape:
                    exitMenu = true;
                    optionSelected = true;
                    break;
            }
        } while (!optionSelected && !exitMenu);

        if (exitMenu)
        {
            return;
        }

        string selectedDrawingFile = drawingFiles[selectedOption];

        Console.WriteLine("Biztosan törölni szeretnéd a rajzot? Törléshez nyomj ENTER, visszalépéshez nyomj ESC gombot!");
        ConsoleKeyInfo confirmKeyInfo = Console.ReadKey(true);
        ConsoleKey confirmKey = confirmKeyInfo.Key;

        if (confirmKey == ConsoleKey.Enter)
        {
            File.Delete(selectedDrawingFile);
            Console.WriteLine($"Sikeresen törölted a rajzod. Neve: {selectedDrawingFile}");
        }
        else if(confirmKey == ConsoleKey.Escape)
        {
            Console.WriteLine("Törlés megszakítva.");
        }

        Console.WriteLine("Nyomd meg az ESC gombot a visszalépéshez!");
        Console.ReadKey(true);
    }

    static void LoadExistingDrawing()
    {
        string[] drawingFiles = Directory.GetFiles(".", "*.txt");

        if (drawingFiles.Length == 0)
        {
            Console.WriteLine("Nem található ilyen rajz.");
            return;
        }

        int selectedOption = 0;
        bool optionSelected = false;
        bool exitMenu = false;

        Console.Clear();
        do
        {
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("Válassz egy rajzot, amit folytatni szeretnél!");
            int menuWidth = 30;
            int menuHeight = drawingFiles.Length + 2;
            int menuX = (Console.WindowWidth - menuWidth) / 2;
            int menuY = (Console.WindowHeight - menuHeight) / 2;

            for (int y = menuY - 1; y <= menuY + menuHeight; y++)
            {
                Console.SetCursorPosition(menuX - 1, y);
                Console.Write("│");
                Console.SetCursorPosition(menuX + menuWidth, y);
                Console.Write("│");
            }
            Console.SetCursorPosition(menuX - 1, menuY - 1);
            Console.Write("┌");
            Console.SetCursorPosition(menuX + menuWidth, menuY - 1);
            Console.Write("┐");
            Console.SetCursorPosition(menuX - 1, menuY + menuHeight);
            Console.Write("└");
            Console.SetCursorPosition(menuX + menuWidth, menuY + menuHeight);
            Console.Write("┘");

            for (int i = 0; i < drawingFiles.Length; i++)
            {
                Console.SetCursorPosition(menuX, menuY + i + 1);
                Console.WriteLine(selectedOption == i ? $"> {i + 1}. {Path.GetFileNameWithoutExtension(drawingFiles[i])} <" : $"  {i + 1}. {Path.GetFileNameWithoutExtension(drawingFiles[i])}  ");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOption > 0)
                    {
                        selectedOption--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedOption < drawingFiles.Length - 1)
                    {
                        selectedOption++;
                    }
                    break;
                case ConsoleKey.Enter:
                    optionSelected = true;
                    break;
                case ConsoleKey.Escape:
                    exitMenu = true;
                    optionSelected = true;
                    break;
            }
        } while (!optionSelected && !exitMenu);

        if (exitMenu)
        {
            return;
        }

        string selectedDrawingFile = drawingFiles[selectedOption];
        string[] lines = File.ReadAllLines(selectedDrawingFile);

        InitScreen();

        for (int y = 0; y < Math.Min(lines.Length, 25); y++)
        {
            for (int x = 0; x < Math.Min(lines[y].Length, 80); x++)
            {
                screen[y, x] = lines[y][x];
                screenColors[y, x] = ConsoleColor.White;
            }
        }

        Console.SetCursorPosition(10, 10);
        Console.CursorVisible = false;
        Console.Clear(); 
        DrawScreen();
        EditDrawing();
    }

    static void CreateNewDrawing()
    {
        InitScreen();
        DrawScreen();
        EditDrawing();
    }

   

static void EditDrawing()
    {
        while (true)
        {
            DrawScreen();

            ConsoleKeyInfo originalKeyInfo = Console.ReadKey(true);
            ConsoleKey originalKey = originalKeyInfo.Key;

            switch (originalKey)
            {
                case ConsoleKey.Backspace:
                    Backspace();
                    break;
                case ConsoleKey.UpArrow:
                    MoveCursor(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    MoveCursor(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    MoveCursor(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    MoveCursor(1, 0);
                    break;
                case ConsoleKey.Spacebar:
                    DrawChar(currentChar, currentColor);
                    break;
                case ConsoleKey.F1: currentChar = "█";
                    break;
                case ConsoleKey.F2: currentChar = "▓";
                    break;
                case ConsoleKey.F3: currentChar = "▒";
                    break;
                case ConsoleKey.F4: currentChar = "░";
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Console.ResetColor();
                    break;
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    currentColor = ConsoleColor.White;
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    currentColor = ConsoleColor.Red;
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    currentColor = ConsoleColor.Green;
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    currentColor = ConsoleColor.Blue;
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    currentColor = ConsoleColor.Yellow;
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    currentColor = ConsoleColor.Cyan;
                    break;
                case ConsoleKey.D7:
                case ConsoleKey.NumPad7:
                    currentColor = ConsoleColor.Magenta;
                    break;
                case ConsoleKey.D8:
                case ConsoleKey.NumPad8:
                    currentColor = ConsoleColor.Gray;
                    break;
                case ConsoleKey.D9:
                case ConsoleKey.NumPad9:
                    currentColor = ConsoleColor.Black;
                    break;
                case ConsoleKey.Escape:
                    Console.SetCursorPosition(0, 26);
                    Console.WriteLine("Adj meg egy nevet!");
                    string fileName = Console.ReadLine();

                    string[] lines = new string[25];
                    for (int y = 0; y < 25; y++)
                    {
                        string line = "";
                        for (int x = 0; x < 80; x++)
                        {
                            line += screen[y, x];
                        }
                        lines[y] = line;
                    }

                    string filePath = $"{fileName}.txt";
                    File.WriteAllLines(filePath, lines);

                    Console.WriteLine($"Sikeresen mentetted a rajzod. Név: {fileName}");
                    return;
            }
        }
    }

    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
        }
    }
}