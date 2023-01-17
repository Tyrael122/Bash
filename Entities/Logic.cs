namespace CmdDirectories.Entities
{
    class Cmd
    {
        public static void ChangeDirectory(string[] command)
        {
            if (command.Length > 1)
            {
                string path = Directory.GetCurrentDirectory() + command[1];
                if (Directory.Exists(command[1]))
                    Directory.SetCurrentDirectory(command[1]);
                else if (Directory.Exists(path))
                    Directory.SetCurrentDirectory(path);
                else
                    Console.WriteLine("Directory not found.");
            }
            else
            {
                DirectoryInfo parent = Directory.GetParent(Directory.GetCurrentDirectory());
                if (parent is not null)
                    Directory.SetCurrentDirectory(parent.ToString());
            }
                
        }

        public static void List()
        {
            // List directories
            Console.ForegroundColor = ConsoleColor.Blue;
            string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());
            foreach (string directory in directories)
            {
                Console.WriteLine(directory);
            }

            // List files
            Console.ForegroundColor = ConsoleColor.Red;
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        public static void Copy(string source, string destination)
        {
            if (!(Path.Exists(source) || Path.Exists(destination)))
            {
                Console.WriteLine("Source or destination not found.");
                return;
            }
            
            if (Helpers.IsFile(source))
            {
                // Copy file into file
                if (Helpers.IsFile(destination))
                {
                    File.Copy(source, destination, true);
                    Console.WriteLine($"Successfully copied file {source} into {destination}");
                }
                // Copy file into directory
                else
                {
                    string destinationDirectory = destination + Path.DirectorySeparatorChar + source;
                
                    File.Copy(source, destinationDirectory, true);
                    Console.WriteLine($"Successfully copied file {source} into directory {destination}");
                }
            }
            // Checks if destination is a directory.
            else if (!Helpers.IsFile(destination))
            {
                // Copy directory into directory
                CopyDirectory(source, destination);
                Console.WriteLine($"Successfully copied directory {source} into {destination}");
            }
            else
                Helpers.ErrorMessage("copy");
        }

        public static void CopyDirectory(string sourceDirectory, string destinationDirectory)
        {
            string currentDirectory = destinationDirectory + Path.DirectorySeparatorChar + sourceDirectory;
            // Create the subdirectory in the destination directory.
            Directory.CreateDirectory(currentDirectory);

            // Copy the files in the source subdirectory to the destination subdirectory.
            string[] files = Directory.GetFiles(sourceDirectory);
            foreach (string file in files)
            {
                File.Copy(file, currentDirectory + Path.DirectorySeparatorChar + Path.GetFileName(file), true);
            }

            // Get the subdirectories for the specified directory.
            string[] subdirectories = Directory.GetDirectories(sourceDirectory);
            // Loops through the directories of the current directory and copies them.
            foreach (string subdirectory in subdirectories)
            {
                CopyDirectory(subdirectory, destinationDirectory);
            }
        }

        public static void Move(string source, string destination)
        {
            if (!(Path.Exists(source) || Path.Exists(destination)))
            {
                Console.WriteLine("Source or destination does not exist.");
                return;
            }
            
            if (Helpers.IsFile(source))
            {
                destination = Path.GetFullPath(destination) + Path.DirectorySeparatorChar + Path.GetFileName(source);
                File.Move(source, destination);
            }
            // Checks if the destination is a directory (to move a directory into another directory).
            else if (!Helpers.IsFile(destination))
            {
                // If the destination directory already exists,
                // it copies the directory into the destination, and deletes the source afterwards.
                if (Directory.Exists(destination))
                {
                    CopyDirectory(source, destination);
                    Directory.Delete(source, true);
                    Console.WriteLine("Successfully deleted source directory.\nMoving has finished.");
                }

                // This command cannot move files or folders into an already existing directory.
                // A manual operation is needed then
                else
                    Directory.Move(source, destination);
            }
            else
                Helpers.ErrorMessage("move");
        }
        
        public static void Remove(string path)
        {
            if (!Path.Exists(path))
            {
                Console.WriteLine("The specified path does not exist.");
                return;
            }

            if (Helpers.IsFile(path))
                File.Delete(path);
            else
                Directory.Delete(path, true);
        }

        public static void Rename(string source, string targetName)
        {
            if (!Path.Exists(source))
            {
                Console.WriteLine("The specified path does not exist.");
                return;
            }

            if (Path.Exists(targetName))
            {
                Console.WriteLine("Target name already exists.");
                return;
            }

            if (Helpers.IsFile(source))
                File.Move(source, targetName);
            else
                Directory.Move(source, targetName);
        }

        public static void CreateFile(string fileName)
        {
            if (!Path.HasExtension(fileName))
            {
                Console.WriteLine("Please specify an extension to the file.");
                return;
            }

            File.Create(fileName);
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
