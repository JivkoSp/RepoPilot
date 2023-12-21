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
    }
}
