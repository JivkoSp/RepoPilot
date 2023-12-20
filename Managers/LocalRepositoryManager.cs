
using RepoPilot.Utils;

namespace RepoPilot.Managers
{
    internal sealed class LocalRepositoryManager
    {
        public static void CreateLocalRepository(string? localPath, string? repoName)
        {
            localPath = localPath ?? string.Empty;

            repoName = repoName ?? string.Empty;
            
            string repoPath = Path.Combine(localPath, repoName);

            if (Directory.Exists(repoPath))
            {
                Console.WriteLine("Directory already exists.");
                return;
            }

            try
            {
                Directory.CreateDirectory(repoPath);
                Console.WriteLine($"Initialized new repository at: {repoPath}");

                GitCommandExecutor.ExecuteGitCommand("init", repoPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize local repository: {ex.Message}");
            }
        }
    }
}
