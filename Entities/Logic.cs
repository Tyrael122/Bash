using System.Diagnostics;
using System.Net;
using CmdDirectories.Entities.Services;

namespace CmdDirectories.Entities
{
    class Commands
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
            Utils.ListWithColor(ConsoleColor.Blue, Directory.GetDirectories);

            // List files
            Utils.ListWithColor(ConsoleColor.Red, Directory.GetFiles, Path.GetFileName);

            Console.ResetColor();
            Console.WriteLine();
        }

        public static void Copy(string source, string destination)
        {
            Task task = CommandsAddOns.CopyAsync(source, destination);
            Utils.RunAnimation(task);
            if (task.Exception != null) throw task.Exception.GetBaseException();
        }

        public static void Move(string source, string destination)
        {
            Task task = CommandsAddOns.MoveAsync(source, destination);
            Utils.RunAnimation(task);
            if (task.Exception != null) throw task.Exception.GetBaseException();
        }

        public static void Remove(string path)
        {
            Task task = CommandsAddOns.RemoveAsync(path);
            Utils.RunAnimation(task);
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

            if (Utils.IsFile(source))
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

        public static void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public static void ProcessesStatus()
        {
            List<Process> processes = Process.GetProcesses().ToList();
            processes.Sort((x, y) => y.WorkingSet64.CompareTo(x.WorkingSet64));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("{0,-10} {1,-40} {2,-30} {3, -20}", "ID", "Name", "CPU Time", "Memory Usage (MB)"));
            Console.ResetColor();

            foreach (Process process in processes)
            {
                try
                {
                    Console.WriteLine(string.Format("{0, -10} {1, -40} {2, -30} {3, -20}",
                        process.Id, process.ProcessName, process.TotalProcessorTime.ToString(@"hh\:mm\:ss"), process.WorkingSet64 / 1024 / 1024));
                }
                catch (Exception) { continue; }
            }
        }

        public static void GetIPAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress localIP = Dns.GetHostEntry(hostName).AddressList[1];

            Console.WriteLine($"Local address: {localIP}");
            Console.Write("Public address: ");
            Console.WriteLine(Utils.GetPublicIPAddress().Result);
        }
    }
}
