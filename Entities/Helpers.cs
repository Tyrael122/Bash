namespace CmdDirectories.Entities
{
    class Helpers // Is this the best way to create this helpers class/file?
    {
        public static string[] HandleCommand() 
        {
            Console.Write(Directory.GetCurrentDirectory() + " $ ");
            return Console.ReadLine().Split();
        }

        public static bool IsFile(string path)
        {
            return Path.GetFileName(path) == path && Path.HasExtension(path);
        }

        public static void ErrorMessage(string action)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Are you trying to {action} the right things?\n" +
                    $"You can only {action}:\n" +
                    "Files into files\n" +
                    "Files into directories\n" +
                    "Directories into directories");
            Console.ResetColor();
        }
    }
}
