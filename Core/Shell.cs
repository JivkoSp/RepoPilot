using RepoPilot.Utils;
using System.Diagnostics;

namespace RepoPilot.Core
{
    internal sealed class Shell
    {
        private string _currentDirectory;
        private readonly string _historyFilePath;
        private Dictionary<string, string> _aliases;
        private List<string> _commandHistory;

        internal Shell()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
            _historyFilePath = "commandHistory.txt";
            _aliases = new Dictionary<string, string>();
            _commandHistory = new List<string>();
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

        private void LoadCommandHistory()
        {
            if (File.Exists(_historyFilePath))
            {
                _commandHistory = File.ReadAllLines(_historyFilePath).ToList();
            }
        }

        private void SaveCommandHistory()
        {
            File.WriteAllLines(_historyFilePath, _commandHistory);

            Console.WriteLine("\n Command History **Saved**");
            Console.WriteLine("\n Good Bye! \n");
        }

        private void RunExternalCommand(string command, string arguments)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
            {
                Console.WriteLine($"\n{output}");
            }
            if (!string.IsNullOrEmpty(error))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ResetColor();
            }
        }

        public void Run()
        {
            // Load command history from file
            LoadCommandHistory();

            // Print ASCII art banner
            ConsoleUtils.PrintBanner();

            while (true)
            {
                // Display the current directory
                Console.Write($"{_currentDirectory}> ");

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
                        FileSystemManager.ListDirectoryContents(_currentDirectory, commandArgs);
                        break;
                    case "cd":
                        FileSystemManager.ChangeDirectory(ref _currentDirectory, commandArgs);
                        break;
                    case "mkdir":
                        FileSystemManager.MakeDirectory(_currentDirectory, commandArgs);
                        break;
                    case "look":
                       
                        break;
                    case "clear":
                        Console.Clear();
                        ConsoleUtils.PrintBanner();
                        break;
                    case "git":
                        RunExternalCommand("git", string.Join(' ', commandArgs.Skip(1)));
                        break;
                    case "alias":
                        
                        break;
                    case "help":
                        DisplayHelp();
                        break;
                    case "exit":
                        SaveCommandHistory();
                        return;
                    default:
                        Console.WriteLine($"Command '{command}' not found. Type 'help' for commands.");
                        break;
                }
            }
        }


    }
}
