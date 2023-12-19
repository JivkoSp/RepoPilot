using RepoPilot.Utils;

namespace RepoPilot.Core
{
    internal sealed class Shell
    {
        private string currentDirectory = Directory.GetCurrentDirectory();
        private Dictionary<string, string> _aliases;

        internal Shell()
        {
            _aliases = new Dictionary<string, string>();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("\nAvailable commands:\n");
            Console.WriteLine("  ls [path]              - List directory contents");
            Console.WriteLine("  cd <directory>         - Change directory");
            Console.WriteLine("  mkdir <dir>            - Create a new directory");
            Console.WriteLine("  look <file>            - Display file information");
            Console.WriteLine("  clear                  - Clear the console");
            Console.WriteLine("  git <args>             - Run git commands");
            Console.WriteLine("  alias <name> <command> - Create an alias");
            Console.WriteLine("  help                   - Display this help message");
            Console.WriteLine("  exit                   - Exit the shell");
            Console.WriteLine();
        }

        public void Run()
        {
            // Print ASCII art banner
            ConsoleUtils.PrintBanner();

            while (true)
            {
                // Display the current directory
                Console.Write($"{currentDirectory}> ");

                // Handle tab completion
                var input = ConsoleUtils.ReadLineWithTabCompletion();

                if (string.IsNullOrWhiteSpace(input)) continue;

                // Parse the command and arguments
                string[] commandArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string command = commandArgs[0].ToLower(); // Command is case insensitive

                // Handle aliases
                if (_aliases.ContainsKey(command))
                {
                    commandArgs = _aliases[command].Split(' ').Concat(commandArgs.Skip(1)).ToArray();

                    command = commandArgs[0].ToLower();
                }

                // Execute command based on input
                switch (command)
                {
                    case "ls":
                        
                        break;
                    case "cd":
                        
                        break;
                    case "mkdir":
                        
                        break;
                    case "look":
                       
                        break;
                    case "clear":
                        Console.Clear();
                        ConsoleUtils.PrintBanner();
                        break;
                    case "git":
                       
                        break;
                    case "alias":
                        
                        break;
                    case "help":
                        DisplayHelp();
                        break;
                    case "exit":
                        
                        return;
                    default:
                        Console.WriteLine($"Command '{command}' not found. Type 'help' for commands.");
                        break;
                }
            }
        }


    }
}
