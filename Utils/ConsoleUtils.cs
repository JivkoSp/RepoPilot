
using System.Runtime.InteropServices;
using System.Text;

namespace RepoPilot.Utils
{
    internal sealed class ConsoleUtils
    {
        private static List<string> _commandHistory = new List<string>();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CONSOLE_FONT_INFO_EX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;

            public COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        private const int STD_OUTPUT_HANDLE = -11;
        private const int TMPF_TRUETYPE = 0x4;
        private const int FW_NORMAL = 400;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, 
            ref CONSOLE_FONT_INFO_EX lpConsoleCurrentFontEx);

        public static void SetConsoleFont(string fontName, short fontSize)
        {
            IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
            if (hnd != IntPtr.Zero)
            {
                CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX
                {
                    cbSize = (uint)Marshal.SizeOf(typeof(CONSOLE_FONT_INFO_EX)),
                    nFont = 0,
                    dwFontSize = new COORD(fontSize, fontSize),
                    FontFamily = TMPF_TRUETYPE,
                    FontWeight = FW_NORMAL,
                    FaceName = fontName
                };

                if (!SetCurrentConsoleFontEx(hnd, false, ref info))
                {
                    Console.WriteLine("Unable to set console font.");
                }
            }
        }

        public static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(@"
                    ____                      ____  _ __      __ 
                   / __ \___  ____  ____     / __ \(_) /___  / /_
                  / /_/ / _ \/ __ \/ __ \   / /_/ / / / __ \/ __/
                 / _, _/  __/ /_/ / /_/ /  / ____/ / / /_/ / /_  
                /_/ |_|\___/ .___/\____/  /_/   /_/_/\____/\__/  
                          /_/                                                               
            ");

            Console.ResetColor();

            Console.WriteLine("Type 'help' for commands or 'exit' to quit.\n");
        }

        public static string ReadLineWithTabCompletion()
        {
            var input = new StringBuilder();
            var tabCompletionOptions = new List<string>();
            int tabIndex = -1;
            int cursorPosition = 0;
            int historyIndex = -1;
            int originalConsoleCursorLeft = Console.CursorLeft;

            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return input.ToString();
                }
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Length--;
                    Console.Write("\b \b");
                    tabIndex = -1;
                }
                else if (key.Key == ConsoleKey.Tab)
                {
                    if (tabIndex == -1)
                    {
                        string currentInput = input.ToString();
                        tabCompletionOptions = GetTabCompletionOptions(currentInput);
                        tabIndex = 0;
                    }
                    else
                    {
                        tabIndex = (tabIndex + 1) % tabCompletionOptions.Count;
                    }

                    if (tabCompletionOptions.Count > 0)
                    {
                        ClearCurrentLine(input.Length);
                        input.Clear();
                        input.Append(tabCompletionOptions[tabIndex]);
                        cursorPosition = input.Length;
                        Console.CursorLeft = originalConsoleCursorLeft;
                        Console.Write(tabCompletionOptions[tabIndex]);
                        Console.CursorLeft = originalConsoleCursorLeft + cursorPosition;
                    }
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (cursorPosition >= 0)
                    {
                        cursorPosition = cursorPosition == 0 ? cursorPosition : cursorPosition - 1;
                        Console.CursorLeft = originalConsoleCursorLeft + cursorPosition;
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (cursorPosition < input.Length)
                    {
                        cursorPosition++;
                        Console.CursorLeft = originalConsoleCursorLeft + cursorPosition;
                    }
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (historyIndex > 0)
                    {
                        historyIndex--;
                        ClearCurrentLine(input.Length);
                        input.Clear();
                        input.Append(_commandHistory[historyIndex]);
                        Console.Write(_commandHistory[historyIndex]);
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (historyIndex < _commandHistory.Count - 1)
                    {
                        historyIndex++;
                        ClearCurrentLine(input.Length);
                        input.Clear();
                        input.Append(_commandHistory[historyIndex]);
                        Console.Write(_commandHistory[historyIndex]);
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                    tabIndex = -1;
                }
            }
        }

        public static List<string> GetTabCompletionOptions(string currentInput)
        {
            var options = new List<string>();
            string[] parts = currentInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                options.AddRange(GetAllCommands());
            }
            else if (parts.Length == 1)
            {
                options.AddRange(GetAllCommands().Where(c => c.StartsWith(parts[0], StringComparison.OrdinalIgnoreCase)));
                options.AddRange(Directory.GetFileSystemEntries(Directory.GetCurrentDirectory())
                .Where(f => Path.GetFileName(f).StartsWith(parts[0], StringComparison.OrdinalIgnoreCase))
                .Select(f => Path.GetFileName(f)));
            }
            else
            {
                string lastPart = parts[^1];
                string directoryPath = parts.Length > 1 ? string.Join(' ', parts.Skip(1).Reverse().Skip(1).Reverse()) : Directory.GetCurrentDirectory();
                directoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), directoryPath));
                if (Directory.Exists(directoryPath))
                {
                    options.AddRange(Directory.GetFileSystemEntries(directoryPath)
                        .Where(f => Path.GetFileName(f).StartsWith(lastPart, StringComparison.OrdinalIgnoreCase))
                        .Select(f => $"{parts[0]} {string.Join(' ', parts.Skip(1).Reverse().Skip(1).Reverse())} {Path.GetFileName(f)}"));
                }
            }

            return options;
        }

        private static List<string> GetAllCommands()
        {
            return new List<string> { "ls", "cd", "mkdir", "look", "clear", "git", "alias", "help", "exit" };
        }

        private static void ClearCurrentLine(int length)
        {
            Console.CursorLeft -= length;
            Console.Write(new string(' ', length));
            Console.CursorLeft -= length;
        }
    }
}
