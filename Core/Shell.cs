using Octokit;
using RepoPilot.Managers;
using RepoPilot.Services;
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

        // Helper method to abbreviate the path if it's too long
        private string AbbreviatePath(string path)
        {
            string[] parts = path.Split('\\');

            if (parts.Length <= 4) return path;

            return $"{parts[0]}\\{parts[1]}\\...\\{parts[parts.Length - 2]}\\{parts[parts.Length - 1]}";
        }

        public async Task RunPilotCommandAsync(string arguments)
        {
            if (string.IsNullOrEmpty(arguments))
            {
                Console.WriteLine("\nUsage: pilot <pilot command>");

                UserInteraction.DisplayPilotHelp();
            }
            else
            {
                string token = GitHubService.GetStoredGitHubToken();

                if (string.IsNullOrEmpty(token))
                {
                    await GitHubService.SetGitHubCredentials();
                    token = GitHubService.GetStoredGitHubToken();
                }

                arguments = arguments.ToLower();

                switch (arguments)
                {
                    case "create":
                        await GitHubService.CreateRepository(token);
                        break;
                    case "manage branches":
                        if (!string.IsNullOrEmpty(token))
                        {
                            var githubClient = new GitHubClient(new ProductHeaderValue("RepoPilot"))
                            {
                                Credentials = new Credentials(token)
                            };

                            await LocalRepositoryManager.ManageBranches(githubClient);
                        }
                        else
                        {
                            Console.WriteLine("No GitHub token found. Please create a repository first.");
                        }
                        break;
                    case "manage commits":
                        LocalRepositoryManager.ManageCommits();
                        break;
                }
            }
        }

        public async Task Run()
        {
            // Load command history from file
            LoadCommandHistory();

            // Print ASCII art banner
            ConsoleUtils.PrintBanner();

            while (true)
            {
                string currentBranch = GitHubService.GetCurrentBranchName(_currentDirectory);

                Console.ForegroundColor = ConsoleColor.Cyan;

                // Abbreviate the path if it's too long
                string abbreviatedPath = AbbreviatePath(_currentDirectory);

                // Display the current directory
                Console.Write($"{abbreviatedPath} ");

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write($"({currentBranch})> ");

                Console.ResetColor();

                // Handle tab completion
                var input = ConsoleUtils.ReadLineWithTabCompletion(_currentDirectory);

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
                    case "rmdir":
                        if (commandArgs.Length > 1)
                        {
                            string dirPath = commandArgs[1];
                            FileSystemManager.RemoveDirectory(dirPath);
                        }
                        else
                        {
                            Console.WriteLine("Usage: rmdir <directory>");
                        }
                        break;
                    case "pwd":
                        FileSystemManager.PrintWorkingDirectory();
                        break;
                    case "touch":
                        if (commandArgs.Length > 1)
                        {
                            string filePath = commandArgs[1];
                            FileSystemManager.TouchFile(filePath);
                        }
                        else
                        {
                            Console.WriteLine("Usage: touch <file>");
                        }
                        break;
                    case "look":
                        if (commandArgs.Length > 1)
                        {
                            FileSystemManager.LookCommand(commandArgs[1]);
                        }
                        else
                        {
                            Console.WriteLine("Usage: look <file name>");
                        }
                        break;
                    case "git":
                        RunExternalCommand("git", string.Join(' ', commandArgs.Skip(1)));
                        break;
                    case "pilot":
                        await RunPilotCommandAsync(string.Join(' ', commandArgs.Skip(1)));
                        break;
                    case "alias":
                        AliasManager.HandleAliasCommand(ref _aliases, commandArgs);
                        break;
                    case "help":
                       UserInteraction.DisplayHelp();
                        break;
                    case "clear":
                        Console.Clear();
                        ConsoleUtils.PrintBanner();
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
