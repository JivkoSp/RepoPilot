
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
    }
}
