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
    }
}
