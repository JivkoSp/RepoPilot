
using System.Text;

namespace RepoPilot.Core
{
    internal sealed class FileSystemManager
    {
        private static void PrintFileOrDirectoryInfo(string path, bool isDirectory, int nameColumnWidth)
        {
            string name = Path.GetFileName(path);
            var info = new FileInfo(path);
            string permissions = GetPermissions(info);
            string lastAccessed = info.LastAccessTime.ToString("yyyy-MM-dd HH:mm:ss");

            if (isDirectory)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[DIR]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[FILE]");
            }
            Console.ResetColor();

            int permissionColumnWidth = 5;
            int dateColumnWidth = 20;

            Console.Write(" ");
            Console.Write(name.PadRight(nameColumnWidth));
            Console.Write(" ");
            Console.Write(permissions.PadRight(permissionColumnWidth));
            Console.Write(" ");
            Console.Write(lastAccessed.PadRight(dateColumnWidth));
            Console.WriteLine();
        }

        private static string GetPermissions(FileInfo fileInfo)
        {
            var permissions = new StringBuilder();

            if (fileInfo.Exists)
            {
                if (fileInfo.IsReadOnly)
                {
                    permissions.Append("r--");
                }
                else
                {
                    permissions.Append("rw-");
                }
            }
            else
            {
                permissions.Append("---");
            }

            return permissions.ToString();
        }

        public static void ListDirectoryContents(string currentDirectory, string[] args)
        {
            string path = currentDirectory;
            if (args.Length > 1)
            {
                path = args[1];
            }

            try
            {
                string[] files = Directory.GetFiles(path);

                string[] directories = Directory.GetDirectories(path);

                int maxNameLength = directories.Concat(files).Select(Path.GetFileName).Max(name => name?.Length) ?? 0;

                Console.WriteLine($"\nContents of {path}:\n");

                foreach (var directory in directories)
                {
                    PrintFileOrDirectoryInfo(directory, isDirectory: true, maxNameLength);
                }
                foreach (var file in files)
                {
                    PrintFileOrDirectoryInfo(file, isDirectory: false, maxNameLength);
                }

                Console.WriteLine("\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error listing contents: {e.Message}");
            }
        }

        public static void ChangeDirectory(ref string currentDirectory, string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: cd <directory>");
                return;
            }

            string path = args[1];

            string newDirectory = Path.GetFullPath(Path.Combine(currentDirectory, path));

            if (Directory.Exists(newDirectory))
            {
                currentDirectory = newDirectory;
            }
            else
            {
                Console.WriteLine($"Directory '{newDirectory}' does not exist.");
            }
        }
    }
}
