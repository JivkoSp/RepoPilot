using RepoPilot.Core;
using RepoPilot.Utils;

class Program
{
    static async Task Main()
    {
        Console.SetWindowSize(120, 40);

        ConsoleUtils.SetConsoleFont("Fixedsys", 16);

        var shell = new Shell();

        await shell.Run();
    }
}