
using Octokit;
using RepoPilot.Services;
using System.Diagnostics;

namespace RepoPilot.Utils
{
    internal sealed class GitCommandExecutor
    {
        public static void ExecuteGitCommand(string command, string workingDirectory = "")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = command,
                    WorkingDirectory = string.IsNullOrEmpty(workingDirectory) ? Directory.GetCurrentDirectory() : workingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine(output);
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine($"Error: {error}");
            }
        }

        public static void CreateBranch(string branchName)
        {
            ExecuteGitCommand($"checkout -b {branchName}");
        }

        public static void SwitchBranch(string branchName)
        {
            ExecuteGitCommand($"checkout {branchName}");
        }

        public static void MergeBranches(string branchName)
        {
            ExecuteGitCommand($"merge {branchName}");
        }

        public static void ListLocalBranches()
        {
            ExecuteGitCommand("branch");
        }

        public static async Task ListRemoteBranches(GitHubClient client)
        {
            var repoName = UserInteraction.Prompt("Enter the repository name (format: owner/repo): ");

            if (string.IsNullOrEmpty(repoName)) return;

            try
            {
                var repo = await client.Repository.Get(repoName.Split('/')[0], repoName.Split('/')[1]);

                var branches = await client.Repository.Branch.GetAll(repo.Id);

                Console.WriteLine("Remote branches:");

                foreach (var branch in branches)
                {
                    Console.WriteLine($"- {branch.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to list remote branches: {ex.Message}");
            }
        }

        public static async Task DeleteRemoteBranch(GitHubClient client, string branchName)
        {
            var repoName = UserInteraction.Prompt("Enter the repository name (format: owner/repo): ");

            if (string.IsNullOrEmpty(repoName)) return;

            try
            {
                var repo = await client.Repository.Get(repoName.Split('/')[0], repoName.Split('/')[1]);

                await client.Git.Reference.Delete(repo.Id, $"heads/{branchName}");

                Console.WriteLine($"Deleted remote branch: {branchName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete remote branch: {ex.Message}");
            }
        }

        public static void ViewCommitHistory()
        {
            ExecuteGitCommand("log --oneline --graph --decorate");
        }

        public static void AmendLastCommit()
        {
            ExecuteGitCommand("commit --amend");
        }

        public static void CreateSignedCommit()
        {
            string? commitMessage = UserInteraction.Prompt("Enter commit message: ");

            ExecuteGitCommand($"commit -S -m \"{commitMessage}\"");
        }
    }
}
