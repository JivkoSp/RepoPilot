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

        public static void PrintWorkingDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            Console.WriteLine($"Current directory: {currentDirectory}");
        }

        public static void TouchFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    // If the file exists, update its last write time to the current time
                    File.SetLastWriteTime(filePath, DateTime.Now);
                    Console.WriteLine($"File '{filePath}' timestamp updated.");
                }
                else
                {
                    // If the file doesn't exist, create an empty file
                    using (File.Create(filePath))
                    {
                        Console.WriteLine($"File '{filePath}' created.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to touch file '{filePath}'. {ex.Message}");
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

        public static void MakeDirectory(string currentDirectory, string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: mkdir <directory>");
                return;
            }

            string path = args[1];
            string newDirectory = Path.GetFullPath(Path.Combine(currentDirectory, path));

            if (!Directory.Exists(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
                Console.WriteLine($"Directory '{newDirectory}' created.");
            }
            else
            {
                Console.WriteLine($"Directory '{newDirectory}' already exists.");
            }
        }

        public static void RemoveDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                try
                {
                    Directory.Delete(dirPath, recursive: false);
                    Console.WriteLine($"Directory '{dirPath}' removed successfully.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error: The directory '{dirPath}' is not empty.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Failed to remove directory '{dirPath}'. {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: Directory '{dirPath}' does not exist.");
            }
        }

        public static void LookCommand(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"{fileName} does not exist or is not a file.");
                return;
            }

            var fileSize = new FileInfo(fileName).Length;

            var executableExtensions = new HashSet<string> { ".exe", ".bat", ".sh", ".cmd" };

            var fileExtension = Path.GetExtension(fileName).ToLower();

            var isExecutable = executableExtensions.Contains(fileExtension);

            var lastAccessTime = File.GetLastAccessTime(fileName);

            Console.Write($"\n  File: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{fileName}\n");
            Console.ResetColor();
            Console.Write($"  Size: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{fileSize} ");
            Console.ResetColor();
            Console.Write($"bytes.\n");
            Console.Write($"  Executable: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{(isExecutable ? "Yes" : "No")}\n");
            Console.ResetColor();
            Console.Write($"  Last accessed: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{lastAccessTime}\n\n");
            Console.ResetColor();
        }
    }
}
