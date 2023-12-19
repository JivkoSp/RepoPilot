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


            }
        }
    }
}
