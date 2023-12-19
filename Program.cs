using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    private static void SetConsoleFont(string fontName, short fontSize)
    {
        IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
        if (hnd != IntPtr.Zero)
        {
            CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX();
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.nFont = 0;
            info.dwFontSize = new COORD(fontSize, fontSize);
            info.FontFamily = TMPF_TRUETYPE;
            info.FontWeight = FW_NORMAL;
            info.FaceName = fontName;

            if (!SetCurrentConsoleFontEx(hnd, false, ref info))
            {
                Console.WriteLine("Unable to set console font.");
            }
        }
    }

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
    private static extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

    static void PrintBanner()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

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

    static void Main()
    {
        Console.SetWindowSize(120, 40);

        SetConsoleFont("Fixedsys", 16);

        // Print ASCII art banner
        PrintBanner();
    }
}