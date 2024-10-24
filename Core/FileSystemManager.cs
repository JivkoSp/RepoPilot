﻿using System.Text;

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

        // Method to copy a single file
        public static void CopyFile(string sourceFile, string destFile)
        {
            try
            {
                // Ensure destination directory exists
                string? destDir = Path.GetDirectoryName(destFile);

                if (destDir != null && !Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                // Copy the file to the destination (overwrite if it exists)
                File.Copy(sourceFile, destFile, true);

                Console.WriteLine($"File '{sourceFile}' copied to '{destFile}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to copy file '{sourceFile}'. {ex.Message}");
            }
        }

        // Method to copy a directory and its contents recursively
        public static void CopyDirectory(string sourceDir, string destDir)
        {
            try
            {
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                // Copy all files from the source directory to the destination directory
                foreach (var file in Directory.GetFiles(sourceDir))
                {
                    string destFile = Path.Combine(destDir, Path.GetFileName(file));
                    CopyFile(file, destFile);
                }

                // Recursively copy subdirectories
                foreach (var directory in Directory.GetDirectories(sourceDir))
                {
                    string destSubDir = Path.Combine(destDir, Path.GetFileName(directory));
                    CopyDirectory(directory, destSubDir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to copy directory '{sourceDir}'. {ex.Message}");
            }
        }

        // Main method to handle cp command (supports files and directories with the -r option)
        public static void Copy(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: cp [-r] <source> <destination>");
                return;
            }

            bool isRecursive = args[1] == "-r";
            string sourcePath = isRecursive ? args[2] : args[1];
            string destPath = isRecursive ? args[3] : args[2];

            if (File.Exists(sourcePath))
            {
                // Handle file copying
                CopyFile(sourcePath, destPath);
            }
            else if (Directory.Exists(sourcePath))
            {
                // Handle directory copying (must use -r for recursive)
                if (isRecursive)
                {
                    CopyDirectory(sourcePath, destPath);
                }
                else
                {
                    Console.WriteLine("Error: Source is a directory. Use the -r option to copy directories.");
                }
            }
            else
            {
                Console.WriteLine($"Error: Source '{sourcePath}' does not exist.");
            }
        }

        public static void Rm(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: rm [-rf] <file/directory>");
                return;
            }

            bool recursive = false;
            bool force = false;
            List<string> paths = new List<string>();

            // Parse flags and paths
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "-r")
                {
                    recursive = true;
                }
                else if (args[i] == "-f")
                {
                    force = true;
                }
                else
                {
                    paths.Add(args[i]);  // Collect file or directory paths
                }
            }

            // Process each path
            foreach (var path in paths)
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

                // Check if it's a file or a directory
                if (File.Exists(fullPath))
                {
                    // It's a file
                    if (force || ConfirmDeletion($"Delete file '{path}'?"))
                    {
                        try
                        {
                            File.Delete(fullPath);
                            Console.WriteLine($"Deleted file: {path}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting file '{path}': {ex.Message}");
                        }
                    }
                }
                else if (Directory.Exists(fullPath))
                {
                    // It's a directory
                    if (recursive)
                    {
                        // Recursive deletion
                        if (force || ConfirmDeletion($"Delete directory '{path}' and all its contents?"))
                        {
                            try
                            {
                                Directory.Delete(fullPath, true);
                                Console.WriteLine($"Deleted directory: {path}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting directory '{path}': {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        // Deleting directory without recursive flag
                        Console.WriteLine($"Error: '{path}' is a directory. Use -r to delete it recursively.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: '{path}' does not exist.");
                }
            }
        }

        // Helper function to confirm deletion with the user
        private static bool ConfirmDeletion(string message)
        {
            Console.Write($"{message} (y/n): ");
            var key = Console.ReadKey();
            Console.WriteLine();  // Move to next line after key press
            return key.Key == ConsoleKey.Y;
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
