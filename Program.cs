using RepoPilot.Core;
using RepoPilot.Utils;

class Program
{
    static void Main()
    {
        Console.SetWindowSize(120, 40);

        ConsoleUtils.SetConsoleFont("Fixedsys", 16);

        var shell = new Shell();

        shell.Run();
    }
}