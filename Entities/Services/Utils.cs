namespace CmdDirectories.Entities.Services
{
    class Utils // Is this the best way to create this helpers class/file?
    {
        public static string[] HandleCommand()
        {
            Console.Write(Directory.GetCurrentDirectory() + " $ ");
            return Console.ReadLine().Split();
        }

        public static bool IsFile(string path) => Path.GetFileName(path) == path && Path.HasExtension(path);

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

        public static async Task<string> GetPublicIPAddress()
        {
            using HttpClient client = new HttpClient();
            return await client.GetStringAsync("https://api.ipify.org");
        }

        public static string[] GetPathCollectionWithColor(ConsoleColor color, Func<string, string[]> getFunction)
        {
            Console.ForegroundColor = color;
            return getFunction(Directory.GetCurrentDirectory());
        }

        public static void ListWithColor(ConsoleColor color, Func<string, string[]> getFunction)
        {
            string[] collection = GetPathCollectionWithColor(color, getFunction);
            foreach (string item in collection) { Console.WriteLine(item); }
        }

        public static void ListWithColor(ConsoleColor color, Func<string, string[]> getFunction, Func<string, string> printFunction)
        {
            string[] collection = GetPathCollectionWithColor(color, getFunction);
            foreach (var item in collection) { Console.WriteLine(printFunction(item)); }
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public static void RunAnimation(Task task)
        {
            string[] animation = { ".", "..", "..." };

            for (int i = 0; !task.IsCompleted; i++)
            {
                Utils.ClearLine();

                if (i == 3) i = 0;
                Console.Write("Loading" + animation[i]);

                Task.Delay(500).Wait();
            }
        }

        public static void CheckPathExistent(params string[] paths)
        {
            foreach (string path in paths)
            {
                if (!Path.Exists(path)) throw new FileNotFoundException($"The path {path} does not exist.");
            }
        }

        public static async Task RunTask(Action taskAction, string message)
        {
            if (taskAction is not null)
            {
                await Task.Run(taskAction);
                ClearLine();
                Console.WriteLine(message);
            }
        }

        public static async Task RunTask(Action taskAction)
        {
            if (taskAction is not null) await Task.Run(taskAction);
        }
    }
}
