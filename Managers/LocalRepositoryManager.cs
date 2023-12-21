using Octokit;
using RepoPilot.Services;
using RepoPilot.Utils;

namespace RepoPilot.Managers
{
    internal sealed class LocalRepositoryManager
    {
        public static void CreateLocalRepository(string? localPath, string? repoName)
        {
            localPath = localPath ?? string.Empty;

            repoName = repoName ?? string.Empty;

            if (string.IsNullOrWhiteSpace(localPath))
            {
                Console.WriteLine("Error: Local path cannot be null or empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(repoName))
            {
                Console.WriteLine("Error: Repository name cannot be null or empty.");
                return;
            }

            string repoPath = Path.Combine(localPath, repoName);

            if (Directory.Exists(repoPath))
            {
                if (Directory.Exists(Path.Combine(repoPath, ".git")))
                {
                    Console.WriteLine("Error: Directory is already a Git repository.");
                    return;
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(repoPath);

                    Console.WriteLine($"Created directory: {repoPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create directory: {ex.Message}");
                    return;
                }
            }

            try
            {
                GitCommandExecutor.ExecuteGitCommand("init", repoPath);

                Console.WriteLine($"Initialized new Git repository at: {repoPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize local repository: {ex.Message}");
            }
        }

        public static async Task ManageBranches(GitHubClient client)
        {
            UserInteraction.DisplayBranchesHelp();

            string? choice = UserInteraction.Prompt("Choose an option: ");

            switch (choice)
            {
                case "1":
                    string? branchName = UserInteraction.Prompt("Enter the name of the new branch: ");

                    if (!string.IsNullOrEmpty(branchName))
                    {
                        GitCommandExecutor.CreateBranch(branchName);
                    }
                    break;
                case "2":
                    GitCommandExecutor.ListLocalBranches();
                    break;
                case "3":
                    await GitCommandExecutor.ListRemoteBranches(client);
                    break;
                case "4":
                    string? switchBranch = UserInteraction.Prompt("Enter the branch to switch to: ");
                    if (!string.IsNullOrEmpty(switchBranch))
                    {
                        GitCommandExecutor.SwitchBranch(switchBranch);
                    }
                    break;
                case "5":
                    string? mergeBranch = UserInteraction.Prompt("Enter the branch to merge into the current branch: ");
                    if (!string.IsNullOrEmpty(mergeBranch))
                    {
                        GitCommandExecutor.MergeBranches(mergeBranch);
                    }
                    break;
                case "6":
                    string? remoteBranch = UserInteraction.Prompt("Enter the name of the remote branch to delete: ");
                    if (!string.IsNullOrEmpty(remoteBranch))
                    {
                        await GitCommandExecutor.DeleteRemoteBranch(client, remoteBranch);
                    }
                    break;
                case "7":
                    // Go back to the main menu
                    return;
                default:
                    Console.WriteLine("Unknown option.");
                    break;
            }
        }
    }
}
