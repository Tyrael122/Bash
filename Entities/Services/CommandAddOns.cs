namespace CmdDirectories.Entities.Services
{
    class CommandsAddOns
    {
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

        public static async Task CopyAsync(string source, string destination)
        {
            Utils.CheckPathExistent(source);

            Action taskAction = null;
            string message = null;

            if (Utils.IsFile(source))
            {
                // Copy file into file
                if (Utils.IsFile(destination))
                {
                    taskAction = delegate () { File.Copy(source, destination); };
                    message = $"Successfully copied file {source} into {destination}";
                }
                // Copy file into directory
                else
                {
                    string destinationDirectory = destination + Path.DirectorySeparatorChar + source;

                    taskAction = delegate () { File.Copy(source, destinationDirectory, true); };
                    message = $"Successfully copied file {source} into directory {destination}";
                }
            }
            //Checks if destination is a directory
            else if (!Utils.IsFile(destination))
            {
                // Copy directory into directory
                taskAction = delegate () { CopyDirectory(source, destination); };
                message = $"Successfully copied directory {source} into {destination}";
            }
            else
                Utils.ErrorMessage("copy");

            await Utils.RunTask(taskAction, message);
        }

        public static async Task MoveAsync(string source, string destination)
        {
            Utils.CheckPathExistent(source, destination);

            Action taskAction = null;
            string message = null;

            if (Utils.IsFile(source))
            {
                destination = Path.GetFullPath(destination) + Path.DirectorySeparatorChar + Path.GetFileName(source);
                taskAction = delegate () { File.Move(source, destination); };
                message = $"Successfully moved file {source} into {destination}";
            }
            // Checks if the destination is a directory (to move a directory into another directory).
            else if (!Utils.IsFile(destination))
            {
                // If the destination directory already exists,
                // it copies the directory into the destination, and deletes the source afterwards.
                if (Directory.Exists(destination))
                {
                    taskAction = delegate ()
                    {
                        CopyDirectory(source, destination);
                        Directory.Delete(source, true);
                    };
                    message = $"Successfully moved directory {source} into {destination}";
                }

                // This command cannot move files or folders into an already existing directory.
                // A manual operation is needed then
                else
                {
                    taskAction = delegate () { Directory.Move(source, destination); };
                    message = $"Successfully moved directory {source} into {destination}";
                }
            }
            else
                Utils.ErrorMessage("move");

            await Utils.RunTask(taskAction, message);

        }

        public static async Task RemoveAsync(string path)
        {
            Utils.CheckPathExistent(path);

            Action taskAction = null;
            if (Utils.IsFile(path))
                taskAction = delegate () { File.Delete(path); };
            else
                taskAction = delegate () { Directory.Delete(path, true); };
            await Utils.RunTask(taskAction);
        }
    }
}
