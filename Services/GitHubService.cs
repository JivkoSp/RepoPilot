using Octokit;

namespace RepoPilot.Services
{
    internal sealed class GitHubService
    {
        private const string TokenFilePath = "github_token.txt";

        public static string GetStoredGitHubToken()
        {
            return File.Exists(TokenFilePath) ? File.ReadAllText(TokenFilePath).Trim() : "";
        }

        public static async Task SetGitHubCredentials()
        {
            Console.Write("Enter your GitHub personal access token: ");
            string? token = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token cannot be empty.");
                return;
            }

            var githubClient = new GitHubClient(new ProductHeaderValue("RepoPilot"))
            {
                Credentials = new Credentials(token)
            };

            try
            {
                var user = await githubClient.User.Current();

                Console.WriteLine($"Authenticated as {user.Login}");

                File.WriteAllText(TokenFilePath, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication failed: {ex.Message}");
            }
        }

        public static async Task CreateRepository(string token)
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("RepoPilot"))
            {
                Credentials = new Credentials(token)
            };

            string? repoName = UserInteraction.Prompt("Enter a name for the repository: ");

            string? repoDescription = UserInteraction.Prompt("Optionally enter a description of the repository: ");

            bool isPublic = UserInteraction.Prompt("Public or private (public/private): ")?.ToLower() == "public";

            try
            {
                var newRepo = new NewRepository(repoName)
                {
                    Description = repoDescription,
                    Private = !isPublic
                };

                var createdRepo = await githubClient.Repository.Create(newRepo);
                Console.WriteLine($"Repository created on GitHub: {createdRepo.HtmlUrl}");

                if (UserInteraction.Prompt("Do you want to create this repository locally as well? (y/n): ")?.ToLower() == "y")
                {
                    string? localPath = UserInteraction.Prompt("Enter the local path where you want the repository to be created: ");
                    if (string.IsNullOrEmpty(localPath) || !Directory.Exists(Path.GetDirectoryName(localPath)))
                    {
                        Console.WriteLine("Invalid path specified.");
                    }
                    else
                    {
                        //LocalRepositoryManager.CreateLocalRepository(localPath, repoName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create repository: {ex.Message}");
            }
        }
    }
}
