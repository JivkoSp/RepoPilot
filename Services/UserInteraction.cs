
namespace RepoPilot.Services
{
    internal sealed class UserInteraction
    {
        public static string? Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine()?.Trim();
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("\nAvailable commands:\n");
            Console.WriteLine("  ls [path]              - List directory contents");
            Console.WriteLine("  cd <directory>         - Change directory");
            Console.WriteLine("  mkdir <dir>            - Create a new directory");
            Console.WriteLine("  look <file>            - Display file information");
            Console.WriteLine("  clear                  - Clear the console");
            Console.WriteLine("  git <args>             - Run git commands");
            Console.WriteLine("  pilot <args>             - Run pilot commands");
            Console.WriteLine("  alias <name> <command> - Create an alias");
            Console.WriteLine("  help                   - Display this help message");
            Console.WriteLine("  exit                   - Exit the shell");
            Console.WriteLine();
        }

        public static void DisplayPilotHelp()
        {
            Console.WriteLine("\nAvailable pilot commands:\n");
            Console.WriteLine("  pilot create          - Create a new GitHub repository and optionally initialize it locally");
            Console.WriteLine("  pilot manage branches - Manage branches in the local repository");
            Console.WriteLine("  pilot manage commits  - Manage commits in the local repository");
            Console.WriteLine("  help                  - Display this help message");
            Console.WriteLine();
        }

        public static void DisplayBranchesHelp()
        {
            Console.WriteLine("\nAvailable pilot branch management commands:\n");
            Console.WriteLine("  1. - Create a new branch");
            Console.WriteLine("  2. - List all branches (local)");
            Console.WriteLine("  3. - List all remote branches");
            Console.WriteLine("  4. - Switch between branches (local)");
            Console.WriteLine("  5. - Merge branches (local)");
            Console.WriteLine("  6. - Delete a remote branch");
            Console.WriteLine("  7. - Back to main menu");
            Console.WriteLine();
        }
    }
}
